const rows = 
  [ { key: 'row_1', column: 'account_data:name', timestamp: Date.now(), $: 'Gennadiy' }
  , { key: 'row_2', column: 'account_data:name', timestamp: Date.now(), $: 'Pavlo' }
  , { key: 'row_3', column: 'account_data:name', timestamp: Date.now()+1, $: 'Mykyta' }
  ]

const names = ['Andriy', 'Oleh', 'Petro', 'Ostap', 'Pavlo', 'Mykhailo', 'Ievhen', 'Oleksandr', 'Olha', 'Maryna', 'Olena', 'Kateryna', 'Oksana'];

const getRandomName = () => names[Math.floor(Math.random()*names.length)];


const getRows = () => {
  const rows = [];

  for (let i = 0; i < 1000; i++) {
    rows.push({ key: `row_${i}`, column: 'account_data:name', timestamp: Date.now(), $: getRandomName() });
  }

  return rows;
};

const hbase = require('hbase')
client = hbase({ host: '127.0.0.1', port: 32778 }); //8080

client
  .table('Account')
  .row()
  .put(getRows(), function (err, success) {
    if (err) {
      console.log('failed to insert rows');
      return;
    }

    console.log('inserted rows: ' + success);
  });
