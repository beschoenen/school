module.exports = function(app) {
    require('./api/_index')(app);
    app.use('/', require('./web')(app));

    app.use(function(req, res) {
        res.status(404);
        res.type('txt').send('404 Not found');
    });
};