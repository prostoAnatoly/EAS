import * as React from 'react';
import { Box, BoxProps, styled } from '@mui/material';

const BootstrapBox = styled(Box)<BoxProps>(({ theme }) => ({
    width: '100%',
    marginTop: theme.spacing(1),
}));

class FormContentProps {
    constructor(
        public readonly boxProps?: BoxProps,
        public readonly children?: React.ReactNode) { }
}

export function FormContent(props: FormContentProps) {
    return (<>
        {
            props.children &&
            <BootstrapBox {...props.boxProps}>{props.children}</BootstrapBox>
        }
    </>);
}