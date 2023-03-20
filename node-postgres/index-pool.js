const { Pool } = require('pg');
 
const pool = new Pool({
    user: 'postgers_admin',
    host: 'dis-postgres.cgiy8on6k3vt.eu-central-1.rds.amazonaws.com',
    database: 'postgres',
    password: '',
    port: 5432
});

pool
  .query('SELECT * FROM cities')
  .then((res) => console.table(res.rows))
  .catch((err) =>
    setImmediate(() => {
      throw err
    })
);

pool
  .query('SELECT * FROM cities WHERE name LIKE $1', ['My%'])
  .then((res) => console.table(res.rows))
  .catch((err) =>
    setImmediate(() => {
      throw err
    })
);
