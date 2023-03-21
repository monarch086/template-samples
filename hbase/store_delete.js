const hbase = require('hbase');
client = hbase({ host: '127.0.0.1', port: 32778 });

const TABLE_NAME = 'products';

client.table(TABLE_NAME).exists((error, success) => {
    console.info('Table exists: ' + (success ? 'yes' : 'no'));
});

client.table(TABLE_NAME).delete((error, success) => {
    console.info('Table deleted: ' + (success ? 'yes' : 'no'));
});
