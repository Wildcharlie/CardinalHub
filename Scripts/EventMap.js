define([
    'jquery',
    'google',
    'mapicons'
], function () {
    function initMap() {
        var infowindow = new google.maps.InfoWindow;
        var myLatlng = new google.maps.LatLng(38.2157472, -85.7590743);
        var myOptions = {
            zoom: 17,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        var myLatlng = { lat: parseFloat($('#MapData').data('lat')), lng: parseFloat($('#MapData').data('long')) };
        map = new google.maps.Map(document.getElementById('map'), myOptions);

        marker = new Marker({
            map: map,
            position: myLatlng,
            icon: {
                path: marker_options[$('#MapData').data('iconmarker')],
                fillColor: $('#MapData').data('fillcolor'),
                fillOpacity: 1,
                strokeColor: '',
                strokeWeight: 0
            },
            map_icon_label: '<span class="map-icon map-icon-' + $('#MapData').data('icontype').replace(/_/g, '-') + '"></span>'
        });
        map.setZoom(16);
        map.setCenter(myLatlng);
        map.panBy(0, -45);
        infowindow.setContent($('#MapData').data('eventname'));
        infowindow.open(map, marker);
    }

   initMap();
});