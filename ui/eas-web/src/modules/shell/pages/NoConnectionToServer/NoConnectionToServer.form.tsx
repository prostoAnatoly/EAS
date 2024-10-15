import { Box } from '@mui/material';
import * as React from 'react';
import { useNavigate } from 'react-router';
import { ButtonRefresh } from '../../../../kit/kit';
import { useQuery } from '../../../../utils/hooks';
import { QueryPath } from '../../../../utils/queryPath';
import { RoutingByShell } from '../../config/routingByShell';

export function NoConnectionToServerForm() {
    const query = useQuery();
    const navigate = useNavigate();

    const style = {
        fontSize: 'calc(3px + 3vw)',
        color: 'lightcoral',
    };

    const refresh = () => {
        const resource = query.get(QueryPath.TARGET);

        if (resource) {
            navigate(resource);

            return;
        }

        navigate(RoutingByShell.ROOT);
    };

    return (<>
        <Box component='div' className='row-center' sx={style}>Нет связи с сервером.</Box>
        <Box component='div' className='row-center' sx={style}>Пожалуйста, обновите страницу позже...</Box>
        <Box component='div' className='row-center' sx={{
            marginTop: (theme) => theme.spacing(4),
            marginBottom: (theme) => theme.spacing(2),
        }}>
            <ButtonRefresh sx={{
                fontSize: 'calc(12px + 0.5vw)',
            }} onClick={refresh}>Обновить страницу</ButtonRefresh>
        </Box>
    </>);
}