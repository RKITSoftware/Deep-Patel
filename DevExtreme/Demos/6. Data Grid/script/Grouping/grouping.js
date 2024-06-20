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
      sorting: {
        mode: "multiple",
      },
      showBorders: true,
      allowColumnReordering: true,
      //   Record Grouping
      grouping: {
        allowCollapsing: true,
        autoExpandAll: false,
        contextMenuEnabled: true,
        expandMode: "rowClick",
        texts: {
          groupContinuedMessage: "Continue",
          groupContinuesMessage: "Next",
          ungroup: "UnGroup",
          ungroupAll: "UnGroupAll",
        },
      },
      groupPanel: {
        visible: true,
      },
      editing: {
        allowUpdating: true,
        allowAdding: true,
        allowDeleting: true,
        mode: "popup",
      },
      columns: [
        {
          dataField: "id",
          caption: "Id",
        },
        {
          dataField: "name",
          caption: "Name",
          groupIndex: 0,
        },
        {
          dataField: "gender",
          caption: "Gender",
        },
        {
          dataField: "number",
          caption: "Mobile Number",
          allowGrouping: false,
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
});
