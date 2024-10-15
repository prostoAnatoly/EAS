import { Backdrop } from '@mui/material';
import * as React from 'react';
import { LoadingProgress } from '../kit';

export interface BackdropLoadingProps {
    text?: string;
    isLoading: boolean;
}

export function BackdropLoading(props: BackdropLoadingProps) {
    return <>{
        props.isLoading && <Backdrop sx={{
            zIndex: (theme) => theme.zIndex.drawer + 1,
            color: '#fff',
        }} open={props.isLoading}>
            <LoadingProgress text={props.text} />
        </Backdrop>
    }</>;
}