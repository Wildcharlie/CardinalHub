require.config({
    paths: {
        jquery: "jquery-1.10.2",
        bootstrap: "bootstrap",
        google: "https://maps.googleapis.com/maps/api/js?key=AIzaSyDFRB5faj9FozFL-YUA513d_WaeVyLxDSg&v=3.exp",
        eventmap: "EventMap",
        editeventmodal: "EditEventModal",
        mapicons: 'map-icons',
        jscolor: "jscolor",
        updatemarkers: 'UpdateMarkers'
    },
    shim: {
        'bootstrap': ['jquery']
    }
});

require(['eventmap', 'editeventmodal','bootstrap', 'jscolor'], function () {
    $('#EditEvent').click(function () {
        $('#EventStartDateTime').val($('#EventDate').val() + ' ' + $('#EventStartTime').val());
        if ($('#EventEndTime').val().length > 0) {
            $('#EventStartEndTime').val($('#EventDate').val() + ' ' + $('#EventEndTime').val());
        }

        var form = $("#cardEventForm");
        var url = form.attr("action");
        var formData = form.serialize();
        $.post(url, formData, function (data) {
            if (data) {
                location.reload();
            } else {
                $('#msgEvent').html('<div class="alert alert-dismissible alert-danger"><button type="button" class="close" data-dismiss="alert">×</button>\
                                    <span>Failed to edit Event! Make sure all forms are filled out.</span></div>');
            }
        });
    });

    $('#EventStatusButtons').on('click', '.event-status-button', function () {
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