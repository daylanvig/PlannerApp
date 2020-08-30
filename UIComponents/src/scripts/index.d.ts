import { DOMHelpers } from './domHelpers';
import { DateTimeHelper } from './dateTimeHelpers';

declare interface ICustomScripts {
    DOM: DOMHelpers;
    Date: DateTimeHelper;
}

declare var UIComponents: ICustomScripts;