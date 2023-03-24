const hbase = require('hbase');
client = hbase({ host: '127.0.0.1', port: 32778 }); //8080

const getRows = () => {
	const rows = [];
  
	for (let i = 0; i < 1000; i++) {
	  rows.push(`row_${i}`);
	}
  
	return rows;
  };

client.table('Account')
.row()
.delete(
	['row_1'],
	(err, success) => console.log(success)
);
