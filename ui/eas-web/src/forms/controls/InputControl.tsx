import * as React from 'react';

import { Control, ControlProps } from '../Forms';

import { TextField } from '@mui/material';

export class InputControlProps extends ControlProps {

}

export class InputControl extends Control {

    constructor(public props: InputControlProps) {
        super(props);
    }

    public view() {
        const isRequired = this.isRequired();
        const inputProps = this.getInputPropsBase();

        const field = this.getStyleField();

        return (<TextField sx={field}
            inputRef={this.inputRef}
            onChange={this.onChangeValue}
            value={this.getValue()}
            onKeyPress={this.props.onKeyPress}
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
        />);
    }

}