export class DOMHelpers {
    scrollIntoView(cssSelector) {
        const element = document.querySelector(cssSelector);
        if (element == null) {
            throw new Error('Element not found');
        }
        element.scrollIntoView({ behavior: 'smooth' });
    }
    getBoundingClientRect(element) {
        return element.getBoundingClientRect();
    }
}
;
//# sourceMappingURL=domHelpers.js.map