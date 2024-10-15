import { IValidation, ValidationResult } from "./Validation";

export class MinLengthValidation implements IValidation {
    constructor(public minLength: number) {

    }

    validation(val?: string): ValidationResult {
        let isValid = true;
        let errorText = '';
        if (val) {
            const len = val.length;
            if (len < this.minLength) {
                isValid = false;
                errorText = `Длина должна быть не менее ${this.minLength}`;
            }
        }
        return new ValidationResult(!isValid, errorText);
    }
}