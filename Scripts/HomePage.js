require.config({
    paths: {
        jquery: "jquery-1.10.2",
        bootstrap: "bootstrap",
        google: "https://maps.googleapis.com/maps/api/js?key=AIzaSyDFRB5faj9FozFL-YUA513d_WaeVyLxDSg&v=3.exp",
        mapicons: "map-icons",
        map: "Map",
        modalmap: "ModalMap",
        jscolor: "jscolor",
        updatemarkers: 'UpdateMarkers'
    },
    shim: {
        'bootstrap': ['jquery']
    }
});

require(['map', 'modalmap', 'bootstrap', 'jscolor', 'updatemarkers'], function () {
    $('#feed').on('click', '.event-status-button', function () {
        var eventStatus = { EventStatus: $(this).data('type'), CardEventID: $(this).parent().data('eventid') };
        var postData = JSON.stringify(eventStatus);
        $.ajax({
            url: '/Event/EventStatus',
            context: this,
            data: postData,
            success: function (returnData) {
                if (returnData) {
                    $(this).parent().children('.event-status-selected').removeClass('event-status-selected');
                    $(this).addClass('event-status-selected');
                }
            },
            type: 'POST',
            contentType: 'application/json, charset=utf-8'
        });
    });
});