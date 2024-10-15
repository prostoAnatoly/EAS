import * as React from 'react';
import { styled } from '@mui/material/styles';
import { Button, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, ButtonProps, DialogTitleProps } from '@mui/material';

export interface FormDialogProps {
    title: string;
    contentText: string;
    yesButtonText: string;
    noButtonText: string;
    open: boolean;
    onHandleYes?: () => void;
    onHandleNo?: () => void;
    children?: React.ReactNode,
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

export function FormDialog(props: FormDialogProps) {

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
        <Dialog open={props.open} onClose={handleNo}>
            <BootstrapDialogTitle>{props.title}</BootstrapDialogTitle>
            <DialogContent>
                <DialogContentText>
                    {props.contentText}
                </DialogContentText>
                {props.children}
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