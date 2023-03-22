const redis = require('redis');
const subscriber = redis.createClient({
    // url: 'redis://alice:pass1@localhost:6379'
    url: 'redis://localhost:6379'
});
const publisher = redis.createClient({
    url: 'redis://localhost:6379'
});

async function connect() {
  await subscriber.connect();
  await publisher.connect();
}

async function subscribe() {
  const listener = (message, channel) => console.log(`Received message from ${channel}: ${message}`);
  await subscriber.subscribe('my_channel', listener);
}

function publish() {
  let i = 0;
  setInterval(async () => {
    await publisher.publish('my_channel', `${i++}: Hello from Redis Pub/Sub!`);
  }, 1000);
}

async function main() {
  await connect();
  await subscribe();
  publish();

  //subscriber.quit();
  //publisher.quit();
}

main();