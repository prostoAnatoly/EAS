import { createDate, dateToFormatYmd } from "../../utils/date-utils";
import { IValidation, ValidationResult } from "./Validation";

export class MinDateValidation implements IValidation {
    constructor(public minDate: Date) {

    }

    validation(val?: string): ValidationResult {
        let isValid = true;
        let errorText = '';

        if (val) {
            const curDate = createDate(val);
            if (curDate && curDate < this.minDate) {
                isValid = false;
                errorText = `Дата должна быть больше или равна ${dateToFormatYmd(this.minDate)}`;
            }
        }
        return new ValidationResult(!isValid, errorText);
    }

    public static getMinDate(): MinDateValidation {
        return new MinDateValidation(new Date(1901, 0, 1));
    }
}