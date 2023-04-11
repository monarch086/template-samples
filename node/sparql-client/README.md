# SPARQL Client Example

This is an example of starting [D2RQ](http://d2rq.org/) server and connecting to it with SPARQL HTTP client.

## Commands

### Creating SQL database

````SQL
create table cities(id serial PRIMARY KEY, name varchar(200));

INSERT INTO cities (name) VALUES ('Kyiv'), ('Kharkiv'), ('Lviv'), ('Odessa'), ('Mariupol'), ('Mykolaiv'), ('Rivne');
````

### Generate mapping for data model

````bash
generate-mapping [-u user] [-p password] [-d driver]
        [-l script.sql] [--[skip-](schemas|tables|columns) list]
        [--w3c] [-v] [-b baseURI] [-o outfile.ttl]
        jdbc:postgresql://localhost:5432/postgres

generate-mapping -o mapping.ttl -d org.postgresql.Driver -u postgres -p postgres jdbc:postgresql://localhost:5432/postgres
````

### Start D2RQ server

````bash
d2r-server mapping.ttl
````

In case of this issue: 'Invalid version number: Version number may be negative or greater than 255' update icu4j.jar with the latest version (<https://icu.unicode.org/>).
<https://stackoverflow.com/questions/64040255/invalid-version-number-version-number-may-be-negative-or-greater-than-255>

````
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
LIMIT 10
````
