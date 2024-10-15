import { Box } from '@mui/material';
import * as React from 'react';
import { QueryPath } from '../../../../utils/queryPath';
import { useQuery } from '../../../../utils/hooks';

export function ForbiddenForm() {
    const query = useQuery();

    const getTextResource = () => {
        const resource = query.get(QueryPath.FORBIDDEN_RESOURCE);
        if (resource) {
            return `Ресурс: ${resource}`;
        }
        return '';
    }

    return (<>
        <Box component='div' className='row-center' sx={{
            fontSize: 'calc(12px + 10vw)',
            color: 'indianred',
        }}>403</Box>
        <Box component='div' className='row-center' sx={{
            fontSize: 'calc(8px + 3vw)',
            color: 'cadetblue',
        }}>Доступ к ресурсу запрещён.</Box>
        <Box component='div' className='row-center' sx={{
            fontSize: 'calc(8px + 1vw)',
            paddingTop: (theme) => theme.spacing(2),
        }}>{getTextResource()}</Box>
    </>);
}