import * as React from 'react';
import { Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { RoutingByShell } from '../../config/routingByShell';

export function Brand() {
    return (<Link to={RoutingByShell.ROOT} style={{ textDecoration: 'none', }}>
        <Typography variant="h6" sx={{
            paddingRight: (theme) => theme.spacing(1),
            paddingLeft: (theme) => theme.spacing(1),
            minHeight: '64px',
            display: 'inline-flex',
            alignItems: 'center',
            backgroundColor: 'rgba(120, 220, 220, 255) !important',
            boxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 10%) inset',
            WebkitBoxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 10%) inset',
            MozBoxShadow: '0 2px 8px rgb(0 0 0 / 40%), 0 0 20px rgb(0 0 0 / 10%) inset',
        }}>
            EAS
        </Typography>
    </Link>);
}