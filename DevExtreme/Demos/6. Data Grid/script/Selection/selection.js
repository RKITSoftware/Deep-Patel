import { generateData } from "../../data/generate.js";

$(function () {
  const dataGridInst = $("#selectionDataGrid")
    .dxDataGrid({
      dataSource: generateData(1000),
      showBorders: true,
      keyExpr: "id",
      columns: [
        "id",
        {
          dataField: "firstName",
          caption: "First Name",
        },
        {
          dataField: "lastName",
          caption: "Last Name",
        },
        {
          dataField: "gender",
          caption: "Gender",
        },
        "birthDate",
      ],
      paging: {
        pageSize: 10,
      },

      // Selection
      onSelectionChanged: (e) => {
        console.log("Selection Changed", e);
        CalculateWordLength();
      },

      selection: {
        allowSelectAll: true,
        deferred: true,
        mode: "multiple",
        selectAllMode: "allPages",
        showCheckBoxesMode: "always",
      },
    })
    .dxDataGrid("instance");

  const deferredInst = $("#tbDeferred").dxTextBox({}).dxTextBox("instance");

  async function CalculateWordLength() {
    const selectedItems = await dataGridInst.getSelectedRowsData();
    let totalChars = 0;
    selectedItems.map((row) => {
      totalChars += row.firstName.length;
    });

    deferredInst.option("value", totalChars);
  }

  $("#sbSelectAllMode").dxSelectBox({
    dataSource: [
      {
        text: "All Pages",
        mode: "allPages",
      },
      {
        text: "Pages",
        mode: "page",
      },
    ],
    displayExpr: "text",
    valueExpr: "mode",
    value: "allPages",
    onValueChanged: function (e) {
      dataGridInst.option("selection.selectAllMode", e.value);
    },
  });

  $("#sbShowCheckBox").dxSelectBox({
    dataSource: [
      {
        text: "None",
        mode: "none",
      },
      {
        text: "On Click",
        mode: "onClick",
      },
      {
        text: "On LongTap",
        mode: "onLongTap",
      },
      {
        text: "Always",
        mode: "always",
      },
    ],
    displayExpr: "text",
    valueExpr: "mode",
    value: "always",
    onValueChanged: function (e) {
      dataGridInst.option("selection.showCheckBoxesMode", e.value);
    },
  });
});
