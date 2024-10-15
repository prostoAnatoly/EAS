import { AccountCircle, ExitToApp } from '@mui/icons-material';
import { Avatar, Box, Button, Container, Link, Paper, Typography } from '@mui/material';
import { deepPurple } from '@mui/material/colors';
import * as React from 'react';
import { useNavigate } from 'react-router';
import { EmailValidation, FormDef, InputControl, IsRequiredValidation, MaxLengthValidation, PasswordControl } from '../../../../forms/Forms';
import { ColorHr, FormErrorMessage, FormRow } from '../../../../kit/kit';
import { useProcessingOnlyOne, useQuery, useStateSmart } from '../../../../utils/hooks';
import { QueryPath } from '../../../../utils/queryPath';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { LoginCommand } from '../../application/features/LoginCommand';
import { RoutingByRegister } from '../../config/routingByRegister';
import { RegisterLoginedIntegrationEvent } from '../../contracts/events/RegisterLoginedIntegrationEvent';
import { RegisterServiceCollection } from '../../RegisterServiceCollection';
import { currentProfileUserStore } from '../../stores/CurrentProfileUserStore';

class LoginFormDef extends FormDef {
    public userName = new InputControl({
        validations: [new IsRequiredValidation(), new EmailValidation()],
        title: 'Логин',
        isAutofocus: true
    });

    public pwd = new PasswordControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(24)],
        title: 'Пароль'
    });
}

class UserProfile {

    constructor(public readonly userName: string | undefined, public readonly name: string | undefined,
        public readonly avatarUrl: string | undefined) { }
}

class LoginFormState {
    constructor(public profile?: UserProfile, public errorMessage: string = '') {

    }
}

const mediator = RegisterServiceCollection.GetMediator();

export function LoginForm() {
    const query = useQuery();
    const error = query.get(QueryPath.ERROR) ?? '';

    const [state, setState] = useStateSmart(
        new LoginFormState(
            new UserProfile(
                currentProfileUserStore.Profile?.userName,
                currentProfileUserStore.Profile?.fullName?.name,
                currentProfileUserStore.Profile?.avatarUrl), error));

    const navigation = useNavigate();

    const form = new LoginFormDef();

    const [isLoading, login] = useProcessingOnlyOne(async () => {

        if ((isShowProfile() && !form.pwd.validation()) || (!isShowProfile() && !form.validation())) {
            return;
        }

        const profile = state.profile;
        let userName = form.userName.getValue();
        if (isShowProfile() && profile) {
            userName = profile.userName || '';
        }

        const loginCommand = new LoginCommand(userName, form.pwd.getValue());

        await mediator.send(loginCommand)
            .then(async () => await RegisterServiceCollection.getServiceBus().publishEvent(new RegisterLoginedIntegrationEvent()))
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message }));
            });

    }, [navigation]);

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            login();
        }
    };

    const getAvatarControl = () => {
        const profile = state.profile;

        if (!profile) { return undefined; }

        const avatarStyle = {
            width: (theme) => theme.spacing(12),
            height: (theme) => theme.spacing(12),
            margin: '0 auto',
            marginBottom: (theme) => theme.spacing(2),
        };

        if (profile.avatarUrl) {
            return (<Avatar sx={avatarStyle} alt={profile.name} src={profile.avatarUrl} />)
        }

        if (profile.name) {
            return (<Avatar sx={{
                ...avatarStyle, ...{
                    color: (theme) => theme.palette.getContrastText(deepPurple[500]),
                    backgroundColor: deepPurple[500],
                }
            }} >{profile.name}</Avatar>);
        }
        return (<AccountCircle sx={avatarStyle} />);
    };

    const getProfileControl = () => {
        const profile = state.profile;

        if (!profile) { return undefined; }

        return (<>
            <div>
                {getAvatarControl()}
            </div>
            <Box component='div' sx={{
                textAlign: 'center',
                fontSize: 'calc(12px + 0.8vw)',
                marginBottom: (theme) => theme.spacing(2),
            }}>
                Здравствуйте, {profile.name}
            </Box>
            <Box component='div' sx={{
                textAlign: 'center',
            }}>
                <Link component="button" disabled={isLoading} onClick={goToUserOther}>Другой пользователь</Link>
            </Box>
        </>);
    };

    const goToUserOther = () => {
        setState(prev => ({
            ...prev, profile: undefined
        }));
    };

    const isShowProfile = (): boolean => {
        return state.profile?.userName ? true : false;
    };

    const exit = (): void => {
        currentProfileUserStore.Profile = undefined;

        goToUserOther();
    };

    const registration = () => {
        navigation(RoutingByRegister.REGISTRATION);
    };

    // Настройка формы
    form.userName.props.onKeyPress = handleKeyDown;
    form.pwd.props.onKeyPress = handleKeyDown;

    form.setDisabled(isLoading);

    return (
        <Container component="main" maxWidth="xs">
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
                    alignItems: 'center',
                }}>
                    {!isShowProfile() && <Typography component="h1" variant="h5">
                        Вход
                    </Typography>}
                    <FormErrorMessage message={state.errorMessage} />
                    <Box component='div' sx={{
                        width: '100%',
                        marginTop: (theme) => theme.spacing(1),
                    }}>
                        {isShowProfile() && getProfileControl()}
                        {!isShowProfile() && <FormRow>{form.userName.view()}</FormRow>}
                        <FormRow>
                            {form.pwd.view()}
                        </FormRow>
                        <FormRow>
                            <Button variant="contained" color="primary" sx={{
                                margin: (theme) => theme.spacing(3, 1, 2),
                            }}
                                disabled={isLoading} onClick={login} fullWidth>Войти</Button>
                        </FormRow>
                    </Box>
                    {isShowProfile() && <Box component='div' sx={{
                        textAlign: 'right',
                        width: '100%',
                    }}>
                        <Button variant='text' sx={{
                            fontSize: 'var(--SMF-FONT-SIZE-SMALL-TEXT)',
                            marginRight: (theme) => theme.spacing(1),
                        }}
                            disabled={isLoading}
                            endIcon={<ExitToApp />} onClick={exit}>Выйти</Button>
                    </Box>}

                    <Box component='div' sx={{
                        width: '100%',
                        marginTop: (theme) => theme.spacing(3),
                    }}><ColorHr /></Box>
                    <FormRow>
                        <Button variant="outlined" color="primary" sx={{
                            margin: (theme) => theme.spacing(1, 1, 2),
                        }}
                            disabled={isLoading} onClick={registration} fullWidth>Регистрация</Button>
                    </FormRow>
                </Box>
            </Paper>
        </Container>
    );
}