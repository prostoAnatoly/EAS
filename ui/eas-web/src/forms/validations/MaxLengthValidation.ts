import { IValidation, ValidationResult } from "./Validation";

export class MaxLengthValidation implements IValidation {
    constructor(public maxLength: number) {

    }

    validation(val?: string): ValidationResult {
        let isValid = true;
        let errorText = '';
        if (val) {
            const len = val.length;
            if (len > this.maxLength) {
                isValid = false;
                errorText = `Превышена максимальная длина. Ожидалось ${this.maxLength}`;
            }
        }
        return new ValidationResult(!isValid, errorText);
    }
}