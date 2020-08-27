window.SurveySite = window.SurveySite || {};

window.SurveySite.focusElementById = function (id) {
    var element = document.getElementById(id);
    if (element) {
        element.focus();
    }
};

window.SurveySite.focusElementByName = function (name) {
    var element = document.getElementsByName(name);
    if (element) {
        element[0].focus();
    }
};