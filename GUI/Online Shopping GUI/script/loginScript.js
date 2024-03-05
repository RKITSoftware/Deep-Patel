document.getElementById("btnLogin").addEventListener("click", function () {
  let username = document.getElementById("username").value;
  let password = document.getElementById("password").value;

  // API endpoint URL
  var apiUrl = `http://localhost:59592/Login?username=${username}&password=${password}`;

  fetch(apiUrl, {
    credentials: "include",
  }).then((resp) => {
    if (resp.ok) {
      // let encodedAuthToken = btoa(`${username}:${password}`);
      // setCookie("MyAuth", encodedAuthToken, 20);
      // confirm("Login sucessfully.");
      window.location.href = "/products.html";
    } else if (resp.status == 404) {
      alert("Credentials are invalid");
    }
  });

  // let xml = new XMLHttpRequest();
  // xml.withCredentials = true;
  // xml.open("GET", apiUrl);
  // xml.send();
  // xml.onreadystatechange = function () {
  //   if (xml.readyState == 4 && xml.status == 200) {
  //     confirm("login");
  //   }
  // };
});

function setCookie(cName, cValue, expMinutes) {
  let date = new Date();
  date.setTime(date.getTime() + expMinutes * 60 * 1000);
  const expires = "expires=" + date.toUTCString();
  document.cookie = cName + "=" + cValue + "; " + expires + "; path=/";
}

document.getElementById("form1").addEventListener("submit", function (e) {
  e.preventDefault();
});
