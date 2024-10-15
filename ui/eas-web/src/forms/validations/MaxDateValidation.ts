import { createDate, dateToFormatYmd } from "../../utils/date-utils";
import { IValidation, ValidationResult } from "./Validation";

export class MaxDateValidation implements IValidation {
    constructor(public maxDate: Date) {

    }

    validation(val?: string): ValidationResult {
        let isValid = true;
        let errorText = '';

        if (val) {
            const curDate = createDate(val);
            if (curDate && curDate > this.maxDate) {
                isValid = false;
                errorText = `Дата должна быть меньше или равна ${dateToFormatYmd(this.maxDate)}`;
            }
        }
        return new ValidationResult(!isValid, errorText);
    }

}