import * as React from 'react';
import Button, { ButtonProps } from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import { Add } from '@mui/icons-material';

const BootstrapButton = styled(Button)<ButtonProps>(({ theme }) => ({
    backgroundColor: theme.palette.success.main,
    color: theme.palette.success.contrastText,
    "&:hover": {
        backgroundColor: theme.palette.success.dark
    },
    "&:disabled": {
        backgroundColor: theme.palette.success.light
    }
}));

export function ButtonAdd(props: ButtonProps) {
    return (<BootstrapButton {...props} startIcon={ <Add />} />)
}