import {
    Box, Button, Container, Link, Paper
} from '@mui/material';
import * as React from 'react';
import { useEffect } from 'react';
import { Link as LinkDom } from 'react-router-dom';
import {
    EmailValidation,
    FormDef, InputControl, IsRequiredValidation, MaxLengthValidation, MinLengthValidation,
    PasswordControl
} from '../../../../forms/Forms';
import { ColorHr, FormErrorMessage, FormRow, FormSimpleHeader } from '../../../../kit/kit';
import { LocalCache } from '../../../../utils/caches/LocalCache';
import { useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { QueryPath } from '../../../../utils/queryPath';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { LoginCommand } from '../../application/features/LoginCommand';
import { RegistrationUserCommand } from '../../application/features/RegistrationUserCommand';
import { RoutingByRegister } from '../../config/routingByRegister';
import { AlreadyRegisteredIntegrationEvent, ErrorRegisterIntegrationEvent, RegisterLoginedIntegrationEvent } from '../../contracts/integrationEvents';
import { RegisterServiceCollection } from '../../RegisterServiceCollection';

class UserRegistrationFormDef extends FormDef {
    public userName: InputControl = new InputControl({
        validations: [new EmailValidation, new IsRequiredValidation(), new MaxLengthValidation(50), new MinLengthValidation(3)],
        title: 'Ваш email для входа в систему',
        isAutofocus: true
    });
    public pwd = new PasswordControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(24), new MinLengthValidation(5)],
        title: 'Придумай пароль',
        autocomplete: 'new-password'
    });
}

class UserRegistrationState {
    constructor(public errorMessage: string = '') {

    }
}

class UserRegistrationFormStateToCache {
    constructor(public email?: string) {

    }
}

const CACHE_KEY_USER_REG = 'USER_REG_STATE_FORM';

const mediator = RegisterServiceCollection.GetMediator();

export function UserRegistrationForm() {
    const [state, setState] = useStateSmart(new UserRegistrationState());
    const form = new UserRegistrationFormDef();

    useEffect(() => {
        const stateFromCache = LocalCache.Get<UserRegistrationFormStateToCache>(CACHE_KEY_USER_REG);

        if (stateFromCache) {
            form.userName.setNewInitialValue(stateFromCache.email);
            setState(prev => ({ ...prev, errorMessage: '' }));
        }
    }, []);

    const [isLoading, registration] = useProcessingOnlyOne(async () => {

        if (!form.validation()) {
            return;
        }

        const userName = form.userName.getValue();
        const pwd = form.pwd.getValue();

        const registrationUserCommand = new RegistrationUserCommand(userName, pwd);

        await mediator.send(registrationUserCommand)
            .then(async () => {
                const loginCommand = new LoginCommand(userName, pwd);

                await loginCommand.execute()
                    .then(async () => {
                        LocalCache.Remove(CACHE_KEY_USER_REG);

                        await RegisterServiceCollection.getServiceBus().publishEvent(new RegisterLoginedIntegrationEvent());
                    }).catch(async err => {
                        const errMessage = HttpRequestError.createFromPromiseCatch(err).message;

                        await RegisterServiceCollection.getServiceBus().publishEvent(new ErrorRegisterIntegrationEvent(errMessage));
                    });
            })
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message }));
            });
    }, []);

    const isDisabled = (): boolean => {
        return isLoading;
    };

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            registration();
        }
    };

    const alreadyRegistered = async () => {
        LocalCache.Remove(CACHE_KEY_USER_REG);

        await RegisterServiceCollection.getServiceBus().publishEvent(new AlreadyRegisteredIntegrationEvent());
    };

    form.setDisabled(isDisabled());
    form.pwd.props.onKeyPress = handleKeyDown;

    const getRegForm = () => {
        return (<>
            <FormErrorMessage message={state.errorMessage} />
            <FormRow>
                {form.userName.view()}
            </FormRow>
            <FormRow>
                {form.pwd.view()}
            </FormRow>
            <FormRow>
                <Button variant="contained" color="primary" sx={{
                    margin: (theme) => theme.spacing(3, 1, 2),
                }}
                    disabled={isDisabled()} onClick={registration} fullWidth>Продолжить</Button>
            </FormRow>
            <Box component='div' sx={{
                width: '100%',
                fontSize: 'var(--EAS-FONT-SIZE-MD-TEXT)',
            }}>
                Нажимая "Продолжить", Вы соглашаетесь с <LinkDom
                    to={`${RoutingByRegister.AGREEMENT_USER}?${new URLSearchParams({ [QueryPath.BACK]: RoutingByRegister.USER_REGISTRATION }).toString()}`}
                    onClick={() => { LocalCache.Push(CACHE_KEY_USER_REG, new UserRegistrationFormStateToCache(form.userName.getValue()), 300); }}>Соглашение на обработку персональных данных</LinkDom>
            </Box>
        </>);
    };

    return (<Container component="main" maxWidth="sm">
        <Paper sx={{
            marginTop: (theme) => theme.spacing(4),
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            padding: (theme) => theme.spacing(2),
            margin: (theme) => theme.spacing(2),
            borderRadius: (theme) => theme.spacing(2),
            cursor: 'pointer',
            textAlign: 'center',
        }}>
            <Box component='div' sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
            }}>
                <FormSimpleHeader>
                    Зарегистрироваться
                </FormSimpleHeader>

                <Box component='div' sx={{
                    width: '100%',
                    marginTop: (theme) => theme.spacing(2),
                    marginBottom: (theme) => theme.spacing(4),
                }}><ColorHr /></Box>

                {getRegForm()}

                <Box component='div' sx={{
                    marginTop: (theme) => theme.spacing(4),
                    fontSize: 'var(--EAS-FONT-SIZE-MD-TEXT)',
                    textAlign: 'center',
                }}>
                    Уже зарегистрировались? <Link component="button" onClick={alreadyRegistered}>Войти</Link>
                </Box>
            </Box>
        </Paper>
    </Container>);
}