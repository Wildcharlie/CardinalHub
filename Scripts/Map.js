define([
    'jquery',
    'google',
    'mapicons'
], function () {

    localEvents = [];
    hostnameRegexp = new RegExp('^https?://.+?/');

    function initMap() {
        var myLatlng = new google.maps.LatLng(38.2157472, -85.7590743);
        var myOptions = {
            zoom: 17,
            center: myLatlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        map = new google.maps.Map(document.getElementById('map'), myOptions);

        google.maps.event.addListener(map, 'tilesloaded', tilesLoaded);
    }

    function tilesLoaded() {
        search();
        google.maps.event.clearListeners(map, 'tilesloaded');
        google.maps.event.addListener(map, 'zoom_changed', search);
        google.maps.event.addListener(map, 'dragend', search);
    }

    function search() {
        // clear table with event results.
        clearResults();
        // clear markers on map
        clearEventMarkers();

        var actionUrl = GetActionURL();
        var bounds = map.getBounds();
        var neCoords = bounds.getNorthEast();
        var swCoords = bounds.getSouthWest();

        // get action url from view and pass bounds of map.
        actionUrl += ('?neLat=' + neCoords.lat() + '&neLong=' + neCoords.lng() + '&swLat=' + swCoords.lat() + '&swLong=' + swCoords.lng());

        // var srchtype = parseInt(document.getElementById('SearchType').value);
        //var searchterm = document.getElementById('keyword').value;
        // actionUrl += '&srchType=' + srchtype;
        // actionUrl += '&srchTerm=' + searchterm;

        // Async to server and get favs in the bounds.
        $.getJSON(actionUrl, displayCardEvents);
    }

    function displayCardEvents(response) {

        if (response != null) {
            var tempString = "";

            for (var i = 0; i < response.length; i++) {
                var mylatlng = { lat: response[i].GeoLat, lng: response[i].GeoLong };
                var iconName;
                if (response[i].IconName) {
                    iconName = response[i].IconName;
                }
                else {
                    iconName = 'map-icon-school';
                }

                var marktype = response[i].markertype;
                if (!marktype) {
                    marktype = MAP_PIN;
                }
                else {
                    marktype = eval(response[i].markertype);
                }

                var markcol = response[i].markercolor;
                if (!markcol) {
                    markcol = '#00AACC';
                }

                marker = new Marker({
                    map: map,
                    position: mylatlng,
                    icon: {
                        path: marktype,
                        fillColor: markcol,
                        fillOpacity: 1,
                        strokeColor: '',
                        strokeWeight: 0
                    },
                    map_icon_label: '<span class="map-icon map-icon-' + iconName + '"></span>'
                });

                localEvents.push(marker);
                google.maps.event.addListener(marker, 'click', getCardDetails(response[i], i));
                //window.setTimeout(dropMarker(i), i * 100);
                tempString += addCardResult(response[i], i);
            }
            $('#results').html(tempString);
        }
    }

    $('#results').on('click', '.event-list-row', function () {
        google.maps.event.trigger(localEvents[$(this).find('.map-icon').data('index')], 'click');

    });

    function clearEventMarkers() {

        for (var i = 0; i < localEvents.length; i++) {
            localEvents[i].setMap(null);
        }
        localEvents = [];
    }

    // Adds a result to the card table listing
    function addCardResult(result, i) {
        var tr = '<tr class="event-list-row"><td>' + result.PlaceName + '</td><td><span data-index="' + i + '" class="map-icon map-icon-' + result.IconName + '" style="height:30px;width:30px;font-size:xx-large;padding-left:5px; "></span><td></tr>'

        return tr;
    }

    function clearResults() {
        $('#results').html('<tr><td colspan="2" class="loading text-center"><i class="fa fa-spinner fa-spin fa-2x"></i></td><tr>');
    }

    function getCardDetails(result, i) {
        return function (place, status) {
            if (typeof(iw) != 'undefined') {
                iw.close();
                iw = null;
            }
            iw = new google.maps.InfoWindow({
                content: getCardIWContent(result)
            });
            iw.open(map, localEvents[i]);
        }
    }

    function getCardIWContent(place) {
        var content = '';
        //alert(place.IconName);
        content += '<table>';
        content += '<tr class="iw_table_row">';
        content += '<td colspan="2" style="text-align: left">'
                + '<span id="markerIcon" class="map-icon map-icon-' + place.IconName + '" style="height:30px;width:30px;font-size:xx-large;padding-left:5px; ">'
                + ' </span><b><a href="/Event/Details/' + place.id + '">'
                   + place.PlaceName
                   + '</a></b>&nbsp;';

        if (place.goingflag) {

            content += '<i style="color:gold;" class="fa fa-star fa-lg"> </i>';
        }
        content += '<br />' + place.rtype + '</td>';
        content += '</tr>';
        if (place.vicinity) {
            content += '<tr class="iw_table_row"><td class="iw_attribute_name">Address:</td><td>' + place.vicinity + '</td></tr>';
        }

        if (place.IWString) {
            content += '<tr ><td colspan="2" >' + ((place.IWString.length > 100) ? ($.trim(place.IWString).substring(0, 100) + "...") : place.IWString) + '</td></tr>';
        }

        if (place.phone) {
            content += '<tr class="iw_table_row"><td class="iw_attribute_name">Telephone:</td><td>' + place.phone + '</td></tr>';
        }
        // Add row here for favorites count and likes
        //

        content += '<tr class="iw_table_row"><td class="iw_attribute_name">Going:</td><td class="iw_attribute_val">' + place.goingcount + '</td></tr>';
        content += '<tr class="iw_table_row"><td class="iw_attribute_name">Maybe:</td><td class="iw_attribute_val">' + place.maybecount + '</td></tr>';
        content += '<tr class="iw_table_row"><td class="iw_attribute_name">Invited:</td><td class="iw_attribute_val">' + place.invitedcount + '</td></tr>';
        content += '</table>';
        return content;
    }

    initMap();
});