const path = require('path');
const CopyPlugin = require('copy-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const {CleanWebpackPlugin} = require('clean-webpack-plugin');
const Dotenv = require('dotenv-webpack');

const config = {
    entry : [
        './src/entry.tsx',
    ],

    output : {
        publicPath: '/', // ассеты со слэшем в начале
        path       : path.resolve(__dirname, 'dist'),
        filename   : '[name].[contenthash].js',
    },

    resolve : {
        extensions : [
            '.tsx',
            '.ts',
            '.js',
        ],
    },

    devServer : {
        contentBase        : './dist',
        port               : 8080,
        historyApiFallback: true, // @see https://ui.dev/react-router-cannot-get-url-refresh/
        // compress    : true,
        // hot         : true,
    },
    devtool   : "source-map",

    optimization : {
        runtimeChunk : 'single',
        splitChunks  : {
            cacheGroups : {
                vendor : {
                    test   : /[\\/]node_modules[\\/]/,
                    name   : 'vendors',
                    chunks : 'all',
                },
            },
        },
    },

    module  : {
        rules : [
            {
                test : /\.css$/,
                use  : [
                    MiniCssExtractPlugin.loader,
                    {
                        loader : 'css-loader',
                        options : {importLoaders : 1}
                    },
                    'postcss-loader',
                ],
            },
            {
                test    : /\.ts(x)?$/,
                // loader  : 'ts-loader',
                loader  : 'esbuild-loader', // @see https://github.com/privatenumber/esbuild-loader
                options : {
                    loader : 'tsx',
                    target : 'es2015',
                },
                exclude : /node_modules/,
            },
            {
                test : /\.svg$/,
                // use  : 'file-loader',
                use  : [{loader : 'url-loader', options : {mimetype : 'image/svg'}}],
            },
            {
                test : /\.png$/,
                use  : [{loader : 'url-loader', options : {mimetype : 'image/png'}}],
            },
            {
                test : /\.styl$/,
                use  : [MiniCssExtractPlugin.loader, 'css-loader'],
            },
        ],
    },
    plugins: [
        new CopyPlugin({
            patterns : [{from : 'src/public'}],
        }),
        new HtmlWebpackPlugin({
            appMountId : 'app',
            template   : 'src/entry.html',
            filename   : 'index.html',
            minify     : {
                collapseWhitespace        : true,
                removeComments            : true,
                removeRedundantAttributes : true,
                useShortDoctype           : true,
            },
        }),
        new MiniCssExtractPlugin(),
        new CleanWebpackPlugin(),
    ],

};

module.exports = (env, argv) => {
    if (argv.hot) {
        // не можем юзать 'contenthash', когда hot reloading включен
        config.output.filename = '[name].[hash].js';
    }

    const mode = env.mode;
    let envPath = '.env';
    if (mode === 'production') {
        envPath = '.env.production';
    }
    else if (mode === 'preproduction') {
        envPath = '.env.preproduction';
    }

    // Путь до сайта
    const publicPath = env.publicPath || '';

    config.output.publicPath += publicPath;

    config.plugins.push(new Dotenv({
        path: envPath
    }));

    return config;
};