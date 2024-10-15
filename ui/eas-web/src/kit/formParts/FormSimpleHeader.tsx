import * as React from 'react';
import { Typography } from '@mui/material';

class FormSimpleHeaderProps {
    constructor(public readonly children?: React.ReactNode) { }
}

export function FormSimpleHeader(props: FormSimpleHeaderProps) {
    return (<>{
        props.children && <Typography component="h1" variant="h5">
            {props.children}
        </Typography>
    }</>);
}