import * as React from 'react';

const getStyle = () => {
    return {
        color: '#7a59d9',
        height: '1px',
        backgroundColor: '#7a59d9',
        border: '0 none',
    };
};

export function ColorHr() {
    return (<hr style={getStyle()} />);
}