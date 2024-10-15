import { Box } from '@mui/material';
import { observer } from 'mobx-react';
import * as React from 'react';
import { useEffect } from 'react';
import { usePathnames } from '../../../../utils/hooks';
import { MenuByEmployees } from '../../../employees/config/menuByEmployees';
import { MenuByOrganizations } from '../../../organizations/config/menuByOrganizations';
import { organizationsStore } from '../../../organizations/stores/OrganizationsStore';
import { currentProfileUserStore } from '../../../register/stores/CurrentProfileUserStore';
import { LogoutCommand } from '../../application/features/LogoutCommand';
import { RoutingByShell } from '../../config/routingByShell';
import { shellStore } from '../../stores/ShellStore';
import MainMenu from '../menu/MainMenu';
import OrganizationInfoView from './OrganizationInfoView';

const ROOT_APP = RoutingByShell.ROOT_APP;

export function Layout(props: { children?: React.ReactNode }) {
    const pathnames = usePathnames();

    const getIndexPathPageMain = (): number => {
        return ROOT_APP ? 2 : 1;
    }

    const getStyleBox = () => {
        return {
            backgroundImage: `url("${ROOT_APP}/assets/images/layout/sky.JPG");`,
            backgroundPosition: 'center',
            backgroundSize: 'cover',
            backgroundRepeat: 'no-repeat',
            width: '100%',
            height: '100vh',
        };
    };

    const getMenuItems = () => {
        const items = [
            ...MenuByOrganizations.getMenuItems(),
        ];

        if (!organizationsStore.selectedOgranization) {
            return items;
        }

        return [
            ...items,
            ...MenuByEmployees.getMenuItems(),
        ];
    };

    const Menu = observer(() => {
        const profile = currentProfileUserStore.Profile;
        const actionLogout = () => {
            const logoutCommand = new LogoutCommand();
            return logoutCommand.execute();
        }

        return (<MainMenu menuItems={getMenuItems()} rootApp={`${ROOT_APP}/`} userFullName={profile?.fullName}
            avatarUrl={shellStore.avatarObjectURL} actionLogout={actionLogout} />);
    });

    const fetchAvatar = async () => {
        if (!currentProfileUserStore.Profile?.avatarUrl) { return; }

        await fetch(currentProfileUserStore.Profile?.avatarUrl)
            .then(resp => resp.blob())
            .then(blob => {
                const reader = new FileReader();
                reader.onloadend = () => {
                    if (reader.result) {
                        shellStore.avatarObjectURL = reader.result as string;
                    }
                };
                reader.readAsDataURL(blob);
            })
            .catch(err => console.log('Невозможно загрузить аватарку -> ' + err));
    };

    useEffect(() => {
        fetchAvatar();
    }, []);

    return (
        <Box component='div' sx={{ ...(!pathnames[getIndexPathPageMain()] && getStyleBox()) }}>
            <OrganizationInfoView />
            <Menu />
            <Box component='div'>
                {props.children}
            </Box>
        </Box>
    );
}