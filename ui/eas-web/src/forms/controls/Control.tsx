import * as React from 'react';

import { IValidation, MaxLengthValidation, IsRequiredValidation, ValidationResult, MinDateValidation, MaxDateValidation } from '../Forms';

import { dateToFormatYmd } from '../../utils/date-utils';
import { useStateSmart } from '../../utils/hooks';
import { RefObjectExtensions } from '../extensions/RefObjectExtensions';

export class ControlProps {
    public validations: IValidation[] = [];
    public title: string = '';
    public onKeyPress?: React.KeyboardEventHandler<HTMLDivElement> = undefined;
    public disabled?: boolean = false;
    public initialValue?: string = undefined;
    public onChangeValue?: () => void = undefined;
    public allwaysDisabled?: boolean = false;
    public isAutofocus?: boolean = false;
}

class ControlState {
    constructor(public validation: ValidationResult) {
    }
}

export abstract class Control {

    private readonly emptyValidationResult = new ValidationResult(false, '');

    private readonly state: [ControlState, (newState: ControlState | ((prev: ControlState) => ControlState)) => void];
    protected inputRef: React.RefObject<HTMLInputElement>;
    private readonly initialValueRef: React.MutableRefObject<string | undefined>;
    private readonly isSetValueRef = React.useRef(false);

    constructor(public props: ControlProps) {
        this.inputRef = React.useRef<HTMLInputElement>(null);

        this.state = useStateSmart(new ControlState(this.emptyValidationResult));
        this.initialValueRef = React.useRef(props.initialValue);

        this.onChangeValue = this.onChangeValue.bind(this);      
    }

    protected processNewValue(val: string): string {
        return val;
    }

    public getValue(): string {
        if (this.isSetValueRef.current || !this.initialValueRef.current) {
            return RefObjectExtensions.getValue(this.inputRef);
        }
        return this.initialValueRef.current;
    }

    public setNewInitialValue(val?: string): void {
        const v = this.setValueOnly(val);
        this.initialValueRef.current = v;
    }

    public setValue(val?: string): void {
        const v = this.setValueOnly(val);

        const validResult = this.getValidResult(v);

        this.initialValueRef.current = v;

        this.isSetValueRef.current = true;

        this.state[1](prev => ({ ...prev, validation: validResult }));
    }

    public isChanged(): boolean {
        return (this.initialValueRef.current || '') !== this.getValue();
    }

    protected setValueInner(val?: string): void {
        const v = this.setValueOnly(val);

        const validResult = this.getValidResult(v);

        this.isSetValueRef.current = true;

        this.state[1](prev => ({ ...prev, validation: validResult }));
    }

    private setValueOnly(val?: string): string {
        val = val ?? '';
        const v = this.processNewValue(val);

        RefObjectExtensions.setValue(this.inputRef, v);

        return v;
    }

    protected onChangeValue(e): void {
        let v = e.target.value ?? '';

        v = this.processNewValue(v);

        const validResult = this.getValidResult(v);

        this.state[1](prev => ({ ...prev, validation: validResult }));

        if (this.props.onChangeValue) {
            this.props.onChangeValue();
        }
    };

    private getValidResult(val: string): ValidationResult {
        let validationResult: ValidationResult = this.emptyValidationResult;

        if (this.props.validations.length !== 0) {
            for (let item of this.props.validations) {
                validationResult = item.validation(val);
                if (validationResult.getInvalid()) {
                    break;
                }
            }
        }
        return validationResult;
    }

    /**
     * Представление контрола на форме
     */
    public abstract view();

    /**
     * true - ошибок нет
     * false - есть ошибки
     */
    public validation(): boolean {
        const v = this.getValue();

        const validResult = this.getValidResult(v);

        this.state[1](prev => ({ ...prev, validation: validResult }));

        return !validResult.getInvalid();
    }

    /**
     * true - ошибок нет
     * false - есть ошибки
     */
    public isValid(): boolean {
        const v = this.getValue();

        const validResult = this.getValidResult(v);

        return !validResult.getInvalid();
    }

    public focus() {
        if (this.inputRef && this.inputRef.current) {
            this.inputRef.current.focus();
        }
    }

    protected isRequired(): boolean {
        return this.props.validations.some(x => x instanceof IsRequiredValidation);
    }

    protected getStyleField() {
        return {
            margin: (theme) => theme.spacing(2, 1, 1).toString() + ' !important',
        };
    }

    protected getInputPropsBase(): {} {
        const inputProps = {
        };
        if (this.props.validations) {
            this.setMaxLength(inputProps);
            this.setMaxDate(inputProps);
            this.setMinDate(inputProps);
        }
        return inputProps;
    }

    protected getValidationInfo(): ValidationResult {
        return this.state[0].validation;
    }

    private setMaxLength(inputProps: {}): void {
        const maxLengthValidation = this.props.validations.find(x => x instanceof MaxLengthValidation) as MaxLengthValidation
        if (maxLengthValidation) {
            inputProps['maxLength'] = maxLengthValidation.maxLength;
        }
    }

    private setMaxDate(inputProps: {}): void {
        const maxValidation = this.props.validations.find(x => x instanceof MaxDateValidation) as MaxDateValidation
        if (maxValidation) {
            inputProps['max'] = dateToFormatYmd(maxValidation.maxDate);
        }
    }

    private setMinDate(inputProps: {}): void {
        const mimValidation = this.props.validations.find(x => x instanceof MinDateValidation) as MinDateValidation
        if (mimValidation) {
            inputProps['min'] = dateToFormatYmd(mimValidation.minDate);
        }
    }

}