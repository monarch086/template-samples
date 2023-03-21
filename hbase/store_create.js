const hbase = require('hbase');
client = hbase({ host: '127.0.0.1', port: 32778 });

const TABLE_NAME = 'products';

client
    .table(TABLE_NAME)
    .create('col_family', function (error, success) {
        console.info('Table created: ' + (success ? 'yes' : 'no'))
    });