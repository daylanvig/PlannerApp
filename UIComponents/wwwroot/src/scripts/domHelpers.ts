
window.customScripts.DOM = {
    /**
     * Scroll to element
     * @param cssSelector
     */
    scrollIntoView: function scrollIntoView(cssSelector: string): void {
        const element = document.querySelector(cssSelector);
        if (element == null) {
            throw new Error('Element not found');
        }
        element.scrollIntoView({ behavior: 'smooth' });
    },
    getBoundingClientRect: function getBounds(element: HTMLElement): DOMRect {
        return element.getBoundingClientRect();
    }
};


