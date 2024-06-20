import { generateData } from "../../data/generate.js";

$(function () {
  const dataGridInst = $("#pagingAndScrollingDG")
    .dxDataGrid({
      dataSource: generateData(1000),
      keyExpr: "id",
      showBorders: true,
      scrolling: {
        mode: "standard",

        useNative: true,
        // scrollByContent: true,
        // scrollByThumb: true,
        // showScrollBar: "onHover",
      },
    })
    .dxDataGrid("instance");

  $("#scrollingMode").dxSelectBox({
    dataSource: [
      { mode: "standard", text: "Standard Mode" },
      { mode: "virtual", text: "Virtual Mode" },
      { mode: "infinite", text: "Infinite Mode" },
    ],
    displayExpr: "text",
    valueExpr: "mode",
    value: "standard",
    onValueChanged: function (e) {
      dataGridInst.option("scrolling.mode", e.value);
      if (e.value == "virtual") {
        dataGridInst.option("scrolling.rowRenderingMode", e.value);
      } else {
        dataGridInst.option("scrolling.rowRenderingMode", undefined);
      }
    },
  });
});
