import { fetchCities } from './local-client.js';
import { fetchUsers } from './remote-client.js';

console.log('Data from local PostgreSQL instance:');
fetchCities();

console.log('Data from remote PostgreSQL instance:');
fetchUsers();
