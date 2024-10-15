'use strict';
const express = require('express');
const path = require('path');

const app = express();

// add middleware
app.use(express.static('dist'));

// Allows you to set port in the project properties.
app.set('port', process.env.PORT || 1488);

console.log('port: ' + app.get('port'));

app.listen(app.get('port'), function () {
    console.log('listening');
});

app.get('*', (req, res) => {
    //console.log(req.originalUrl);
    res.header('Cache-Control', 'no-cache');
    res.sendFile(path.resolve(__dirname, 'dist', 'index.html'));
});