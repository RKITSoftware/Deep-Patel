$(function () {
  const userUrl = "https://localhost:7097/api/User";
  const jobUrl = "https://localhost:7097/api/Job";
  const genderArr = ["Male", "Female"];

  let userCustomStore = new DevExpress.data.CustomStore({
    key: "id",
    onBeforeSend(method, ajaxOptions) {
      ajaxOptions.xhrFields = { withCredentials: true };
    },
    load: function (loadOptions) {
      console.log(loadOptions);
      return $.ajax({
        type: "GET",
        url: userUrl,
        dataType: "json",
        contentType: "application/json",
        data: loadOptions,
        success: function (response) {
          return response;
        },
        error: function (err) {
          return err;
        },
      });
    },
    remove: function (key) {
      return $.ajax({
        type: "DELETE",
        url: userUrl + "/" + key,
        success: function (response) {
          return response;
        },
        error: function (err) {
          return err;
        },
      });
    },
    insert: function (values) {
      let user = {
        Id: 0,
        Name: values?.name,
        Gender: values?.gender,
        Number: values?.number,
        JobId: values?.jobId,
      };

      return $.ajax({
        type: "POST",
        url: userUrl,
        data: JSON.stringify(user),
        contentType: "application/json",
        success: function (response) {
          return response;
        },
        error: function (err) {
          return err;
        },
      });
    },
    update: function (key, values) {
      let user = {
        Id: key,
        Name: values?.name ?? "",
        Gender: values?.gender ?? "",
        Number: values?.number ?? 0,
        JobId: values?.jobId ?? 0,
      };

      return $.ajax({
        type: "PUT",
        url: userUrl,
        data: JSON.stringify(user),
        contentType: "application/json",
        success: function (response) {
          return response;
        },
        error: function (err) {
          return err;
        },
      });
    },
    onUpdated: function (e) {
      console.log(e);
    },
    errorHandler: function (err) {
      console.log("Error", err);
    },
  });

  let jobCustomStore = new DevExpress.data.CustomStore({
    key: "id",
    byKey: function (key) {
      return $.ajax({
        type: "GET",
        url: `${jobUrl}/${key}`,
        dataType: "json",
        success: function (response) {
          return response;
        },
      });
    },
    load: function () {
      return $.ajax({
        type: "GET",
        url: jobUrl,
        dataType: "json",
        success: function (response) {
          return response;
        },
      });
    },
  });

  const dataGridInst = $("#groupingDataGrid")
    .dxDataGrid({
      dataSource: userCustomStore,
      showBorders: true,
      //   allowColumnReordering: true,
      filterRow: {
        visible: true,
        applyFilter: "auto",
      },
      filterPanel: { visible: true },
      filterSyncEnabled: false,
      //   filterBuilder: {
      //     customOperations: [
      //       {
      //         name: "Name",
      //         caption: "Name",
      //         dataTypes: ["string"],
      //         hasValue: false,
      //         calculateFilterExpression: function (filterValue, field) {
      //           return [field.dataField, "=", 0];
      //         },
      //       },
      //     ],
      //   },
      filterBuilderPopup: {
        width: 400,
        title: "My Filter Builder",
      },
      headerFilter: {
        visible: true,
        allowSearching: true,
      },
      searchPanel: {
        visible: true,
        // text: "user",
      },
      columns: [
        {
          dataField: "id",
          caption: "Id",
        },
        {
          dataField: "name",
          caption: "Name",
          //   allowSearch: false,
          // allowFiltering: false,
          //   headerFilter: {
          //     allowSearch: false,
          //   },
        },
        {
          dataField: "gender",
          caption: "Gender",
          filterOperations: ["startswith"],
          selectedFilterOperation: "startswith",
          filterValues: ["Male", "Female"],
        },
        {
          dataField: "number",
          caption: "Mobile Number",
          allowHeaderFiltering: false,
        },
        {
          dataField: "jobId",
          caption: "Job",
          lookup: {
            dataSource: jobCustomStore,
            valueExpr: "id",
            displayExpr: "jobName",
          },
        },
      ],
    })
    .dxDataGrid("instance");

  // dataGridInst.searchByText("1/29/2016");

  //   dataGridInst.columnOption("number", {
  //     selectedFilterOperation: "=",
  //     filterValue: "000",
  //   });

  // Standalone filter buider
});
