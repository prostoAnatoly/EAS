import * as React from 'react';
import { Box, BoxProps, styled } from '@mui/material';

const BootstrapBox = styled(Box)<BoxProps>(({ theme }) => ({
    marginTop: theme.spacing(3),
    width: '100%',
    display: 'block',
    textAlign: 'right',
    paddingRight: theme.spacing(2),
}));

class FormButtonPanelProps {
    constructor(
        public readonly boxProps?: BoxProps,
        public readonly children?: React.ReactNode) { }
}

export function FormButtonPanel(props: FormButtonPanelProps) {
    return (<>
        {
            props.children &&
            <BootstrapBox {...props.boxProps}>{props.children}</BootstrapBox>
        }
    </>);
}