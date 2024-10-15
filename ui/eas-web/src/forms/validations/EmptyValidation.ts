import { IValidation, ValidationResult } from "./Validation";

export class EmptyValidation implements IValidation {
    validation(val?: string): ValidationResult {
        return new ValidationResult(false, '');
    }
}