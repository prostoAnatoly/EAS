import { Box } from '@mui/material';
import { observer } from 'mobx-react';
import * as React from 'react';
import { createMuiStyle } from '../../../../utils/mui-utils';
import { organizationsStore } from '../../../organizations/stores/OrganizationsStore';

const style = createMuiStyle({
    boxSelectedOrgainzation: {
        backgroundColor: 'mediumpurple',
        textAlign: 'right',
        fontSize: 'var(--EAS-FONT-SIZE-LG-TEXT)',
        color: 'white',
        padding: (theme) => theme.spacing(0.5),
    },
    boxUnSelectedOrgainzation: {
        backgroundColor: 'mediumpurple',
        textAlign: 'center',
        fontSize: 'var(--EAS-FONT-SIZE-LG-TEXT)',
        color: 'white',
        padding: (theme) => theme.spacing(0.5),
    },
});

const OrganizationInfoView = () => {
    const hasSelectedOgranization = () => {
        return organizationsStore.selectedOgranization ? true : false;
    };

    const getContentView = () => {
        if (hasSelectedOgranization()) {
            return (<>Организация: <Box component='b'>{organizationsStore.selectedOgranization?.name}</Box></>);
        }

        if (organizationsStore.ogranizations?.length) {
            return (<><Box component='b'>Выберите организацию</Box></>);
        }

        return (<><Box component='b'>Добавь организацию. Начни работать</Box></>);
    };

    return (<Box component='div'
        sx={hasSelectedOgranization() ? style.boxSelectedOrgainzation : style.boxUnSelectedOrgainzation}>
        {getContentView()}
    </Box>);
};

export default observer(OrganizationInfoView);