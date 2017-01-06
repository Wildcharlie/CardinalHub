define([
    'jquery',
    'google',
    'updatemarkers'
], function () {
    $('#eventModal').on('shown.bs.modal', function () {
        if (typeof modalMapLoad === 'undefined') {
            var modalLatLng = { lat: parseFloat($('#MapData').data('lat')), lng: parseFloat($('#MapData').data('long')) };
            var myOptions = {
                zoom: 17,
                center: modalLatLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            modalMapLoad = new google.maps.Map(document.getElementById('modalMap'), myOptions);
            geocoder = new google.maps.Geocoder;
            modalInfowindow = new google.maps.InfoWindow;

            modalMarker = new Marker({
                map: modalMapLoad,
                position: modalLatLng,
                icon: {
                    path: marker_options[$('#MapData').data('iconmarker')],
                    fillColor: $('#MapData').data('fillcolor'),
                    fillOpacity: 1,
                    strokeColor: '',
                    strokeWeight: 0
                },
                map_icon_label: '<span class="map-icon map-icon-' + $('#MapData').data('icontype').replace(/_/g, '-') + '"></span>'
            });
            //  modalMap.setZoom(16);
            // modalMap.setCenter(myLatlng);

            google.maps.event.addListener(modalMapLoad, 'click', function (event) {
                placeMarker(event.latLng);
                // geocodeLatLng(geocoder, modalMap, modalInfowindow, event, modalMarker);
                document.getElementById("Latitude").value = event.latLng.lat();
                document.getElementById("Longitude").value = event.latLng.lng();
                //  getAddress(event.latLng);
            });

        }
    });

    function placeMarker(location) {

        if (modalMarker) {
            modalMarker.setPosition(location);
        } else {
            modalMarker = new Marker({
                map: modalMapLoad,
                position: modalLatLng,
                icon: {
                    path: $('#MapData').data('iconmarker'),
                    fillColor: $('#MapData').data('fillcolor'),
                    fillOpacity: 1,
                    strokeColor: '',
                    strokeWeight: 0
                },
                map_icon_label: '<span class="map-icon map-icon-' + $('#MapData').data('icontype').replace(/_/g, '-') + '"></span>'
            });
            modalMapLoad.setZoom(16);
        }
    }

    function getAddress(latLng) {
        geocoder.geocode({ 'latLng': latLng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK && results[0]) {
                document.getElementById("Address").value = results[0].formatted_address.split(',')[0];
            }
            else document.getElementById("Address").value = null;
        });
    }


    function geocodeLatLng(geocoder, modalMap, modalInfowindow, event, modalMarker) {
        geocoder.geocode({
            'location': event.latLng
        }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    //modalMap.setZoom(11);
                    modalInfowindow.setContent(results[1].formatted_address);
                    modalInfowindow.open(modalMapLoad, modalMarker);
                } else {
                    window.alert('No results found');
                }
            } else {
                window.alert('Geocoder failed due to: ' + status);
            }
        });
    }
});