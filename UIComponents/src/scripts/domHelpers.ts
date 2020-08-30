export class DOMHelpers {
    /**
     * Scroll to element
     * @param cssSelector
     */
    scrollIntoView(cssSelector: string): void {
        const element = document.querySelector(cssSelector);
        if (element == null) {
            throw new Error('Element not found');
        }
        element.scrollIntoView({ behavior: 'smooth' });
    }

    getBoundingClientRect(element: HTMLElement): DOMRect {
        return element.getBoundingClientRect();
    }
};


