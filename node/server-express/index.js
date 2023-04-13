const app = require("express")();

app.get("/", (req, res) => res.send("hello from a lightweight container!"))

app.get("/home", (req, res) => res.send("Welcome to Home page!"))

app.post("/home", (req, res) => {
    postHandler();
    res.send("Welcome to Home page (POST)!");
})

app.listen(9999, () => console.log("Listening on 9999"))

function postHandler() {
    console.log('POST triggered');
}