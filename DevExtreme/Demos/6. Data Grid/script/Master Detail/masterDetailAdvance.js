window.jsPDF = window.jspdf.jsPDF;

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

  const innerColumn = [
    {
      dataField: "id",
      dataType: "number",
      caption: "Product Id",
    },
    {
      dataField: "productName",
      dataType: "string",
      caption: "Product Name",
    },
    {
      dataField: "department",
      dataType: "string",
      caption: "Category",
    },
  ];

  $("#masterDetailAdvDG").dxDataGrid({
    dataSource: dataSource,
    keyExpr: "id",
    columns: columns,
    columnAutoWidth: true,
    showBorders: true,
    export: {
      enabled: true,
      allowExportSelectedData: true,
    },
    selection: {
      mode: "multiple",
    },
    groupPanel: {
      visible: true,
    },
    onExporting: function (e) {
      const doc = new jsPDF();

      DevExpress.pdfExporter
        .exportDataGrid({
          jsPDFDocument: doc,
          component: e.component,
          indent: 5,
        })
        .then(() => {
          doc.save("orders.pdf");
        });

      // var workbook = new ExcelJS.Workbook();
      // var worksheet = workbook.addWorksheet("Main sheet");
      // DevExpress.excelExporter
      //   .exportDataGrid({
      //     worksheet: worksheet,
      //     component: e.component,
      //     customizeCell: function (options) {
      //       var excelCell = options;
      //       excelCell.font = { name: "Arial", size: 12 };
      //       excelCell.alignment = { horizontal: "left" };
      //     },
      //   })
      //   .then(function () {
      //     workbook.xlsx.writeBuffer().then(function (buffer) {
      //       saveAs(
      //         new Blob([buffer], { type: "application/octet-stream" }),
      //         "DataGrid.xlsx"
      //       );
      //     });
      //   });
      // e.cancel = true;
    },
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
            columns: innerColumn,
            export: {
              enabled: true,
            },
            onExporting(innerE) {
              const doc2 = new jsPDF();

              console.log(doc2);

              DevExpress.pdfExporter
                .exportDataGrid({
                  jsPDFDocument: doc2,
                  component: innerE.component,
                  indent: 5,
                })
                .then(() => {
                  innerE.component("");
                  doc2.save("orders1.pdf");
                });
            },
            key: "id",
            masterDetail: {
              enabled: true,
              autoExpandAll: false,
              template(innerContainer, innerOptions) {
                const { data } = innerOptions;

                const markup = `<div>
                <h6>Price</h6>
                <p>${data.productPrice}</p>
                <h6>Quantity</h6>
                <p>${data.quantity}</p>
                <h6>Price</h6>
                <p>${data.productPrice * data.quantity}</p>
                </div>`;

                innerContainer.append(markup);
              },
            },
          })
          .appendTo(container);
      },
    },
  });
});
