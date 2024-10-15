import { FormHelperText } from '@mui/material';
import * as React from 'react';

interface FormErrorMessageProps {
    message?: string;
}

export function FormErrorMessage(props: FormErrorMessageProps) {
    return (<>{
        props.message &&
            <FormHelperText error>
                {props.message}
            </FormHelperText>
    }</>);
}