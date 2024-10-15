import { IValidation, ValidationResult } from "./Validation";

export class PhoneNumberValidation implements IValidation {
    private readonly lenPhone = 11;

    validation(val?: string): ValidationResult {
        let invalid = false;
        let errorText = '';
        if (val) {
            val = val.replace(/\D+/g, '');// Оставляем только цифры
            if (val.length !== this.lenPhone) {
                invalid = true;
                errorText = `Номер телефона должен содержать ${this.lenPhone} цифр`;
            }
        }
        return new ValidationResult(invalid, errorText)
    }
}