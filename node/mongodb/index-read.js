import { MongoClient, ObjectId } from 'mongodb';
import { DB_CONFIG_CLOUD, DB_CONFIG_LOCAL, getConnectionString } from './config.js';

const connString = getConnectionString(DB_CONFIG_LOCAL);
const client = new MongoClient(connString);

async function run() {
    try {
        await client.connect();
        const db = client.db("usersdb");
        const collection = db.collection("users");
        const count = await collection.countDocuments();
        console.log(`В колекції users ${count} документ/ів`);

        const results = await collection.find().toArray();
        console.table(results);

        const firstDocumentId = results[0]._id.toString();
        console.log('First document id = ', firstDocumentId);

        var objectId = new ObjectId(firstDocumentId);
        const firstObject = await collection.findOne({_id: objectId});
        console.table(firstObject);
    } catch(err) {
        console.log(err);
    } finally {
        await client.close();
    }
}
run().catch(console.error);