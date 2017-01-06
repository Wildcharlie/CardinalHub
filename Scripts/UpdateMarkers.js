define([
    'mapicons'
], function () {

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
});