import { isNullOrUndefined } from 'util';
const hyphen: string = '-';
const monthNames = ["января", "февраля", "марта", "апреля", "мая", "июня",
    "июля", "августа", "сентября", "октября", "ноября", "декабря"
];

function splitDate(date: Date) {
    const yyyy = date.getFullYear().toString();
    const mm = (date.getMonth() + 1).toString();
    const day = date.getDate().toString();
    const h = date.getHours().toString();
    const m = date.getMinutes().toString();
    const s = date.getSeconds().toString();
    return { yyyy, mm, day, h, m, s };
}

function padTwo(val: string): string {
    return val.length < 2 ? "0" + val : val;
}

export function dateToFormatYmd(date: Date | string | undefined): string {
    if (isNullOrUndefined(date)) { return ''; }

    const d = new Date(date);
    let { yyyy, mm, day } = splitDate(d);
    return `${yyyy}${hyphen}${padTwo(mm)}${hyphen}${padTwo(day)}`;
}

export function dateToFormatDmy(date: Date | string | undefined): string {
    if (isNullOrUndefined(date)) { return ''; }

    const d = new Date(date);
    let { yyyy, mm, day } = splitDate(d);
    return `${padTwo(day)}${hyphen}${padTwo(mm)}${hyphen}${yyyy}`;
}

export function dateYmdToFormatJson(date: string | undefined): string | undefined {
    if (isNullOrUndefined(date)) { return undefined; }

    const d = createDate(date);
    if (isNullOrUndefined(d)) { return undefined; }

    const { yyyy, mm, day } = splitDate(d);
    return `${yyyy}${hyphen}${padTwo(mm)}${hyphen}${padTwo(day)}T00:00:00`;
}

export function createDate(rawDateOnly: string | undefined): Date | undefined {
    if (!isNullOrUndefined(rawDateOnly)) {
        let dt = rawDateOnly.split('T');
        let parts = dt[0].split(hyphen);
        if (parts.length >= 3) {
            const yyyy = Number(parts[0]);
            const mm = Number(parts[1]) - 1;
            const dd = Number(parts[2]);
            const newDate = new Date(yyyy, mm, dd);
            newDate.setFullYear(yyyy); // нужно чтоб работать с годом менее чем 100 лет от н.э.
            newDate.setHours(0, 0, 0, 0);
            return newDate;
        }
    }
    return undefined
}

export function addYears(date: Date, years: number): Date {
    date.setFullYear(date.getFullYear() + years);
    return date;
}

export function addMonths(date: Date, months: number): Date {
    date.setMonth(date.getMonth() + months);
    return date;
}

export function getMaskedDate(date: Date | string | undefined): string {
    if (isNullOrUndefined(date)) { return ''; }

    const d = new Date(date);
    const mm = monthNames[d.getMonth()];
    const day = d.getDate().toString();

    return `${day} ${padTwo(mm)}`;
}