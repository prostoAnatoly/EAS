import { IValidation, ValidationResult } from "./Validation";

export class MaxValueValidation implements IValidation {
    constructor(public maxValue: number) {

    }

    validation(val?: string): ValidationResult {
        let isValid = true;
        let errorText = '';

        if (val) {
            const curValue = Number(val);
            if (curValue > this.maxValue) {
                isValid = false;
                errorText = `Значение должно быть меньше или равно ${this.maxValue}`;
            }
        }
        return new ValidationResult(!isValid, errorText);
    }

}