import * as React from 'react';

import { Control, ControlProps } from '../Forms';

import { IconButton, InputAdornment, TextField } from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { useStateSmart } from '../../utils/hooks';

export class PasswordControlProps extends ControlProps {

    public autocomplete?: string = 'current-password';
}

export class PasswordControl extends Control {

    private readonly statePassword: [boolean, (newState: boolean | ((prev: boolean) => boolean)) => void];

    constructor(public props: PasswordControlProps) {
        super(props);
        this.statePassword = useStateSmart<boolean>(false);
    }

    public override view() {

        const isRequired = this.isRequired();

        const inputProps = this.getInputPropsBase();

        const field = this.getStyleField();

        const handleClickShowPassword = () => {
            this.statePassword[1](prev => !prev);
        };

        const handleMouseDownPassword = (event) => {
            event.preventDefault();
        };

        const showPassword = this.statePassword[0];

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
            type={showPassword ? 'text' : 'password'}
            required={isRequired}
            variant="outlined"
            autoFocus={this.props.isAutofocus}
            autoComplete={this.props.autocomplete}
            InputLabelProps={{
                shrink: true,
            }}
            inputProps={inputProps}
            InputProps={{
                endAdornment: (
                    <InputAdornment position="end">
                        <IconButton
                            size='small'
                            aria-label="toggle password visibility"
                            onClick={handleClickShowPassword}
                            onMouseDown={handleMouseDownPassword}
                        >
                            {showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                    </InputAdornment>
                ),
            }}
        />);
    }
}