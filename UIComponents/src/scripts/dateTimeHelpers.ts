export class DateTimeHelper {
    getTimeZoneOffset(): number {
        return new Date().getTimezoneOffset();
    }
}