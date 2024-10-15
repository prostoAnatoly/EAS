import { Container } from '@mui/material';
import * as React from 'react';
import { useNavigate } from 'react-router';
import { FormDef, InputControl, IsRequiredValidation, MaxLengthValidation } from '../../../../forms/Forms';
import { ButtonAdd, ButtonCancel, FormButtonPanel, FormContent, FormErrorMessage, FormOnPaper, FormRow, FormSimpleHeader } from '../../../../kit/kit';
import { useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { createMuiStyle } from '../../../../utils/mui-utils';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { CreateOrganizationCommand } from '../../application/features/CreateOrganizationCommand';
import { RoutingByOrganizations } from '../../config/routingByOrganizations';
import { OrganizationsServiceCollection } from '../../OrganizationsServiceCollection';
import { organizationsStore } from '../../stores/OrganizationsStore';

class CreateOrganizationFormState {
    constructor(public errorMessage: string = '') {

    }
}

class CreateOrganizationFormDef extends FormDef {
    public name = new InputControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(100)],
        title: 'Наименование',
        isAutofocus: true
    });
}

const mediator = OrganizationsServiceCollection.GetMediator();

const style = createMuiStyle({
    buttonCancel: {
        marginRight: (theme) => theme.spacing(2),
    },
});

export function CreateOrganizationForm() {

    const [state, setState] = useStateSmart(new CreateOrganizationFormState());

    const navigation = useNavigate();

    const form = new CreateOrganizationFormDef();

    const [isRegistrationing, registration] = useProcessingOnlyOne(async () => {

        if (!form.validation()) {
            return;
        }

        const createOrganizationCommand = new CreateOrganizationCommand(form.name.getValue());

        await mediator.send(createOrganizationCommand)
            .then(org => {
                organizationsStore.ogranizations = (organizationsStore.ogranizations || []).concat(org);

                navigation(RoutingByOrganizations.ORGANIZATIONS);
            })
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message }));
            });
    }, [navigation]);

    const cancel = () => {
        navigation(RoutingByOrganizations.ORGANIZATIONS);
    };

    // Настройка формы
    form.setDisabled(isRegistrationing);

    return (<Container component="main" maxWidth="md">
        <FormOnPaper>
            <FormSimpleHeader>
                Регистрация организации
            </FormSimpleHeader>
            <FormErrorMessage message={state.errorMessage} />
            <FormContent>
                <FormRow>
                    {form.name.view()}
                </FormRow>
            </FormContent>
            <FormButtonPanel>
                <ButtonCancel disabled={isRegistrationing} onClick={cancel} sx={style.buttonCancel}>Отмена</ButtonCancel>
                <ButtonAdd disabled={isRegistrationing} onClick={registration}>Зарегистрировать</ButtonAdd>
            </FormButtonPanel>
        </FormOnPaper>
    </Container>);
}