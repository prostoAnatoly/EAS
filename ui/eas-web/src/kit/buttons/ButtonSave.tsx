import * as React from 'react';
import Button, { ButtonProps } from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import { Save } from '@mui/icons-material';

const BootstrapButton = styled(Button)<ButtonProps>(({ theme }) => ({
    backgroundColor: theme.palette.secondary.main,
    color: theme.palette.secondary.contrastText,
    "&:hover": {
        backgroundColor: theme.palette.secondary.dark
    },
    "&:disabled": {
        backgroundColor: theme.palette.secondary.light
    }
}));

export function ButtonSave(props: ButtonProps) {
    return (<BootstrapButton {...props} startIcon={<Save />} />)
}