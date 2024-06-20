$(function () {
  const url = "https://666c098749dbc5d7145c4fdb.mockapi.io/users";
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
    // {
    //   dataField: "profilepic",
    //   caption: "Profile Pic",
    //   dataType: "string",
    //   cellTemplate(container, option) {
    //     const { value } = option;
    //     $("<div>")
    //       .append(
    //         $("<img>", { src: value, style: "hieght: 50px; width: 50px" })
    //       )
    //       .appendTo(container);
    //   },
    // },
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
      cellTemplate(container, options) {
        let color = options.value * 10 > 33 ? "green" : "red";
        $("<div>")
          .dxBullet({
            value: options.value * 10,
            target: 33,
            color: color,
            startScaleValue: 0,
            endScaleValue: 100,
            showTarget: true,
            showZeroLevel: true,
            tooltip: {
              enabled: true,
              format: "fixedPoint",
              customizeTooltip: function (arg) {
                return {
                  text:
                    "Value: " + arg.value + "%\nTarget: " + arg.target + "%",
                };
              },
            },
          })
          .appendTo(container);
      },
    },
  ];

  $("#dataGrid").dxDataGrid({
    dataSource: dataSource,
    keyExpr: "id",
    // rowTemplate(container, item) {
    //   const { data } = item;
    //   const markup =
    //     "<tr>" +
    //     `<td>${data.id}</td>` +
    //     `<td><img src='${data.profilepic}' style="height: 50px; with: 50px;" /> ${data.name}</td>` +
    //     `<td>${data.gender}</td>` +
    //     `<td>${data.city}</td>` +
    //     `<td>${data.state}</td>` +
    //     "</tr>";
    //   container.append(markup);
    // },
    columns: columns,
    rowAlternationEnabled: true,
    hoverStateEnabled: true,
    columnAutoWidth: true,
    showBorders: true,
    columnChooser: {
      enabled: true,
    },
    editing: {
      allowAdding: true,
      mode: "popup",
    },
    onToolbarPreparing(e) {
      let toolbarItems = e.toolbarOptions.items;

      toolbarItems.push({
        location: "center",
        locateInMenu: "never",
        template() {
          return $(
            "<div class='toolbar-label'><b>Deep's Club</b> Products</div>"
          );
        },
      });

      toolbarItems.push({
        location: "before",
        widget: "dxButton",
        options: {
          icon: "refresh",
          onClick() {
            window.location.reload();
          },
          hint: "Refresh Page",
        },
      });

      toolbarItems.push({
        location: "after",
        widget: "dxButton",
        locateInMenu: "always",
        options: {
          text: "Google",
          onClick() {
            window.location.assign("https://google.com");
          },
        },
      });

      toolbarItems.push({
        location: "before",
        widget: "dxButton",
        locateInMenu: "always",
        options: {
          text: "Youtube",
          onClick() {
            window.location.assign("https://youtube.com");
          },
        },
      });
    },
  });
});
