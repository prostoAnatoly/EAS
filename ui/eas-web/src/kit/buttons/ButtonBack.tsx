import * as React from 'react';
import Button, { ButtonProps } from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import { ArrowBack } from '@mui/icons-material';

const BootstrapButton = styled(Button)<ButtonProps>(({ theme }) => ({
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.primary.contrastText,
    "&:hover": {
        backgroundColor: theme.palette.primary.dark
    },
    "&:disabled": {
        backgroundColor: theme.palette.primary.light
    }
}));

export function ButtonBack(props: ButtonProps) {
    return (<BootstrapButton {...props} startIcon={<ArrowBack />} />)
}