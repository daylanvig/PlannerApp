import { DateTimeHelper } from "./dateTimeHelpers";
import { DOMHelpers } from './domHelpers';
// @ts-ignore
window.UIComponents = {
    DOM: new DOMHelpers(),
    Date: new DateTimeHelper(),
}