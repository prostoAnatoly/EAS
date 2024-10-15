import { IValidation, ValidationResult } from "./Validation";

export class MinValueValidation implements IValidation {
    constructor(public minValue: number) {

    }

    validation(val?: string): ValidationResult {
        let isValid = true;
        let errorText = '';

        if (val) {
            const curValue = Number(val);
            if (curValue < this.minValue) {
                isValid = false;
                errorText = `Значение должно быть больше или равно ${this.minValue}`;
            }
        }
        return new ValidationResult(!isValid, errorText);
    }

}