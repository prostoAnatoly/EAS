import * as React from 'react';

import { Control, ControlProps } from '../Forms';

import { dateToFormatYmd, dateYmdToFormatJson } from '../../utils/date-utils';
import { TextField } from '@mui/material';

export class DateControlProps extends ControlProps {

}

export class DateControl extends Control {

    constructor(public props: DateControlProps) {
        super(props);
    }

    public getValueToJson(): string | undefined {
        var raw = super.getValue();
        if (raw) {
            return dateYmdToFormatJson(raw);
        }
        return undefined;
    }

    protected override processNewValue(val: string): string {
        if (val) {
            return dateToFormatYmd(new Date(val));
        }
        return val;
    }

    public override view() {
        const isRequired = this.isRequired();
        const inputProps = this.getInputPropsBase();

        const field = this.getStyleField();

        return (<TextField sx={field}
            inputRef={this.inputRef}
            onChange={this.onChangeValue}
            value={this.getValue()}
            disabled={this.props.disabled}
            error={this.getValidationInfo().getInvalid()}
            helperText={this.getValidationInfo().getErrorText()}
            margin="normal"
            fullWidth
            label={this.props.title}
            type="date"
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