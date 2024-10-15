import * as React from 'react';
import { Routes, Route, Navigate } from "react-router-dom";
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Token } from './utils/Token';
import { RoutingByRegister } from './modules/register/config/routingByRegister';
import { RoutingByShell } from './modules/shell/config/routingByShell';
import { RoutingByOrganizations } from './modules/organizations/config/routingByOrganizations';
import { NavigateFunction, useLocation, useNavigate } from "react-router";

// Страницы
import { MainPage } from './modules/dashboard/pages/dashboardPages';
import { LoginPage, JoinPage, UserRegistrationPage, RegistrationPage, AgreementUserPage } from './modules/register/pages/registerPages';
import { Layout, NotFoundPage, ForbiddenPage, NoConnectionToServerPage } from './modules/shell/pages/shellPages';
import { CreateOrganizationPage, OrganizationsListPage } from './modules/organizations/pages/organizationsPages';
import { ConfigApiOrganizations } from './modules/organizations/api/configApi';
import { ConfigApiShell } from './modules/shell/api/configApi';
import { ConfigApiEmployees } from './modules/employees/api/configApi';
import { RoutingByEmployees } from './modules/employees/config/routingByEmployees';
import { CreateEmployeePage, EditEmployeePage, EmployeesListPage } from './modules/employees/pages/employeesPages';
import { ConfigApiRegister } from './modules/register/api/configApi';
import { organizationsStore } from './modules/organizations/stores/OrganizationsStore';
import { HttpClient } from './utils/web/HttpClient';
import { QueryPath } from './utils/queryPath';

export function PrivatePage({ children }) {
    if (Token.getIsExpired()) {
        return <Navigate to={RoutingByShell.LOGIN} replace />;
    }

    if (!organizationsStore.selectedOgranization) {
        const location = useLocation();
        const pathname = location.pathname;
        if (pathname !== RoutingByOrganizations.ORGANIZATIONS &&
            pathname !== RoutingByOrganizations.CREATE_ORGANIZATION) {
            return <Navigate to={RoutingByOrganizations.ORGANIZATIONS} replace />;
        }
    }

    return <Layout>{children}</Layout>;
}

export function ToLoginPage() {
    if (Token.getIsExpired()) {
        return <LoginPage />;
    }

    return <Navigate to={RoutingByOrganizations.ORGANIZATIONS} replace />;
}

export function PublicPage({ children }) {
    return <Layout>{children}</Layout>;
}

export function OpenPage({ children }) {
    if (Token.getIsExpired()) {
        return <Navigate to={RoutingByShell.LOGIN} replace />;
    }

    return <>{children}</>;
}

function initHttpClient(navigation: NavigateFunction) {

    HttpClient.setHandlerErrConnectionRefuused(() => {
        return new Promise(() => {
            const locationPathname = location.pathname;
            if (locationPathname.toLowerCase() === RoutingByShell.NO_CONNECTION_TO_SERVER.toLowerCase()) {
                return;
            }

            navigation({
                pathname: RoutingByShell.NO_CONNECTION_TO_SERVER,
                search: '?' +
                    new URLSearchParams({ [QueryPath.TARGET]: locationPathname }).toString()
            });
        });
    });

    HttpClient.addResponseHandlerByStatusCode(401, () => {
        return new Promise(() => {
            navigation(RoutingByShell.LOGIN);
        });
    });

    HttpClient.addResponseHandlerByStatusCode(403, (resp, msg, locationWhereCalled) => {
        return new Promise(() => {
            if (locationWhereCalled === location.href) {
                navigation({
                    pathname: RoutingByShell.FORBIDDEN,
                    search: '?' +
                        new URLSearchParams({ [QueryPath.FORBIDDEN_RESOURCE]: resp.url }).toString()
                });
            }
        });
    });

    HttpClient.addResponseHandlerByStatusCode(404, (resp, msg, locationWhereCalled) => {
        if (Token.get()) {
            return new Promise(() => {
                if (locationWhereCalled === location.href) {
                    navigation(RoutingByShell.NOT_FOUND);
                }
            });
        }
        return undefined;
    });
}

export default function App() {
    const config = {
        initialTitle: 'Система учёта сотрудников',
    };

    const navigate = useNavigate();

    React.useEffect(() => {
        document.getElementsByTagName('title')[0].innerHTML = config.initialTitle;

        initHttpClient(navigate);

        ConfigApiOrganizations.configuration(navigate);
        ConfigApiShell.configuration(navigate);
        ConfigApiEmployees.configuration(navigate);
        ConfigApiRegister.configuration(navigate);
    }, []);

    const theme = createTheme({
        palette: {
            primary: {
                main: '#00897b',
                light: '#4ebaaa',
                dark: '#005b4f',
            },
            secondary: {
                main: '#ba68c8',
                light: '#ee98fb',
                dark: '#883997',
            },
            success: {
                main: '#ffb74d',
                dark: '#c88719',
                light: '#ffe97d',
            },
        },
    });

    return (<ThemeProvider theme={theme}>
        <Routes>
            <Route path={RoutingByShell.ROOT} element={<PrivatePage><MainPage /></PrivatePage>} />
            <Route path={RoutingByShell.NOT_FOUND} element={<PublicPage><NotFoundPage /></PublicPage>} />
            <Route path={RoutingByShell.FORBIDDEN} element={<PublicPage><ForbiddenPage /></PublicPage>} />
            <Route path={RoutingByShell.LOGIN} element={<ToLoginPage />} />
            <Route path={RoutingByShell.NO_CONNECTION_TO_SERVER} element={<NoConnectionToServerPage />} />

            <Route path={RoutingByRegister.REGISTRATION} element={<RegistrationPage />} />
            <Route path={RoutingByRegister.JOIN_TO_ORGANIZATION} element={<JoinPage />} />
            <Route path={RoutingByRegister.AGREEMENT_USER} element={<AgreementUserPage />} />
            <Route path={RoutingByRegister.USER_REGISTRATION} element={<UserRegistrationPage />} />

            <Route path={RoutingByOrganizations.ORGANIZATIONS} element={<PrivatePage><OrganizationsListPage /></PrivatePage>} />
            <Route path={RoutingByOrganizations.CREATE_ORGANIZATION} element={<PrivatePage><CreateOrganizationPage /></PrivatePage>} />

            <Route path={RoutingByEmployees.EMPLOYEES} element={<PrivatePage><EmployeesListPage /></PrivatePage>} />
            <Route path={RoutingByEmployees.CREATE_EMPLOYEE} element={<PrivatePage><CreateEmployeePage /></PrivatePage>} />
            <Route path={RoutingByEmployees.EDIT_EMPLOYEE} element={<PrivatePage><EditEmployeePage /></PrivatePage>} />
        </Routes>
    </ThemeProvider>
    );

};
