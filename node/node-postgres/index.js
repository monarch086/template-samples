import pg from 'pg';
import { DB_CONFIG } from './config.js';

const client = new pg.Client({
    user: DB_CONFIG.user,
    host: DB_CONFIG.host,
    database: DB_CONFIG.database,
    password: DB_CONFIG.password,
    port: DB_CONFIG.port
});

async function selectAllCities() {
    await client.connect();

    const res = await client.query('SELECT * FROM cities');
    console.table(res.rows);
    await client.end();
}

selectAllCities();