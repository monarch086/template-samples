const hbase = require('hbase');
client = hbase({ host: '127.0.0.1', port: 32778 });

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

client.table('products').scan(
    {
        startRow: 'row_1',
        maxVersions: 1,
        filter: {
                "op":"MUST_PASS_ALL","type":"FilterList","filters":[
                    {"op":"EQUAL","type":"ValueFilter","comparator":{"value":"apple","type":"BinaryComparator"}},                 //<==============
                    //{"op":"EQUAL","type":"RowFilter","comparator":{"value":"row_15","type":"BinaryComparator"}},                    //<===============
                    //{"op":"EQUAL","type":"QualifierFilter","comparator":{"value":"product","type":"BinaryComparator"}},             //<==============
                    //{"op":"EQUAL","type":"FamilyFilter","comparator":{"value":"col_family","type":"BinaryComparator"}},             //<==============
                    //{"op":"EQUAL","type":"SingleColumnValueFilter","family":"col_family","qualifier":"product","latestVersion": true,"comparator":{"value":"apple","type":"BinaryComparator"}},
                  ]
    }
    },
    (err, rows) => {
        const data = toData(rows);
        console.table(data);
    }
);

// client.table('products').scan(
//     {
//         startRow: 'row_1',
//         maxVersions: 1,
//     },
//     (err, rows) => {
//         const data = toData(rows);
//         const startDate = new Date(2023, 0, 01);
//         const endDate = new Date(2023, 0, 30);

//         //const filtered = data.filter(i => i['col_family:product'] === 'apple');
//         //const filtered = data.filter(i => new Date(i['col_family:date']) >= startDate && new Date(i['col_family:date']) <= endDate);
//         const filtered = data.filter(i => i['col_family:store'] === 'C');

//         console.table(filtered);

//         const total = calculateTotal(filtered);
//         console.log('Total price: ', total);
//     }
// );