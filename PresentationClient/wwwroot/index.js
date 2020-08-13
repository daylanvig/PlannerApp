"use strict";
const domHelpers = {
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
Object.assign(window.customScripts, domHelpers);
//# sourceMappingURL=index.js.map