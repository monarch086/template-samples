import http from "http"

const hostname = "127.0.0.1";
const port = 8080;

const server = http.createServer();

server.on("request", (req, res) => 
    {
        console.log("request: ", req.url);

        res.writeHead(200, {'Content-Type': 'text/plain'});
        res.write("hello from NodeJs");
        res.end();
    });

server.listen(port, hostname, () => {
    console.log(`Server running at http://${hostname}:${port}/`);
})