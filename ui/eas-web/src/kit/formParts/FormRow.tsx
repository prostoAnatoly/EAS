import * as React from 'react';
import { Box, BoxProps, styled } from '@mui/material';

const BootstrapBox = styled(Box)<BoxProps>(() => ({
    display: 'flex',
    width: '100%',
}));

class FormRowProps {
    constructor(
        public readonly boxProps?: BoxProps,
        public readonly children?: React.ReactNode) { }
}

export function FormRow(props: FormRowProps) {
    return (<>
        {
            props.children &&
            <BootstrapBox {...props.boxProps}>{props.children}</BootstrapBox>
        }
    </>);
}