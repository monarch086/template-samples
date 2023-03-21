const { Client, auth } = require('cassandra-driver');
const client = new Client({
    contactPoints: ['127.0.0.1:9042'],
    localDataCenter: 'datacenter1', // here is the change required
    keyspace: 'tutorial',
    //authProvider: new auth.PlainTextAuthProvider('cassandra', 'cassandra'),
});

client.connect();

const tableName = 'tutorial.products';

const query = `
CREATE TABLE IF NOT EXISTS ${tableName} (
    id int,
    date timestamp,
    store text,
    product text,
    price decimal,
    quantity decimal,
    PRIMARY KEY (id, date)
);`;

client.execute(query, [])
  .then(result => console.log(`Created ${tableName} table.`));

