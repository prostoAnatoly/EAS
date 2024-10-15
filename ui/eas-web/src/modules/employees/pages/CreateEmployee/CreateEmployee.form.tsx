import { Container } from '@mui/material';
import * as React from 'react';
import { useNavigate } from 'react-router';
import { DateControl, EmailValidation, FormDef, InputControl, IsRequiredValidation, MaxDateValidation, MaxLengthValidation, MinDateValidation, PhoneNumberControl, PhoneNumberValidation } from '../../../../forms/Forms';
import { ButtonAdd, ButtonCancel, FormButtonPanel, FormContent, FormErrorMessage, FormOnPaper, FormRow, FormSimpleHeader } from '../../../../kit/kit';
import { FullName } from '../../../../sharedModels/indexSharedModels';
import { addYears } from '../../../../utils/date-utils';
import { useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { createMuiStyle } from '../../../../utils/mui-utils';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { CreateEmployeeCommand } from '../../application/features/CreateEmployeeCommand';
import { RoutingByEmployees } from '../../config/routingByEmployees';
import { EmployeesServiceCollection } from '../../EmployeesServiceCollection';

class CreateEmployeeFormState {
    constructor(public errorMessage: string = '') {

    }
}

class CreateEmployeeFormDef extends FormDef {
    public surname = new InputControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(50)],
        title: 'Фамилия',
        isAutofocus: true
    });

    public name = new InputControl({
        validations: [new IsRequiredValidation(), new MaxLengthValidation(50)],
        title: 'Имя'
    });

    public patronymic = new InputControl({
        validations: [new MaxLengthValidation(50)], title: 'Отчество'
    });

    public phoneNumber = new PhoneNumberControl({
        validations: [new PhoneNumberValidation()], title: 'Номер телефона'
    });

    public email = new InputControl({
        validations: [new EmailValidation()], title: 'Эл. почта'
    });

    public birthday = new DateControl({
        validations: [new IsRequiredValidation(), new MaxDateValidation(addYears(new Date(), -16)),
        MinDateValidation.getMinDate()], title: 'Дата рождения'
    });

    public employmentDate = new DateControl({
        validations: [new IsRequiredValidation(), new MaxDateValidation(new Date()),
        MinDateValidation.getMinDate()], title: 'Дата приёма на работу'
    });
}

const mediator = EmployeesServiceCollection.GetMediator();

const style = createMuiStyle({
    buttonCancel: {
        marginRight: (theme) => theme.spacing(2),
    },
});

export function CreateEmployeeForm() {
    const [state, setState] = useStateSmart(new CreateEmployeeFormState());

    const navigation = useNavigate();

    const form = new CreateEmployeeFormDef();

    const [isCreating, create] = useProcessingOnlyOne(async () => {

        if (!form.validation()) {
            return;
        }

        const createOrganizationCommand = new CreateEmployeeCommand(
            new FullName(form.name.getValue(), form.surname.getValue(), form.patronymic.getValue()),
            form.birthday.getValueToJson(), form.phoneNumber.getValue(),
            form.email.getValue(), form.employmentDate.getValueToJson()
        );

        await mediator.send(createOrganizationCommand)
            .then(empl => {
                navigation(RoutingByEmployees.EMPLOYEES);
            })
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message }));
            });
    }, [navigation]);

    const cancel = () => {
        navigation(RoutingByEmployees.EMPLOYEES);
    };

    // Настройка формы
    form.setDisabled(isCreating);

    return (<Container component="main" maxWidth="md">
        <FormOnPaper>
            <FormSimpleHeader>
                Добавление сотрудника в организацию
            </FormSimpleHeader>
            <FormErrorMessage message={state.errorMessage} />
            <FormContent>
                <FormRow>
                    {form.surname.view()}
                    {form.name.view()}
                    {form.patronymic.view()}
                </FormRow>
                <FormRow>
                    {form.birthday.view()}
                    {form.employmentDate.view()}
                </FormRow>
                <FormRow>
                    {form.phoneNumber.view()}
                    {form.email.view()}
                </FormRow>
            </FormContent>
            <FormButtonPanel>
                <ButtonCancel disabled={isCreating} onClick={cancel} sx={style.buttonCancel}>Отмена</ButtonCancel>
                <ButtonAdd disabled={isCreating} onClick={create}>Создать</ButtonAdd>
            </FormButtonPanel>
        </FormOnPaper>
    </Container>);
}