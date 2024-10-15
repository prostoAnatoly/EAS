import * as React from 'react';
import { Link } from 'react-router-dom';
import { useCallback } from 'react';
import { useNavigate } from 'react-router';
import { AppBar, Avatar, Backdrop, IconButton, Menu, Toolbar, Tooltip, MenuItem, Box } from '@mui/material';
import { AccountCircle, ExitToApp } from '@mui/icons-material';
import { usePathnames, useStateSmart } from '../../../../utils/hooks';
import { LoadingProgress, SnackBarError, MenuItemDef } from '../../../../kit/kit';
import { getFullnameShort, getSurNameAndNameShort } from '../../../../utils/fullname-utils';
import { deepPurple } from '@mui/material/colors';
import { Brand } from '../brand/Brand';
import { RoutingByShell } from '../../config/routingByShell';
import { FullName } from '../../../../sharedModels/FullName';

interface MainMenuProps {
    menuItems: MenuItemDef[];
    rootApp: string;
    userFullName?: FullName;
    avatarUrl?: string;
    actionLogout: () => Promise<void>
}

export default function MainMenu(props: MainMenuProps) {
    const [anchorAccountIconButton, setAnchorEl] = React.useState(null);

    const navigate = useNavigate();
    const [loadingInfo, setIsLoading] = useStateSmart({ isLoading: false, errorMessage: '' });
    const pathnames = usePathnames();

    const logout = useCallback(async () => {
        if (loadingInfo.isLoading) { return };

        setIsLoading({ isLoading: true, errorMessage: '' });

        await props.actionLogout()
            .then(() => {
                setIsLoading({ isLoading: false, errorMessage: '' });

                navigate(RoutingByShell.LOGIN);
            })
            .catch((error: Error) => {
                setIsLoading({ isLoading: false, errorMessage: 'Выход не осуществлён: ' + error.message });
            });
    }, [loadingInfo, navigate]);

    const handlerCloseMessage = (): void => {
        setIsLoading({ isLoading: false, errorMessage: '' });
    };

    const getUserName = (): string => {
        return getFullnameShort(props.userFullName);
    };

    const getUrlToMenuItem = (): string => {
        if (props.rootApp === '/') {
            return `${props.rootApp}${pathnames[1]}`;
        }

        return `${props.rootApp}${pathnames[2]}`;
    };

    const handleMenu = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const logoutByMenuItem = () => {
        handleClose();
        logout();
    };

    const getAvatarControl = () => {

        if (props.avatarUrl) {
            return (<Avatar alt={getFullnameShort(props.userFullName)} src={props.avatarUrl} />)
        }

        const short = getSurNameAndNameShort(props.userFullName);
        if (short) {
            return (<Avatar sx={{
                color: (theme) => theme.palette.getContrastText(deepPurple[500]),
                backgroundColor: deepPurple[500],
            }} >{short}</Avatar>);
        }

        return (<AccountCircle />);
    };

    const getUserControl = () => {
        return (<Box component='div' sx={{
            marginLeft: (theme) => theme.spacing(0.5),
            textAlign: 'left',
        }}>
            <Box component='div' sx={{
                marginRight: (theme) => theme.spacing(1),
                fontSize: 'var(--EAS-FONT-SIZE-MD-TEXT)',
            }}>{getUserName()}</Box>
        </Box>);
    };

    const getMenuItemsControl = () => {
        return (props.menuItems.map(item => {
            return (
                <MenuItem key={item.to} selected={getUrlToMenuItem() === item.to}
                    sx={{
                        minHeight: '64px !important',
                    }}
                    classes={{
                        selected: 'menu-item-selected',
                        root: 'menu-item-root',
                    }}
                    component={Link} to={item.to}>{item.text}</MenuItem>)
        }));
    };

    return (
        <>
            <Backdrop sx={{
                zIndex: (theme) => theme.zIndex.drawer + 1,
                color: '#fff',
            }} open={loadingInfo.isLoading}>
                <LoadingProgress text={'Осуществляем выход из системы. Пожалуйста, ожидайте...'} />
            </Backdrop>
            <SnackBarError
                message={loadingInfo.errorMessage}
                handleCloseMessage={handlerCloseMessage}
            />
            <AppBar position='static' sx={{
                backgroundColor: 'rgba(50,150,100,1)',
            }}>
                <Toolbar sx={{paddingLeft: '0!important'}}>
                    <Brand />
                    {getMenuItemsControl()}
                    <Box component='div' sx={{
                        margin: '0 0 0 auto',
                    }}>
                        <IconButton
                            sx={{
                                '&:hover': {
                                    backgroundColor: 'var(--EAS-BACKGROUND-COLOR-SELECTED) !important',
                                    boxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 15%) inset',
                                    WebkitBoxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 15%) inset',
                                    MozBoxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 15%) inset',
                                },
                            }}
                            size='medium'
                            aria-controls='menu-account-appbar'
                            aria-haspopup='true'
                            onClick={handleMenu}
                            color='inherit'
                            disabled={loadingInfo.isLoading}
                        >
                            {getAvatarControl()}
                            {getUserControl()}
                        </IconButton>
                        <Tooltip title='Выйти'>
                            <IconButton sx={{
                                '&:hover': {
                                    backgroundColor: 'var(--EAS-BACKGROUND-COLOR-SELECTED) !important',
                                    boxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 15%) inset',
                                    WebkitBoxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 15%) inset',
                                    MozBoxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 15%) inset',
                                },
                            }}
                                aria-label='exit'
                                onClick={logout}
                                disabled={loadingInfo.isLoading}
                                color='inherit'>
                                <ExitToApp />
                            </IconButton>
                        </Tooltip>            
                        <Menu
                            id='menu-account-appbar'
                            anchorEl={anchorAccountIconButton}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'left',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorAccountIconButton)}
                            onClose={handleClose}
                        >
                            <MenuItem onClick={logoutByMenuItem}>Выход</MenuItem>
                        </Menu>
                    </Box>
                </Toolbar>
            </AppBar>
        </>
    );
}