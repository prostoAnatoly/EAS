import * as React from 'react';
import Button, { ButtonProps } from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import { Cancel } from '@mui/icons-material';

const BootstrapButton = styled(Button)<ButtonProps>(({ theme }) => ({
    backgroundColor: '#457cc7',
    color: theme.palette.primary.contrastText,
    "&:hover": {
        backgroundColor: '#2f4e98'
    },
    "&:disabled": {
        backgroundColor: '#c2e0f7'
    }
}));

export function ButtonCancel(props: ButtonProps) {
    return (<BootstrapButton {...props} startIcon={<Cancel />} />)
}