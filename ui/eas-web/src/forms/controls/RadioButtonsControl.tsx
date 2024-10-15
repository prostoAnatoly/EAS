import * as React from 'react';

import { Control, ControlProps, ItemInfo } from '../Forms';

import { FormControl, FormControlLabel, FormHelperText, FormLabel, Radio, RadioGroup, TextField } from '@mui/material';

export class RadioButtonsControlProps extends ControlProps {

    constructor(public items: ItemInfo[]) {
        super();
    }
}

export class RadioButtonsControl extends Control {

    constructor(public props: RadioButtonsControlProps) {
        super(props);
    }

    public view() {

        const onChangeValue = (e) => {
            const val = e.target.value;
            this.setValueInner(val);

            if (this.props.onChangeValue) {
                this.props.onChangeValue();
            }
        };

        const field = this.getStyleField();

        return (<FormControl sx={field} component="fieldset" disabled={this.props.disabled}>
            <FormLabel component="legend">{this.props.title}</FormLabel>
            <TextField sx={{
                display: 'none !important',
            }}
                inputRef={this.inputRef}
                value={this.getValue()}
            />
            <RadioGroup row name="row-radio-buttons-group" value={this.getValue()} onChange={onChangeValue}>
                {this.props.items.map((item, ind) =>
                    <FormControlLabel key={ind} value={item.value} control={<Radio />} label={item.text} />)}
            </RadioGroup>
            {this.getValidationInfo().getInvalid() ?
                <FormHelperText error={this.getValidationInfo().getInvalid()}>{this.getValidationInfo().getErrorText()}</FormHelperText> : null}
        </FormControl>);
    }

}