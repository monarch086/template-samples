import fetch from "node-fetch";

function fetchData() {
    const apiUrl = "http://localhost:9000";
  
    fetch(apiUrl)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Не вдалося завантажити дані");
        }
        return response.json();
      })
      .then((data) => {
        console.log(data);
      })
      .catch((error) => {
        console.error("Помилка:", error);
      });
  }

  fetchData();