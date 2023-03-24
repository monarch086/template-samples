const hbase = require('hbase')
client = hbase({ host: '127.0.0.1', port: 32772 });

const TABLE_NAME = 'products';

const products = ['bread', 'butter', 'carrot', 'onion', 'pizza', 'strawberry', 'apple', 'meat', 'milk', 'juice'];

const stores = ['A', 'B', 'C'];

const getRandom = (items) => items[Math.floor(Math.random() * items.length)];

const getRandomFloat = (max) => Math.random() * max;

const getRandomInteger = (max, min) => {
    const val = Math.floor(Math.random() * max);

    return val < min ? min : val;
}

const prependZero = (value) => value < 10 ? '0' + value : value;

const getRandomDate = () => {
    const month = prependZero(getRandomInteger(12, 0));
    const day = prependZero(getRandomInteger(28, 1));
    const hour = prependZero(getRandomInteger(23, 0));

    const date = new Date(2023, month, day, hour);

    if (date instanceof Date && !isNaN(date)) {
        return date;
    }

    console.error(`2023-${month}-${day}T${hour}:24:00`);
}

const getRows = () => {
  const rows = [];

  for (let i = 0; i < 100000; i++) {
    rows.push({ key: `row_${i}`, column: 'col_family:date', timestamp: Date.now(), $: getRandomDate() });
    rows.push({ key: `row_${i}`, column: 'col_family:store', timestamp: Date.now(), $: getRandom(stores) });
    rows.push({ key: `row_${i}`, column: 'col_family:product', timestamp: Date.now(), $: getRandom(products) });
    rows.push({ key: `row_${i}`, column: 'col_family:price', timestamp: Date.now(), $: getRandomFloat(50).toString() });
    rows.push({ key: `row_${i}`, column: 'col_family:quantity', timestamp: Date.now(), $: getRandomInteger(100, 1).toString() });
  }

  return rows;
};

client
  .table(TABLE_NAME)
  .row()
  .put(getRows(), function (err, success) {
    if (err) {
      console.log('failed to insert rows');
      return;
    }

    console.log('inserted rows: ' + success);
  });