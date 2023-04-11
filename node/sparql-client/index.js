const SparqlClient = require('sparql-http-client')

const endpointUrl = 'http://localhost:2020/sparql'

const query = `
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>
PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>
PREFIX vocab: <http://localhost:2020/resource/vocab/>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX map: <http://localhost:2020/resource/#>
PREFIX db: <http://localhost:2020/resource/>

SELECT DISTINCT * WHERE {
    ?s rdf:type vocab:cities;
       vocab:cities_name ?o .
}
LIMIT 10`;

async function fetchData() {
    const client = new SparqlClient({ endpointUrl });
    const res = await client.query.select(query);

    res.on('data', event => {
      // console.log(event);

      console.log(event['o'].value);
    });
}

fetchData();
