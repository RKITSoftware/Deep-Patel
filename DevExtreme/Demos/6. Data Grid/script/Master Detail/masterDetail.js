$(function () {
  const url = "https://666c098749dbc5d7145c4fdb.mockapi.io/users";
  const ordersUrl = "https://666c098749dbc5d7145c4fdb.mockapi.io/orders";

  let dataSource = new DevExpress.data.CustomStore({
    load: function () {
      return $.ajax({
        type: "GET",
        url: url,
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

  const columns = [
    {
      dataField: "id",
      dataType: "numer",
      caption: "Id",
    },
    {
      dataField: "name",
      dataType: "string",
      caption: "Name",
    },
    {
      dataField: "gender",
      dataType: "string",
      caption: "Gender",
    },
    {
      caption: "Address",
      columns: [
        {
          dataField: "city",
          dataType: "string",
          caption: "City",
        },
        {
          dataField: "state",
          dataType: "string",
          caption: "State",
        },
      ],
    },
    {
      dataField: "percentage",
      dataType: "number",
      caption: "Passing Percentage",
    },
  ];

  $("#masterDetailDG").dxDataGrid({
    dataSource: dataSource,
    keyExpr: "id",
    columns: columns,
    columnAutoWidth: true,
    showBorders: true,
    masterDetail: {
      enabled: true,
      autoExpandAll: false,
      template(container, options) {
        const currentUserData = options.data;

        $("<div>")
          .addClass("master-detail-header")
          .text(`${currentUserData.name}'s Orders:- `)
          .appendTo(container);

        $("<div>")
          .dxDataGrid({
            dataSource: new DevExpress.data.CustomStore({
              load: function () {
                return $.ajax({
                  type: "GET",
                  url: `${ordersUrl}/${options.data.id}`,
                  dataType: "json",
                });
              },
            }),
            key: "id",
          })
          .appendTo(container);
      },
    },
  });
});
