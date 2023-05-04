import { MongoClient } from 'mongodb';
import { DB_CONFIG_LOCAL, getConnectionString } from './config.js';

const connString = getConnectionString(DB_CONFIG_LOCAL);
const client = new MongoClient(connString);
 
async function run() {
    try {
        await client.connect();
        const db = client.db("usersdb");
        const collection = db.collection("users");
        const user = {name: "Tom 3", age: 29};
        const result = await collection.insertOne(user);
        console.log(result);
        console.log(user);
    }catch(err) {
        console.log(err);
    } finally {
        await client.close();
    }
}
run().catch(console.error);