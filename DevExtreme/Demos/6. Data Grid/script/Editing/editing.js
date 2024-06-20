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

  const dataGridInst = $("#editingDataGrid")
    .dxDataGrid({
      dataSource: userCustomStore,
      remoteOperations: {
        paging: true,
      },
      showBorders: true,
      paging: {
        pageIndex: 0,
        pageSize: 10,
      },
      pager: {
        visible: true,
        showPageSizeSelector: true,
        allowedPageSizes: [10, 20, 50],
        showInfo: true,
        showNavigationButtons: true,
        infoText: "Page #{0} - Total: {1} Pages. ({2} items)",
      },
      editing: {
        allowUpdating: true,
        allowAdding: true,
        allowDeleting: true,
        // mode: "row", // 'batch' | 'cell' | 'form' | 'popup'
      },
      columns: [
        {
          dataField: "id",
          caption: "Id",
        },
        {
          dataField: "name",
          caption: "Name",
          validationRules: [
            {
              type: "required",
            },
          ],
        },
        {
          dataField: "gender",
          caption: "Gender",
          validationRules: [
            {
              type: "custom",
              validationCallback: function (e) {
                return genderArr.includes(e.value);
              },
            },
          ],
        },
        {
          dataField: "number",
          caption: "Mobile Number",
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

  $("#editingMode").dxSelectBox({
    dataSource: [
      { mode: "row", text: "Row Mode" },
      { mode: "batch", text: "Batch Mode" },
      { mode: "cell", text: "Cell Mode" },
      { mode: "form", text: "Form Mode" },
      { mode: "popup", text: "Popup Mode" },
    ],
    displayExpr: "text",
    valueExpr: "mode",
    value: "row",
    onValueChanged: function (e) {
      dataGridInst.option("editing.mode", e.value);
    },
  });
});
