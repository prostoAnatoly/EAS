import { Box, Paper } from '@mui/material';
import * as React from 'react';

export class PageHeaderProps {
    constructor(public title?: string,
        public children?: React.ReactNode,
        public titleCustom?: React.ReactNode) {

    }
}

const style = {
    paper: {
        margin: (theme) => theme.spacing(1),
        padding: (theme) => theme.spacing(1.5),
        backgroundColor: '#6269a3',
        display: 'flex',
        color: 'white'
    },
    title: {
        fontSize: 'var(--EAS-FONT-SIZE-HEADER)',
    },
    customerBlock: {
        margin: '0 0 0 auto',
    },
};

export function PageHeader(props: PageHeaderProps) {
    return <>
        <Paper sx={style.paper}>
            <Box component='div' sx={style.title}>
                {props.title ? props.title : props.titleCustom}
            </Box>
            <Box component='div' sx={style.customerBlock}>
                {props.children}
            </Box>
        </Paper>
    </>
}