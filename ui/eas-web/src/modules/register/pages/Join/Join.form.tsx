import { Box, Button, Container, Link, Paper } from '@mui/material';
import * as React from 'react';
import { useEffect } from 'react';
import { useNavigate } from 'react-router';
import { Link as LinkDom } from 'react-router-dom';
import { FormDef, InputControl, IsRequiredValidation, MaxLengthValidation, MinLengthValidation, PasswordControl } from '../../../../forms/Forms';
import { ColorHr, FormErrorMessage, FormRow } from '../../../../kit/kit';
import { LocalCache } from '../../../../utils/caches/LocalCache';
import { useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { QueryPath } from '../../../../utils/queryPath';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { CheckInvitationCodeCommand } from '../../application/features/CheckInvitationCode/CheckInvitationCodeCommand';
import { LoginCommand } from '../../application/features/LoginCommand';
import { RegistrationUserByCodeCommand } from '../../application/features/RegistrationUserByCodeCommand';
import { RoutingByRegister } from '../../config/routingByRegister';
import { AlreadyRegisteredIntegrationEvent, ErrorRegisterIntegrationEvent, RegisterLoginedIntegrationEvent } from '../../contracts/integrationEvents';
import { RegisterServiceCollection } from '../../RegisterServiceCollection';

class RegEmployeeFormDef extends FormDef {
    public invitationCode = new InputControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(6)],
        title: 'Код из компании',
        isAutofocus: true
    });
    public userName: InputControl = new InputControl({
        validations: [],
        title: 'Ваш логин для входа в систему',
        allwaysDisabled: true,
    });
    public pwd = new PasswordControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(24), new MinLengthValidation(5)],
        title: 'Придумай пароль',
        autocomplete: 'new-password'
    });
}

class JoinFormState {
    constructor(public errorMessage: string = '', public isValidCode: boolean = false) {

    }
}

class JoinFormStateToCache {
    constructor(public code: string, public email?: string) {

    }
}

const CACHE_KEY_JOIN = 'JOIN_STATE_FORM';

const mediator = RegisterServiceCollection.GetMediator();

export function JoinForm() {
    const [state, setState] = useStateSmart(new JoinFormState());

    const navigation = useNavigate();

    const form = new RegEmployeeFormDef();

    useEffect(() => {
        const stateFromCache = LocalCache.Get<JoinFormStateToCache>(CACHE_KEY_JOIN);

        if (stateFromCache) {
            form.invitationCode.setValue(stateFromCache.code);
            form.userName.setNewInitialValue(stateFromCache.email);
            setState(prev => ({ ...prev, errorMessage: '', isValidCode: true }));
        }
    }, []);

    const [isLoading, registration] = useProcessingOnlyOne(async () => {

        if (!form.validation()) {
            return;
        }

        const userName = form.userName.getValue();
        const pwd = form.pwd.getValue();
        const registrationUserByCodeCommand = new RegistrationUserByCodeCommand(form.invitationCode.getValue(), userName, pwd);

        await mediator.send(registrationUserByCodeCommand)
            .then(() => {
                const loginCommand = new LoginCommand(userName, pwd);

                loginCommand.execute()
                    .then(async () => {
                        LocalCache.Remove(CACHE_KEY_JOIN);

                        await RegisterServiceCollection.getServiceBus().publishEvent(new RegisterLoginedIntegrationEvent());
                    }).catch(async err => {
                        const errMessage = HttpRequestError.createFromPromiseCatch(err).message;

                        await RegisterServiceCollection.getServiceBus().publishEvent(new ErrorRegisterIntegrationEvent(errMessage));
                    });    
            })
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message }));
            });
    }, [navigation]);

    const isDisabled = (): boolean => {
        return isLoading || isCheckCode;
    };

    const [isCheckCode, checkCode] = useProcessingOnlyOne(async () => {

        if (!form.invitationCode.validation()) {
            return;
        }

        LocalCache.Remove(CACHE_KEY_JOIN);

        const invitationCode = form.invitationCode.getValue();
        const checkInvitationCodeCommand = new CheckInvitationCodeCommand(invitationCode)

        await mediator.send(checkInvitationCodeCommand)
            .then((result) => {
                form.userName.setNewInitialValue(result.userName);

                LocalCache.Push(CACHE_KEY_JOIN, new JoinFormStateToCache(invitationCode, result.userName), 300);

                setState(prev => ({ ...prev, errorMessage: '', isValidCode: true }));
            })
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message, isValidCode: false }));
            });

    }, []);

    const handleKeyDownByCheckCode = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            checkCode();
        }
    };

    const handleKeyDownByRegistration = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            registration();
        }
    };

    const alreadyRegistered = async () => {
        LocalCache.Remove(CACHE_KEY_JOIN);

        await RegisterServiceCollection.getServiceBus().publishEvent(new AlreadyRegisteredIntegrationEvent());
    };

    form.setDisabled(isDisabled());
    form.invitationCode.props.onKeyPress = handleKeyDownByCheckCode;
    form.pwd.props.onKeyPress = handleKeyDownByRegistration;

    form.invitationCode.props.allwaysDisabled = !state.isValidCode;
    form.invitationCode.props.disabled = state.isValidCode;

    const getCheckInvCodeForm = () => {
        return (<>
            <FormErrorMessage message={state.errorMessage} />

            <FormRow>
                {form.invitationCode.view()}
            </FormRow>
            <FormRow>
                <Button variant="contained" color="primary" sx={{
                    margin: (theme) => theme.spacing(3, 1, 2),
                }}
                    disabled={isDisabled()} onClick={checkCode} fullWidth>Продолжить</Button>
            </FormRow>
        </>);
    };

    const getRegForm = () => {
        return (<>
            <FormErrorMessage message={state.errorMessage} />
            <FormRow>
                {form.invitationCode.view()}
            </FormRow>
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
                Нажимая "Продолжить", Вы соглашаетесь с <LinkDom to={`${RoutingByRegister.AGREEMENT_USER}?${new URLSearchParams({ [QueryPath.BACK]: RoutingByRegister.JOIN_TO_ORGANIZATION }).toString()}`}>Соглашение на обработку персональных данных</LinkDom>
            </Box>
        </>);
    };

    return (
        <Container component="main" maxWidth="sm">
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
                    width: '100%',
                    fontSize: 'calc(14px + 1vw)',
                    marginBottom: (theme) => theme.spacing(1),
                }}>
                    Присоединиться к организации
                    <ColorHr />
                </Box>

                {state.isValidCode ? getRegForm() : getCheckInvCodeForm()}

                <Box component='div' sx={{
                    marginTop: (theme) => theme.spacing(4),
                    fontSize: 'var(--EAS-FONT-SIZE-MD-TEXT)',
                    textAlign: 'center',
                }}>
                    Уже зарегистрировались? <Link component="button" onClick={alreadyRegistered}>Войти</Link>
                </Box>
            </Paper>
        </Container>
    );
}