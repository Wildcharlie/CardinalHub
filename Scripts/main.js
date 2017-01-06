require.config({
    paths: {
        jquery: "jquery-1.10.2",
        jqueryValidate: "../jquery.validate",
        jqueryValidateUnobtrusive: "../jquery.validate.unobtrusive",
        bootstrap: "../bootstrap",
    },
    shim: {
        jqueryValidate: ["jquery"],
        jqueryValidateUnobtrusive: ["jquery", "jqueryValidate"]
    }
});