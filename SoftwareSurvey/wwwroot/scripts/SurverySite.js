window.SurveySite = window.SurveySite || {};

window.SurveySite.focusElementById = function (id) {
    try {
        var element = document.getElementById(id);
        if (element) {
            element.focus();
        }
    }
    catch (err) {
        appInsights.trackException(err);
    }
};

window.SurveySite.focusElementByName = function (name) {
    try {
        var element = document.getElementsByName(name);
        if (element) {
            element[0].focus();
        }
    }
    catch (err) {
        appInsights.trackException(err);
    }
};