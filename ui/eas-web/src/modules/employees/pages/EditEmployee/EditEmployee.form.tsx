import { Container } from '@mui/material';
import * as React from 'react';
import { useNavigate, useParams } from 'react-router';
import { DateControl, EmailValidation, FormDef, InputControl, IsRequiredValidation, MaxDateValidation, MaxLengthValidation, MinDateValidation, PhoneNumberControl, PhoneNumberValidation } from '../../../../forms/Forms';
import { ButtonAdd, ButtonCancel, FormButtonPanel, FormContent, FormErrorMessage, FormOnPaper, FormRow, FormSimpleHeader } from '../../../../kit/kit';
import { FullName, Guid } from '../../../../sharedModels/indexSharedModels';
import { addYears } from '../../../../utils/date-utils';
import { useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { createMuiStyle } from '../../../../utils/mui-utils';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { GetEmployeeQuery } from '../../application/features/GetEmployeeQuery';
import { UpdateEmployeeCommand } from '../../application/features/UpdateEmployeeCommand';
import { RoutingByEmployees } from '../../config/routingByEmployees';
import { Employee } from '../../domain/indexDomain';
import { EmployeesServiceCollection } from '../../EmployeesServiceCollection';

class EditEmployeeFormState {
    constructor(
        public employee: Employee | undefined = undefined,
        public errorMessage: string = '',
        public isErrorLoadingByEmployee: boolean = false) {

    }
}

class EditEmployeeFormDef extends FormDef {
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

export function EditEmployeeForm() {
    const routeParams = useParams();

    const [isLoading, getEmployee] = useProcessingOnlyOne(async () => {
        const id = routeParams.id as Guid;

        const getEmployeeQuery = new GetEmployeeQuery(id);

        await mediator.send(getEmployeeQuery)
            .then(empl => {
                if (empl.fullName) {
                    form.surname.setValue(empl.fullName.surname);
                    form.name.setValue(empl.fullName.name);
                    form.patronymic.setValue(empl.fullName.patronymic);
                }
                form.birthday.setValue(empl.birthday);
                form.employmentDate.setValue(empl.employmentDate);
                form.phoneNumber.setValue(empl.phoneNumber);
                form.email.setValue(empl.email);

                setState(prev => ({ ...prev, employee: empl, isErrorLoadingByEmployee: false, errorMessage: '' }));
            })
            .catch(err => {
                fillErrorField(err);
            });
    }, [], () => {
        setState(prev => ({
            ...prev,
            errorMessage: 'Невозможно получить данные по сотруднику. Попробуйте обновить страницу',
            isErrorLoadingByEmployee: true,
            employee: undefined,
        }));
    });

    const [state, setState] = useStateSmart(new EditEmployeeFormState(), getEmployee);

    const navigation = useNavigate();

    const form = new EditEmployeeFormDef();

    const fillErrorField = (err: any) => {
        setState(prev => ({
            ...prev,
            errorMessage: HttpRequestError.createFromPromiseCatch(err).message,
            isErrorLoadingByEmployee: false,
        }));
    };

    const [isSaving, saveEmployee] = useProcessingOnlyOne(async () => {

        if (!form.validation()) {
            return;
        }

        if (!state.employee) {
            setState(prev => ({
                ...prev,
                errorMessage: 'Вернитесь в список сотрудников и выберите для редактирования нужного сотрудника',
                isErrorLoadingByEmployee: false,
            }));

            return;
        }

        const updateEmployeeCommand = new UpdateEmployeeCommand(state.employee?.id,
            new FullName(form.name.getValue(), form.surname.getValue(), form.patronymic.getValue()),
            form.birthday.getValueToJson(), form.phoneNumber.getValue(), 
            form.email.getValue(), form.employmentDate.getValueToJson()
        );

        await mediator.send(updateEmployeeCommand)
            .then(empl => {
                navigation(RoutingByEmployees.EMPLOYEES);
            })
            .catch(err => {
                fillErrorField(err);
            });
    }, [navigation, state], fillErrorField);

    const cancel = () => {
        navigation(RoutingByEmployees.EMPLOYEES);
    };

    const idDisabled = () => {
        return isLoading || isSaving || state.isErrorLoadingByEmployee;
    }

    // Настройка формы
    form.setDisabled(idDisabled());

    return (<Container component="main" maxWidth="md">
        <FormOnPaper>
            <FormSimpleHeader>
                Профиль сотрудника
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
                <ButtonCancel disabled={idDisabled()} onClick={cancel} sx={style.buttonCancel}>Отмена</ButtonCancel>
                <ButtonAdd disabled={idDisabled()} onClick={saveEmployee}>Сохранить</ButtonAdd>
            </FormButtonPanel>
        </FormOnPaper>
    </Container>);
}