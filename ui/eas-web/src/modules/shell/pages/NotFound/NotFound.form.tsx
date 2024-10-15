import * as React from 'react';
import { useNavigate } from 'react-router';
import { Box } from '@mui/material';
import { ButtonBack } from '../../../../kit/kit';
import { RoutingByShell } from '../../config/routingByShell';
 
export function NotFoundForm() {
    const navigate = useNavigate();

    const handleBack = () => {
        navigate(RoutingByShell.ROOT);
    }

    return (<>
        <Box component='div' className='row-center' sx={{
            fontSize: '10vw',
            color: 'indianred',
        }}>404</Box>
        <Box component='div' className='row-center' sx={{
            fontSize: '3vw',
            color: 'cadetblue',
        }}>Страница не найдена</Box>
        <Box component='div' className='row-center' sx={{
            marginTop: (theme) => theme.spacing(4),
            marginBottom: (theme) => theme.spacing(2),
        }}>
            <ButtonBack sx={{
                fontSize: 'calc(12px + 0.5vw)',
            }} onClick={handleBack}>На главную страницу</ButtonBack>
        </Box>
    </>);
}