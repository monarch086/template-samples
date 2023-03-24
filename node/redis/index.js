const redis = require('redis');

const client = redis.createClient({
    //url: 'redis://alice:pass1@localhost:6379'
    url: 'redis://localhost:6379'
});

client.on('error', err => console.log('Redis Client Error', err));

async function main() {
    await client.connect();

    await client.set('key1', 'test value');

    const value = await client.get('key1');
    console.log('Received value: ', value);

    await client.disconnect();
}

main();