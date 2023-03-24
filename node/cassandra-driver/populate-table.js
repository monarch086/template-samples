const { Client, auth } = require('cassandra-driver');
const client = new Client({
    contactPoints: ['127.0.0.1:9042'],
    localDataCenter: 'datacenter1',
    keyspace: 'tutorial',
    authProvider: new auth.PlainTextAuthProvider('cassandra', 'cassandra'),
});

client.connect();

const tableName = 'tutorial.products';

const query = `INSERT INTO ${tableName} (id, date, store, product, price, quantity)
VALUES (?, ?, ?, ?, ?, ?);`;

client.execute(query, [1, new Date(), 'A', 'cheese', '100', '0.7'])
  .then(result => console.log('Inserted: %s', result))
  .catch(err => console.log(err));