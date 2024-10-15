import { FullName } from "../sharedModels/FullName";

export function getFullname(fullName: FullName | undefined): string {
    if (fullName) {
        const surname = fullName.surname ?? '';
        const name = fullName.name ?? '';

        if (fullName.patronymic) {
            return `${surname} ${name} ${fullName.patronymic}`;
        }
        return `${surname} ${name}`;
    }
    return '';
}

export function getFullnameShort(fullName: FullName | undefined): string {
    if (fullName && fullName.surname) {
        const surname = fullName.surname;
        let initialName = '';
        if (fullName.name && fullName.name.length > 1) {
            initialName = fullName.name.charAt(0).toUpperCase();
        }

        if (fullName.patronymic && fullName.patronymic.length > 1) {
            const initialPatronymic = fullName.patronymic.charAt(0).toUpperCase();

            return `${surname} ${initialName}. ${initialPatronymic}.`;
        }
        return `${surname} ${initialName}.`;
    }
    return '';
}

export function getSurNameAndNameShort(fullName: FullName | undefined): string | undefined {
    if (fullName && fullName.surname && fullName.surname.length > 0) {
        let short = fullName.surname.charAt(0);
        if (fullName.name && fullName.name.length > 0) {
            short += fullName.name.charAt(0);
        }
        short = short.toUpperCase();

        return short;
    }

    return undefined;
}