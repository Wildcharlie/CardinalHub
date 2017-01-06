define([
    'jquery',
    'google'
], function () {
    $('#Save').click(function () {
        $('#EventStartDateTime').val($('#Date').val() + ' ' + $('#StartTime').val());
        if ($('#EndTime').val().length > 0) {
            $('#EventStartEndTime').val($('#Date').val() + ' ' + $('#EndTime').val());
        }
        var form = $("#createEventForm");
        var url = form.attr("action");
        var formData = form.serialize();
        $.post(url, formData, function (data) {
            if (data) {
                location.href = location.origin + '/Event/Details/' + data;
            } else {
                $('#msg').html('<div class="alert alert-dismissible alert-danger"><button type="button" class="close" data-dismiss="alert">×</button>\
                                <span>Failed to add event! Make sure all forms are filled out and you selected a location on the map.</span></div>');
            }
        });
    });

    $('#eventModal').on('shown.bs.modal', function () {
        if (typeof modalMapLoad === 'undefined') {
            var myLatlng = new google.maps.LatLng(38.2157472, -85.7590743);
            var myOptions = {
                zoom: 17,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            modalMapLoad = new google.maps.Map(document.getElementById('modalMap'), myOptions);
            geocoder = new google.maps.Geocoder;
            infowindow = new google.maps.InfoWindow;

            google.maps.event.addListener(modalMapLoad, 'click', function (event) {
                placeMarker(event.latLng);
                geocodeLatLng(geocoder, modalMapLoad, infowindow, event, marker);
                document.getElementById("Latitude").value = event.latLng.lat();
                document.getElementById("Longitude").value = event.latLng.lng();
                getAddress(event.latLng);
            });

        }
    });

    function getAddress(latLng) {
        geocoder.geocode({ 'latLng': latLng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK && results[0]) {
                document.getElementById("Address").value = results[0].formatted_address.split(',')[0];
            }
            else document.getElementById("Address").value = null;
        });
    }

    function placeMarker(location) {
        if (typeof marker !== 'undefined') {
            marker.setPosition(location);
        } else {
            marker = new google.maps.Marker({
                position: location,
                map: modalMapLoad,
                draggable: true,
                animation: google.maps.Animation.BOUNCE,
            });
        }
    }

    function geocodeLatLng(geocoder, map, infowindow, event, marker) {
        geocoder.geocode({
            'location': event.latLng
        }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    //map.setZoom(11);
                    infowindow.setContent(results[1].formatted_address);
                    infowindow.open(map, marker);
                } else {
                    window.alert('No results found');
                }
            } else {
                window.alert('Geocoder failed due to: ' + status);
            }
        });
    }
});
