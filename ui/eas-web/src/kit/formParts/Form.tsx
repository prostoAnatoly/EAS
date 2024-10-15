import * as React from 'react';
import { Box, BoxProps, styled } from '@mui/material';

const BootstrapBox = styled(Box)<BoxProps>(({ theme }) => ({
    marginTop: theme.spacing(4),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
}));

class FormProps {
    constructor(
        public readonly boxProps?: BoxProps,
        public readonly children?: React.ReactNode) { }
}

export function Form(props: FormProps) {
    return (<>
        {
            props.children &&
            <BootstrapBox {...props.boxProps}>{props.children}</BootstrapBox>
        }
    </>);
}