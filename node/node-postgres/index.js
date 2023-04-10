import pg from 'pg';

const client = new pg.Client({
    user: 'postgers_admin',
    host: 'dis-postgres.cgiy8on6k3vt.eu-central-1.rds.amazonaws.com',
    database: 'postgres',
    password: '',
    port: 5432
});

async function selectAllCities() {
    await client.connect();

    const res = await client.query('SELECT * FROM cities');
    console.table(res.rows);
    await client.end();
}

selectAllCities();