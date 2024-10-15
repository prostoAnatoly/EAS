import * as React from 'react';
import { Box, Button, Card, CardActions, CardHeader, Container, Grid, Link, Paper, Typography } from '@mui/material';
import StarIcon from '@mui/icons-material/StarBorder';
import { useNavigate } from 'react-router';
import { ColorHr } from '../../../../kit/kit';
import { RoutingByRegister } from '../../config/routingByRegister';
import { RegisterServiceCollection } from '../../RegisterServiceCollection';
import { AlreadyRegisteredIntegrationEvent } from '../../contracts/integrationEvents';

export function RegistrationForm() {

    const navigation = useNavigate();

    const enterCode = () => {
        navigation(RoutingByRegister.JOIN_TO_ORGANIZATION);
    };

    const createUser = () => {
        navigation(RoutingByRegister.USER_REGISTRATION);
    };

    const regByCode = () => {
        return (<Card>
            <CardHeader title="У меня есть КОД"
                titleTypographyProps={{ align: 'center' }}
                action={<StarIcon />}
                sx={{
                    backgroundColor: (theme) => theme.palette.primary.dark,
                    color: (theme) => theme.palette.primary.contrastText,
                }}
            />
            <CardActions sx={{
                backgroundColor: (theme) => theme.palette.primary.light,
            }}>
                <Button variant="contained" color="success" sx={{
                    margin: (theme) => theme.spacing(3, 1, 2),
                }} fullWidth onClick={enterCode}>Хочу присоединиться к организации</Button>
            </CardActions>
        </Card>);
    };

    const regAsUser = () => {
        return (<Card>
            <CardHeader title="Новый пользователь"
                titleTypographyProps={{ align: 'center' }}
                sx={{
                    backgroundColor: '#b96d20',
                }}
            />
            <CardActions sx={{
                backgroundColor: '#e3e29b',
            }}>
                <Button variant="contained" color="success" sx={{
                    margin: (theme) => theme.spacing(3, 1, 2),
                }} fullWidth onClick={createUser}>Хочу управлять сотрудниками</Button>
            </CardActions>
        </Card>);
    };

    const goToLogin = async () => {
        await RegisterServiceCollection.getServiceBus().publishEvent(new AlreadyRegisteredIntegrationEvent());
    };

    return (<Container component="main" maxWidth="lg">
        <Paper sx={(theme) => ({
            marginTop: theme.spacing(3),
            marginBottom: theme.spacing(3),
            padding: theme.spacing(2),
            backgroundColor: 'var(--EAS-BACKGROUND-COLOR-PAPER-INNER)',
        })}>
            <Box component='div' sx={{
                marginTop: (theme) => theme.spacing(4),
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'flex-end',
                fontSize: 'calc(var(--EAS-FONT-SIZE-BASE) + 0.2vw)'
            }}>
                <Link component="button" onClick={goToLogin}>Я уже зарегистрирован</Link>
            </Box>
            <Box component='div' sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}>
                <Typography component="h1" variant="h5">
                    Регистрация
                </Typography>

                <Box component='div' sx={{
                    width: '100%',
                    marginTop: (theme) => theme.spacing(2),
                    marginBottom: (theme) => theme.spacing(4),
                }}><ColorHr /></Box>

                <Grid container spacing={8} justifyContent="center">
                    <Grid
                        item
                        key='regByCode'
                        xs={12}
                        sm={6}
                        md={6}
                        lg={5}
                        xl={5}
                    >
                        {regByCode()}
                    </Grid>

                    <Grid
                        item
                        key='regAsOgr'
                        xs={12}
                        sm={6}
                        md={6}
                        lg={5}
                        xl={5}
                    >
                        {regAsUser()}
                    </Grid>
                </Grid>
            </Box>
        </Paper>

    </Container>);
}