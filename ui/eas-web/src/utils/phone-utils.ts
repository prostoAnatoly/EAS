
export function getMaskedPhoneNumber(value: string | undefined): string {
    if (value && value.length === 11) {
        return `+${value.substring(0, 1)} ${value.substring(1, 4)} ${value.substring(4, 7)} ${value.substring(7, 9)} ${value.substring(9, 11)}`;
    }
    if (value) {
        return value;
    }
    return '';
}