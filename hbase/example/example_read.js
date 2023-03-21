const hbase = require('hbase');
client = hbase({ host: '127.0.0.1', port: 32778 }); //8080

client.table('Account').scan(
	{
		startRow: 'row_1',
		maxVersions: 1,
	},
	(err, rows) => console.info(rows)
);
