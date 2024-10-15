import { Control } from './controls/Control';

export class FormDef {

    public validation(): boolean {
        let counter = 0;
        let isValid = true;
        for (var prop in this) {
            const control = this[prop] as unknown as Control;
            if (control) {
                const v = control.validation();
                if (!v) {
                    if (counter === 0) {
                        control.focus();
                    }
                    isValid = false;
                    counter++;
                }
            }
        }
        return isValid;
    }

    public setDisabled(disabled: boolean): void {
        for (var prop in this) {
            const control = this[prop] as unknown as Control;
            if (control) {
                control.props.disabled = disabled || control.props.allwaysDisabled;
            }
        }
    }

    public isChanged(): boolean {
        for (var prop in this) {
            const control = this[prop] as unknown as Control;
            if (control) {
                if (control.isChanged()) {
                    return true;
                }
            }
        }
        return false;
    }
}