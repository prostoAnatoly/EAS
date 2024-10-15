import { IValidation, ValidationResult } from "./Validation";

export class EmailValidation implements IValidation {

    private static readonly MAIL_FORMAT = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;

    validation(val?: string): ValidationResult {
        let invalid = true;
        let errorText = 'Адрес электронной почты некорректный';
        if (!val || val.match(EmailValidation.MAIL_FORMAT)) {
            invalid = false;
            errorText = '';
        }
        return new ValidationResult(invalid, errorText)
    }
}