import { SxProps, Theme } from '@mui/material';

type Style<Type> = {
    [Property in keyof Type]: SxProps<Theme>;
};

export function createMuiStyle<T>(style: Style<T>): Style<T> {
    return style;
}