import * as React from 'react';
import { BoxProps, Paper, PaperProps, styled } from '@mui/material';
import { Form } from './Form';

const BootstrapPaper = styled(Paper)<PaperProps>(({ theme }) => ({
    padding: theme.spacing(2),
    backgroundColor: 'snow',
}));

class FormOnPaperProps {
    constructor(
        public readonly paperProps?: PaperProps,
        public readonly boxProps?: BoxProps,
        public readonly children?: React.ReactNode) { }
}

export function FormOnPaper(props: FormOnPaperProps) {
    return (<BootstrapPaper {...props.paperProps}><Form boxProps={props.boxProps}>{props.children}</Form></BootstrapPaper>);
}