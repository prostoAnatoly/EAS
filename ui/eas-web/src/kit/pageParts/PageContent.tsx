import { Paper } from '@mui/material';
import * as React from 'react';

export class PageContentProps {
    constructor(public children?: React.ReactNode) {

    }
}

const style = {
    paper: {
        padding: (theme) => theme.spacing(2),
        backgroundColor: 'var(--EAS-BACKGROUND-COLOR-MAIN)',
    }
};

export function PageContent(props: PageContentProps) {
    return <Paper sx={style.paper} elevation={0}>
        {props.children}
    </Paper>
}