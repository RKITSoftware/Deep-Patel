import { generateData } from "../../data/generate.js";

$(function () {
  const dataGridInst = $("#pagingAndScrollingDG")
    .dxDataGrid({
      dataSource: generateData(1000),
      keyExpr: "id",
      showColumnLines: true,
      showRowLines: true,
      rowAlternationEnabled: true,
      showBorders: true,
      paging: {
        // enabled: false,
        //pageIndex: 0,
        pageSize: 10,
      },
      pager: {
        // displayMode: "full", // compact
        visible: true,
        showPageSizeSelector: true,
        allowedPageSizes: [10, 20, 50],
        showInfo: true,
        showNavigationButtons: true,
        infoText: "Page #{0} - Total: {1} Pages. ({2} items)",
      },
      stateStoring: {
        enabled: true,
        type: "localStorage",
        storageKey: "storage",
        savingTimout: 200,
      },
    })
    .dxDataGrid("instance");

  console.log(dataGridInst);

  // console.log("Total Page Count", dataGridInst.pageCount());
  // dataGridInst.pageSize(2);
  // dataGridInst.pageIndex(dataGridInst.pageCount() - 1);

  $("#displayMode").dxSelectBox({
    dataSource: [
      { mode: "compact", text: "Compact Mode" },
      { mode: "full", text: "Full Mode" },
    ],
    displayExpr: "text",
    valueExpr: "mode",
    value: "full",
    onValueChanged: function (e) {
      dataGridInst.option("pager.displayMode", e.value);
      navButtonInst.option("readOnly", e.value == "compact");
    },
  });

  $("#showPageSizeSelector").dxCheckBox({
    value: true,
    text: "Page Size Selector",
    onValueChanged: function (e) {
      dataGridInst.option("pager.showPageSizeSelector", e.value);
    },
  });

  const navButtonInst = $("#showNavigationButton")
    .dxCheckBox({
      value: true,
      text: "Navigation Button Selector",
      onValueChanged: function (e) {
        dataGridInst.option("pager.showNavigationButtons", e.value);
      },
    })
    .dxCheckBox("instance");

  $("#showInfoText").dxCheckBox({
    value: true,
    text: "Show Info Text",
    onValueChanged: function (e) {
      dataGridInst.option("pager.showInfo", e.value);
    },
  });
});
