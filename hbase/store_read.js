const hbase = require('hbase');
client = hbase({ host: '127.0.0.1', port: 32772 });

const toData = (rows) => {
    const map = new Map();

    rows.forEach(row => {
        if (map.has(row.key)) {
            map.get(row.key)[row.column] = row.$;
        } else {
            map.set(row.key, {});
            map.get(row.key)['row key'] = row.key;
            map.get(row.key)[row.column] = row.$;
        }
    });

    return Array.from(map, ([name, value]) => (value));
}

const calculateTotal = (data) => data.reduce((acc, curr) => acc + curr['col_family:price'] * curr['col_family:quantity'], 0);

client.table('products').scan(
    {
        startRow: 'row_1',
        maxVersions: 1,
    },
    (err, rows) => {
        const data = toData(rows);
        console.table(data);

        const total = calculateTotal(data);
        console.log('Total price: ', total);
    }
);
