import { IValidation, ValidationResult } from "./Validation";

const requiredFieldText: string = 'Не может быть пусто';
const cannotContainOnlySpacesFieldText: string = 'Не может содержать одни пробелы';

export class IsRequiredValidation implements IValidation {
    validation(val?: string): ValidationResult {
        let isValid = false;
        let errorText = '';
        if (val) {
            isValid = true;
            const spaces = val.trim();
            if (spaces === '') {
                isValid = false;
                errorText = cannotContainOnlySpacesFieldText;
            }
        }
        else {
            errorText = requiredFieldText;
        }
        return new ValidationResult(!isValid, errorText);
    }
}