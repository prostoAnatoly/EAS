import * as React from 'react';
import Button, { ButtonProps } from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import { Delete } from '@mui/icons-material';

const BootstrapButton = styled(Button)<ButtonProps>(({ theme }) => ({
    backgroundColor: theme.palette.error.main,
    color: theme.palette.error.contrastText,
    "&:hover": {
        backgroundColor: theme.palette.error.dark
    },
    "&:disabled": {
        backgroundColor: theme.palette.error.light
    }
}));

export function ButtonDelete(props: ButtonProps) {
    return (<BootstrapButton {...props} startIcon={<Delete />} />)
}