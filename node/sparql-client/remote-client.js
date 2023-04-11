import SparqlClient from 'sparql-http-client';

const endpointUrl = 'http://localhost:2021/sparql'

const query = `
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>
PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>
PREFIX vocab: <http://localhost:2021/resource/vocab/>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX map: <http://localhost:2021/resource/#>
PREFIX db: <http://localhost:2021/resource/>

SELECT DISTINCT * WHERE {
    ?s rdf:type vocab:users;
       vocab:users_name ?name ;
       vocab:users_surname ?surname .
}
LIMIT 10`;

export async function fetchUsers() {
    const client = new SparqlClient({ endpointUrl });
    const res = await client.query.select(query);

    res.on('data', event => {
      console.log(`${event['name'].value} ${event['surname'].value}`);
    });
}
