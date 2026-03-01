document.querySelector("#load-cities-btn").addEventListener("click",
    async function () {
        var response = await fetch("cities-list", { method: "GET" });
        var responseBody = await response.text();
        document.querySelector("#cities-list").innerHTML = responseBody;
    })