import * as React from 'react';
import { CircularProgress, Box } from '@mui/material';

export interface LoadingProgressProps {
    text?: string;
}

export function LoadingProgress(props: LoadingProgressProps) {
    const text = props.text ?? "Идет загрузка данных...";
    return <Box component="div" sx={{ textAlign: 'center'}}>
        <CircularProgress />
        <div><Box component="span" sx={{ paddingTop: (theme) => theme.spacing(2)}}>{text}</Box></div>
    </Box>
}