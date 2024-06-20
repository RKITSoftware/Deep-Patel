$(function () {
  let commentsDataSource = new DevExpress.data.CustomStore({
    load: function () {
      return $.ajax({
        type: "GET",
        url: "https://jsonplaceholder.typicode.com/posts",
        dataType: "json",
        success: function (response) {
          return response;
        },
        error: function (error) {
          return error;
        },
      });
    },
    errorHandler: function (error) {
      console.log("Error", error);
    },
  });

  $("#dataBindingDG").dxDataGrid({
    dataSource: commentsDataSource,
  });
});
