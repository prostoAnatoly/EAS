import React from 'react';

import { Snackbar, Alert, AlertTitle } from "@mui/material";

interface SnackBarProps {
    message: string,
    handleCloseMessage: Function,
}

export function SnackBarError (props: SnackBarProps) {
    return (
        <Snackbar
            sx={{
                '& .MuiAlert-icon': {
                    '& svg': {
                        display: 'none'
                    }
                }
            }}
            open={props.message.length > 0} onClose={() => props.handleCloseMessage()}>
            <Alert elevation={6}
                onClose={() => props.handleCloseMessage()} severity='error'>
                <AlertTitle>Ошибка</AlertTitle>
                {props.message}
            </Alert>
        </Snackbar>
    );
}