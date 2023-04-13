import http from "http"

const hostname = "127.0.0.1";
const port = 9000;

const server = http.createServer();

server.on("request", (req, res) => 
    {
        console.log("request: ", req.url);

        res.writeHead(200, {'Content-Type': 'application/json'});
        //res.write("{\"text\": \"hello from NodeJs\"}");
        
        const obj = {text: "hello from NodeJs"};
        res.write(JSON.stringify(obj));
        
        res.end();
    });

server.listen(port, hostname, () => {
    console.log(`Server running at http://${hostname}:${port}/`);
})