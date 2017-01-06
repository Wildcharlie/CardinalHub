require.config({
    paths: {
        jquery: "jquery-1.10.2",
        bootstrap: "bootstrap",
        validate: "jquery.validate",
        validateunobtrusive: "jquery.validate.unobtrusive"
    },
    shim: {
        'bootstrap': ['jquery'],
        'validate': ['jquery'],
        'validateunobtrusive': ['jquery', 'validate']
    }
});

require(['bootstrap', 'jquery', 'validate', 'validateunobtrusive'], function () {
});