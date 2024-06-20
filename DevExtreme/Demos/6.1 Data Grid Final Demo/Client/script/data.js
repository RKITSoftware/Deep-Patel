const studentAPIUrl = "https://localhost:7026/api/CLSTU01";
const cityAPIUrl = "https://localhost:7026/api/CLCTY01";
const educationAPIUrl = "https://localhost:7026/api/CLEDC01";

let studentArray;

function CityCustomStore() {
  return new DevExpress.data.CustomStore({
    byKey: function (key) {
      return $.ajax({
        type: "GET",
        url: `${cityAPIUrl}/${key}`,
        dataType: "json",
        success: function (response) {
          return response;
        },
      });
    },
    load: function () {
      return $.ajax({
        type: "GET",
        url: cityAPIUrl,
        dataType: "json",
        success: function (response) {
          return response;
        },
        error: function (err) {
          console.log("City API Error :- ", err);
        },
      });
    },
    errorHandler: function (err) {
      console.log("City Custom Store Error :-", err);
    },
  });
}

function StudentCustomStore() {
  return new DevExpress.data.CustomStore({
    key: "U01F01",
    load: function () {
      return $.ajax({
        type: "GET",
        url: studentAPIUrl,
        dataType: "json",
        success: function (response) {
          return response;
        },
        error: function (err) {
          console.log("Student API Error :-", err);
        },
      });
    },
    errorHandler: function (err) {
      console.log("Student Custom Store Error :-", err);
    },
  });
}

StudentCustomStore()
  .load()
  .then(function (data) {
    studentArray = data;
  });

function GetEducationDetails(id) {
  return new DevExpress.data.CustomStore({
    load: function () {
      return $.ajax({
        type: "GET",
        url: `${educationAPIUrl}/${id}`,
        dataType: "json",
        success: function (response) {
          return response;
        },
        error: function (err) {
          console.log("Education API Error :- ", err);
        },
      });
    },
    errorHandler: function (err) {
      console.log("Education Custom Store Error :- ", err);
    },
  });
}

async function getEducation(id) {
  let educationArray;
  await GetEducationDetails(id)
    .load()
    .then(function (data) {
      educationArray = data;
    });

  return educationArray;
}

export {
  CityCustomStore,
  GetEducationDetails,
  getEducation,
  studentArray,
  studentAPIUrl,
  StudentCustomStore,
};
