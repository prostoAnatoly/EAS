import * as React from 'react';

import { Box } from '@mui/material';

const NameApp = () => {
    return (<Box component="div" sx={{
        fontSize: 'calc(10px + 2vw)',
        color: 'chocolate',
        marginTop: (theme) => theme.spacing(1),
    }}>
        Система учёта сотрудников
    </Box>
    );
};

export function MainPage() {
    return (
        <>
            <NameApp />
        </>
    );
}