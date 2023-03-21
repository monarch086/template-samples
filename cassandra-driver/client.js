const { Client, auth } = require('cassandra-driver');
const client = new Client({
    contactPoints: ['127.0.0.1:9042'],
    localDataCenter: 'datacenter1',
    keyspace: 'tutorial',
    //authProvider: new auth.PlainTextAuthProvider('cassandra', 'cassandra'),
});

client.connect();

const query = 'SELECT * FROM tutorial.posts;';

client.execute(query, [])
  .then(result => console.log('Post: %s', result.rows[0].content));
