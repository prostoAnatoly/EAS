import { Backdrop, Box, IconButton, Paper, Tooltip, SxProps, Theme } from '@mui/material';
import * as React from 'react';
import { FormDef, IsRequiredValidation, ItemInfo, RadioButtonsControl } from '../../../../forms/Forms';
import { ExTable, ExTableFilterDef, ExTableSearchParams } from '../../../../kit/tables/ExTable';
import { Pagination } from '../../../../sharedModels/exTableView/Pagination';
import { getMaskedDate } from '../../../../utils/date-utils';
import { ProcessingAgrs, useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { getMaskedPhoneNumber } from '../../../../utils/phone-utils';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { GetEmployeesQuery } from '../../application/features/GetEmployeesQuery';
import { GetEmployeesArgs } from '../../application/infrastructure/clients/EmployeesClient/arguments/GetEmployeesArgs';
import { Employee, EmployeeState } from '../../domain/indexDomain';
import EditIcon from '@mui/icons-material/Edit';
import { generatePath, useNavigate } from 'react-router';
import { RoutingByEmployees } from '../../config/routingByEmployees';
import { ButtonAdd } from '../../../../kit/kit';
import { Guid } from '../../../../sharedModels/indexSharedModels';
import { EmployeesServiceCollection } from '../../EmployeesServiceCollection';
import { createMuiStyle } from '../../../../utils/mui-utils';

class EmployeesSearchFilter {
    state?: EmployeeState;
}

class EmployeesState {
    constructor(
        public errorMessageByRows: string,
        public filterParams: EmployeesSearchFilter,
        public searchParams: ExTableSearchParams,
        public pagination: Pagination<Employee> | undefined = undefined) {
    }
}

class FilterFormDef extends FormDef {

    public state = new RadioButtonsControl({
        items: [new ItemInfo(STATE_ALL, 'Все'),
            new ItemInfo(EmployeeState.Active, 'Действующие'),
            new ItemInfo(EmployeeState.Dismissed, 'Уволенные')],
        validations: [new IsRequiredValidation()], title: '',
        initialValue: EmployeeState.Active.toString()
    });
}

const STATE_ALL = 'All';

const mediator = EmployeesServiceCollection.GetMediator();

const style = createMuiStyle({
    buttonAdd: {
        fontSize: 'var(--EAS-FONT-SIZE-BUTTON-TEXT)',
    },
    filters: {
        width: '100%',
        position: 'relative',
    },
    filtersBackdrop: {
        position: 'absolute',
        zIndex: (theme) => theme.zIndex.drawer - 1,
        color: '#fff',
    },
    filtersPaper: {
        padding: (theme) => theme.spacing(0.5),
        marginBottom: (theme) => theme.spacing(2),
        display: 'flex',
        alignItems: 'center',
    },
    filtersPanelButton: {
        width: '50%',
        textAlign: 'right',
        paddingRight: (theme) => theme.spacing(1),
    },
    filtersPanel: {
        width: '50%'
    },
    iconButton: {
        color: (theme) => theme.palette.primary.main,
        "&:hover": {
            color: (theme) => theme.palette.secondary.dark
        },
    },
});

export function EmployeesListForm() {
    const navigation = useNavigate();
    const form = new FilterFormDef();
    const [state, setState] = useStateSmart(new EmployeesState('', new EmployeesSearchFilter(),
        new ExTableSearchParams('', 'asc', 1, 10, [])));

    const [isLoading, getData] = useProcessingOnlyOne(async (e: ProcessingAgrs) => {
        const params = e.tag as ExTableSearchParams;
        if (params) {
            if (!form.validation()) {
                setState(prev => ({ ...prev, searchParams: params }));
                return;
            }
            const filter = state.filterParams;

            filter.state = EmployeeState[form.state.getValue()];

            const args = new GetEmployeesArgs(params);
            args.state = filter.state;
            const getEmployeesQuery = new GetEmployeesQuery(args);

            await mediator.send(getEmployeesQuery)
                .then(data => {
                    setState(prev => ({
                        ...prev, errorMessageByRows: '', pagination: data, searchParams: params
                    }));
                })
                .catch(err => {
                    const errMsg = HttpRequestError.createFromPromiseCatch(err).message;
                    setState(prev => ({
                        ...prev, errorMessageByRows: 'Ошибка получения данных: ' + errMsg,
                        pagination: undefined, searchParams: params
                    }));
                });
        }
    }, []);

    form.state.props.onChangeValue = async () => {
        const params = state.searchParams;
        params.page = 0;
        await getData(params);
        setState(prev => ({ ...prev, searchParams: params }));
    };

    const isDismissed = (empl: Employee): boolean => {
        if (!empl.state) {
            return false;
        }

        return empl.state === EmployeeState.Dismissed;
    };

    const getStyleByEmplItem = (empl: Employee): SxProps<Theme> => {
        if (form.state.getValue() === STATE_ALL && isDismissed(empl)) {
            return { backgroundColor: 'lightpink' };
        }

        return {};
    };

    const addEmployee = () => {
        navigation(RoutingByEmployees.CREATE_EMPLOYEE);
    };

    const editEmployee = (id: Guid) => {
        const url = generatePath(RoutingByEmployees.EDIT_EMPLOYEE,
            { id: id });

        navigation(url);
    };

    const FiltersView = () => {
        return (<Box component='div' sx={style.filters}>
            <Backdrop sx={style.filtersBackdrop} open={isLoading} />
            <Paper sx={style.filtersPaper}>
                <Box component='div' sx={style.filtersPanel}>{form.state.view()}</Box>
                <Box sx={style.filtersPanelButton}>
                    <ButtonAdd sx={style.buttonAdd} onClick={addEmployee}>Добавить нового сотрудника</ButtonAdd>
                </Box>
            </Paper>
        </Box>);
    };

    const TableView = () => {
        return (
            <ExTable rows={state.pagination?.items ?? []} onChangeSearchParams={getData} total={state.pagination?.total ?? 0}
                isLoading={isLoading} errorMessage={state.errorMessageByRows}
                rowDef={{ getSx: (item) => getStyleByEmplItem(item), }}
                columns={[
                    {
                        propertyName: 'fullName', description: 'ФИО', filter: new ExTableFilterDef(),
                        renderDataCell: (row) => {
                            return (<>{row.fullName.ToString()}</>);
                        }
                    },
                    {
                        propertyName: 'birthday', description: 'Дата рождения', disibledSort: true,
                        renderDataCell: (row) => {
                            return (<>{getMaskedDate(row.birthday)}</>);
                        }
                    },
                    {
                        propertyName: 'phone', description: 'Телефон', disibledSort: true,
                        renderDataCell: (row) => {
                            return (<>{getMaskedPhoneNumber(row.phoneNumber)}</>);
                        }
                    },
                    {
                        propertyName: 'email', description: 'Эл. адрес', disibledSort: true,
                    },
                    {
                        propertyName: '', description: '', disibledSort: true, renderDataCell: (row) => {
                            return (<><Tooltip title='Редактировать' placement='left-start'>
                                <IconButton sx={style.iconButton} size='medium' onClick={() => editEmployee(row.id)}>
                                    <EditIcon />
                                </IconButton>
                            </Tooltip></>);
                        },
                    }
                ]} propertyToKey={(row) => row.id} orderBy='fullName' order='asc' />
        )
    };

    return (<>
        <FiltersView />
        {TableView()}
    </>);
}