// var settings = {
//   url: "https://localhost:44313/api/CLProduct/GetProductV2",
//   method: "GET",
//   timeout: 0,
//   headers: {
//     Cookie: "MyAuth=ZHAzNjc2OTkxOkBEZWVwMjUxMw%3D%3D",
//   },
// };

// $.get(settings).done(function (response) {
//   console.log(response);
// });

fetch("http://localhost:59592/api/CLProduct/GetProductV2", {
  credentials: "include",
})
  .then((response) => response.json())
  .then((data) => console.log(data));
