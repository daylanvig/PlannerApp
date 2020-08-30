"use strict";
window.customScripts.Date = {
    getTimeZoneOffset() {
        return new Date().getTimezoneOffset();
    }
};
window.customScripts.DOM = {
    scrollIntoView: function scrollIntoView(cssSelector) {
        const element = document.querySelector(cssSelector);
        if (element == null) {
            throw new Error('Element not found');
        }
        element.scrollIntoView({ behavior: 'smooth' });
    },
    getBoundingClientRect: function getBounds(element) {
        return element.getBoundingClientRect();
    }
};
//# sourceMappingURL=index.js.map