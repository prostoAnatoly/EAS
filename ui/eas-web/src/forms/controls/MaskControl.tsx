import { TextField } from '@mui/material';
import * as React from 'react';

import { ControlProps, Control } from '../Forms';
import InputMask from 'react-input-mask';

export class MaskControlProps extends ControlProps {

}

export abstract class MaskControl extends Control {

    constructor(public props: MaskControlProps,
        protected mask: string, protected maskChar: string) {

        super(props);
    }

    protected abstract clean(val: string): string;

    protected override processNewValue(val: string): string {
        return this.clean(val);
    }

    public override getValue() {
        return this.clean(super.getValue());
    }

    public view() {

        const isRequired = this.isRequired();
        const inputProps = this.getInputPropsBase();

        const field = this.getStyleField();

        return (<InputMask mask={this.mask}
            maskChar={this.maskChar}
            disabled={this.props.disabled}
            value={this.getValue()}
            onChange={this.onChangeValue}
        >
            {() => <TextField
                inputRef={this.inputRef}
                sx={field}
                disabled={this.props.disabled}
                error={this.getValidationInfo().getInvalid()}
                helperText={this.getValidationInfo().getErrorText()}
                margin="normal"
                fullWidth
                label={this.props.title}
                type="text"
                required={isRequired}
                variant="outlined"
                autoFocus={this.props.isAutofocus}
                InputLabelProps={{
                    shrink: true,
                }}
                inputProps={inputProps}
            />}
        </InputMask>);
    }
}