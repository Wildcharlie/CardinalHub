require.config({
    paths: {
        jquery: "jquery-1.10.2",
        google: "https://maps.googleapis.com/maps/api/js?key=AIzaSyDFRB5faj9FozFL-YUA513d_WaeVyLxDSg&v=3.exp",
        jscolor: "jscolor",
        mapicons: 'map-icons'
    }
});

require(['google', 'jscolor', 'jquery', 'mapicons'], function () {
    var svgNS = "http://www.w3.org/2000/svg";
    var infowindow = new google.maps.InfoWindow;

    $('#btnAddPoint').click(function () {

        $.ajaxSetup({ cache: false })
        var url = "/CampusMap/GetCurrentDate";
        $.get(url, null, function (data) {
            $("#rData").html(data);
        });
    })


    $('#Save').click(function () {

        var form = $("#eventForm");
        var url = form.attr("action");
        var formData = form.serialize();
        $.post(url, formData, function (data) {
            $("#msg").html(data);
        });
    })

    window.update = function (jscolor) {
        // 'jscolor' instance can be used as a string
        document.getElementById('fillColor').value = ('#' + jscolor);
        document.getElementById('iconPath').setAttribute('fill', '#' + jscolor);
    }

    window.updateMarker = function () {
        var index = document.getElementById('IconMarker').value;
        var svg;

        switch (index) {
            case '0':
                svg = "M24 0c-9.8 0-17.7 7.8-17.7 17.4 0 15.5 17.7 30.6 17.7 30.6s17.7-15.4 17.7-30.6c0-9.6-7.9-17.4-17.7-17.4z";
                break;
            case '1':
                svg = "M45.5 0h-43v43h16.2l5.9 5 5.8-5h15.1z";
                break;
            case '2':
                svg = "M42.7 16.2c.3-3.4 1.3-6.6 3.1-9.5l-7-6.7c-2.2 1.8-4.7 2.8-7.6 3-2.6.2-5.1-.2-7.4-1.4-2.3 1.1-4.8 1.6-7.4 1.4-2.7-.2-5.1-1.1-7.2-2.7l-6.9 6.7c1.7 2.9 2.7 6 2.9 9.2.1 1.5-.3 3.5-1.3 6.1-.5 1.5-.9 2.7-1.2 3.8-.2 1-.4 1.9-.4 2.5 0 2.8.8 5.3 2.4 7.5 1.3 1.6 3.5 3.4 6.4 5.4 3.3 1.6 5.8 2.6 7.6 3.1.5.2 1 .4 1.5.7l1.5.6c1.1.6 1.9 1.3 2.3 2.1.5-.8 1.3-1.5 2.4-2.1.7-.3 1.4-.6 1.9-.8.5-.2.9-.4 1.1-.5.4-.2.9-.4 1.5-.6.6-.2 1.4-.5 2.2-.8 1.7-.6 3-1.1 3.8-1.6 2.9-2 5-3.8 6.4-5.3 1.7-2.2 2.6-4.8 2.5-7.6-.1-1.3-.7-3.3-1.7-6.1-1.1-2.8-1.6-4.9-1.4-6.4z";

                break;
            case '3':
                svg = "M48 19.7c-.3-13.2-7.9-18.5-8.3-18.7l-1.2-.8-1.2.8c-2 1.4-4.1 2-6.1 2-3.4 0-5.8-1.9-5.9-1.9l-1.3-1.1-1.3 1.1c-.1.1-2.5 1.9-5.9 1.9-2.1 0-4.1-.7-6.1-2l-1.3-.8-1.2.8c-.8.5-7.9 5.8-8.2 18.7-.1 1.1 3 22.2 24 28.3 22.9-6.7 24.1-26.9 24-28.3z";

                break;
            case '4':
                svg = "M0 0h48v48h-48z";
                break;
            case '5':
                svg = "M48 40c0 4.4-3.6 8-8 8h-32c-4.4 0-8-3.6-8-8v-32c0-4.4 3.6-8 8-8h32c4.4 0 8 3.6 8 8v32z";
                break;
        }

        document.getElementById('iconPath').setAttribute('d', svg);
    }

    window.updateIcon = function () {
        var cboIcon = document.getElementById('IconType');
        var icon = cboIcon.options[cboIcon.selectedIndex].text;
        //alert(icon);
        icon = icon.replace(/_/g, "-");

        document.getElementById('markerIcon').className = 'map-icon map-icon-' + icon;
    }

    function initMap() {

        var belnap = { lat: 38.2157472, lng: -85.7590743 }
        var bounds = new google.maps.LatLngBounds();
        var hasPoints = false;

        initPage();

        map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: 38.2157472, lng: -85.7590743 },
            zoom: 17
        });

        marker = new Marker({
            map: map,
            position: new google.maps.LatLng(38.2157, -85.759),
            icon: {
                path: MAP_PIN,
                fillColor: '#00CCBB',
                fillOpacity: 1,
                strokeColor: '',
                strokeWeight: 0
            },
            map_icon_label: '<span class="map-icon map-icon-postal-code"></span>'
        });

        if (hasPoints) {
            map.fitBounds(bounds);
        }

        //map.addListener('click', function(e) {
        //    placeMarkerAndPanTo(e.latLng, map);
        //});

        google.maps.event.addListener(map, 'click', function (event) {
            placeMarker(event.latLng);
            //geocodeLatLng(geocoder, map, infowindow, event, marker);
            document.getElementById("Latitude").value = event.latLng.lat();
            document.getElementById("Longitude").value = event.latLng.lng();
        });
    }

    function initPage() {
        document.getElementById('fillColor').value = '#FFFFFF';
        // alert(document.getElementById('fillColor').value);
    }

    function placeMarkerAndPanTo(latLng, map) {
        var marker = new google.maps.Marker({
            position: latLng,
            map: map
        });
        map.panTo(latLng);
    }

    function bindInfoWindow(marker, map, infowindow, html) {
        marker.addListener('click', function () {
            infowindow.setContent(html);
            infowindow.open(map, this);
        });

    }

    function placeMarker(location) {
        if (marker) {
            marker.setPosition(location);
        } else {

            marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true,
                animation: google.maps.Animation.BOUNCE,
                //  icon: icon
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

    initMap();
});