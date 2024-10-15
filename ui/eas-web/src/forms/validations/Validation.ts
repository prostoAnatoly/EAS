
export interface IValidation {
    validation(val?: string): ValidationResult;
}

export class ValidationResult {

    constructor(private invalid: boolean,
        private errorText: string) {
    }

    public getErrorText() {
        return this.invalid ? this.errorText : '';
    }

    public getInvalid() {
        return this.invalid;
    }
}