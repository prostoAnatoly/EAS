import { isNullOrUndefined } from 'util';

export function isNullOrUndefinedOrEmpty(value: string | undefined): boolean {
    return isNullOrUndefined(value) || value == '' || value.trim() == '';
}