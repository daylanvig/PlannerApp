/**
 * Scroll to element
 * @param cssSelector
 */
function scrollIntoView(cssSelector: string): void {
    const element = document.querySelector(cssSelector);
    if (element == null) {
        throw new Error('Element not found');
    }
    element.scrollIntoView({ behavior: 'smooth' });
}


window.customScripts.scrollIntoView = scrollIntoView;