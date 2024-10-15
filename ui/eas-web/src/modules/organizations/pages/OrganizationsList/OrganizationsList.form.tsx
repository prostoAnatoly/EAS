import { AddHome } from '@mui/icons-material';
import { Avatar, Card, CardActionArea, CardContent, CardHeader, FormHelperText, Grid } from '@mui/material';
import * as React from 'react';
import { useNavigate } from 'react-router';
import { BackdropLoading } from '../../../../kit/kit';
import { getMaskedDate } from '../../../../utils/date-utils';
import { useProcessingOnlyOne, useStateSmart } from '../../../../utils/hooks';
import { HttpRequestError } from '../../../../utils/web/HttpRequestError';
import { GetOrganizationsQuery } from '../../application/features/GetOrganizationsQuery';
import { RoutingByOrganizations } from '../../config/routingByOrganizations';
import { SelectedOrganizationChangedIntegrationEvent } from '../../contracts/events/SelectedOrganizationChangedIntegrationEvent';
import { Organization } from '../../domain/indexDomain';
import { OrganizationsServiceCollection } from '../../OrganizationsServiceCollection';
import { organizationsStore } from '../../stores/OrganizationsStore';

class OrganizationsListFormState {
    constructor(public orgs?: Organization[], public errorMessage: string = '') {

    }
}

const mediator = OrganizationsServiceCollection.GetMediator();

export function OrganizationsListForm() {

    const [state, setState] = useStateSmart(new OrganizationsListFormState());

    const navigation = useNavigate();

    const [isLoading, loadOrganizations] = useProcessingOnlyOne(async () => {
        if (organizationsStore.ogranizations) {
            setState(prev => ({ ...prev, orgs: organizationsStore.ogranizations, errorMessage: '' }));

            return;
        }

        const getOrganizationQuery = new GetOrganizationsQuery();

        await mediator.send(getOrganizationQuery)
            .then((orgs) => {
                setState(prev => ({ ...prev, orgs: orgs, errorMessage: '' }));

                organizationsStore.ogranizations = orgs;
            })
            .catch(err => {
                setState(prev => ({ ...prev, errorMessage: HttpRequestError.createFromPromiseCatch(err).message }));
            });

    }, []);

    React.useEffect(() => {
        loadOrganizations();
    }, []);

    const getOrganizationsView = () => {
        return state.orgs?.map(org => getOrganizationView(org));
    }

    const getOrganizationView = (organization: Organization) => {
        return (<Grid xs={3}>
            <Card sx={(theme) => ({
            margin: theme.spacing(2),
            backgroundColor: 'cornsilk',
            })} >
                <CardHeader
                    avatar={
                        <Avatar sx={{ backgroundColor: 'cadetblue' }}>
                            {organization.name[0].toUpperCase()}
                        </Avatar>
                    }
                    title={organization.name}
                    subheader={`от ${getMaskedDate(organization.createDate)}`}
                />
                <CardContent>
                    Готова к работе
                </CardContent>
                <CardActionArea onClick={() => { selectingOrganization(organization) }}
                    sx={(theme) => ({
                        padding: theme.spacing(1),
                        backgroundColor: '#61998f',
                        textAlign: 'center',
                        ":hover": {
                            backgroundColor: '#455b55',
                            color: 'white',
                        },
                        fontSize: 'var(--EAS-FONT-SIZE-MD-TEXT)',
                        fontWeight: 'bold',
                    })}>
                    Выбрать организацию
                </CardActionArea>
        </Card>
        </Grid>);
    };

    const getButtonOrganizationAddView = () => {
        return (<Grid xs={3} sx={{
            margin: organizationsStore.hasOgranizations ? '0' : '0 auto',
        }}>
            <Card sx={(theme) => ({
                margin: theme.spacing(2),
                backgroundColor: 'aquamarine',
            })} >
                <CardHeader
                    avatar={
                        <Avatar sx={{ backgroundColor: 'darkorchid' }}>
                            <AddHome />
                        </Avatar>
                    }
                    title='Новая организация'
                />
                <CardContent>
                    &nbsp;
                </CardContent>
                <CardActionArea onClick={() => { addOrganization() }}
                    sx={(theme) => ({
                        padding: theme.spacing(1),
                        backgroundColor: '#e8d2bd',
                        textAlign: 'center',
                        ":hover": {
                            backgroundColor: '#a2815f',
                            color: 'white',
                        },
                        fontSize: 'var(--EAS-FONT-SIZE-MD-TEXT)',
                        fontWeight: 'bold',
                    })}>
                    Добавить организацию
                </CardActionArea>
            </Card>
        </Grid>);
    };

    const addOrganization = () => {
        navigation(RoutingByOrganizations.CREATE_ORGANIZATION);
    };

    const selectingOrganization = async (ogranization: Organization) => {
        organizationsStore.selectedOgranization = ogranization;

        await OrganizationsServiceCollection.getServiceBus()
            .publishEvent(new SelectedOrganizationChangedIntegrationEvent(ogranization.id));
    };

    return (<Grid container rowSpacing={1} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
        <BackdropLoading isLoading={isLoading} text={'Загружаем список организаций. Пожалуйста, ожидайте...'} />

        {state.errorMessage &&
            <FormHelperText sx={{ textAlign: 'center', width: '100%' }} error>{state.errorMessage}</FormHelperText>}

        {getButtonOrganizationAddView()}
        {getOrganizationsView()}

    </Grid>);
}