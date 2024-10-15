import * as React from 'react';
import { Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, ButtonProps, DialogTitleProps } from '@mui/material';
import { styled } from '@mui/material/styles';

export interface AlertDialogProps {
    title: string;
    contentText: string;
    yesButtonText: string;
    noButtonText: string;
    open: boolean;
    onHandleYes?: () => void;
    onHandleNo?: () => void;
}

const BootstrapButtonYes = styled(Button)<ButtonProps>(({ theme }) => ({
    backgroundColor: theme.palette.secondary.main,
    color: theme.palette.secondary.contrastText,
    "&:hover": {
        backgroundColor: theme.palette.secondary.dark
    },
}));

const BootstrapDialogTitle = styled(DialogTitle)<DialogTitleProps>(({ theme }) => ({
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.primary.contrastText,
}));

export function AlertDialog(props: AlertDialogProps) {

    const handleYes = () => {
        if (props.onHandleYes) {
            props.onHandleYes();
        }
    };

    const handleNo = () => {
        if (props.onHandleNo) {
            props.onHandleNo();
        }
    };

    return (
        <Dialog
            open={props.open}
            onClose={handleNo}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description">

            <BootstrapDialogTitle id="alert-dialog-title">
                {props.title}
            </BootstrapDialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    {props.contentText}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleNo}>{props.noButtonText}</Button>
                <BootstrapButtonYes onClick={handleYes} autoFocus>
                    {props.yesButtonText}
                </BootstrapButtonYes>
            </DialogActions>
        </Dialog>
    );
}