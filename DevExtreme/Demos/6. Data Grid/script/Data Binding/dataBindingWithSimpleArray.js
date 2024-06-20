import { commentsData } from "../../data/data.js";

$(function () {
  let commentsDataSource = new DevExpress.data.DataSource({
    store: {
      type: "array",
      data: commentsData,
      key: "id",
    },
  });

  $("#dataBindingDG").dxDataGrid({
    dataSource: commentsDataSource,
    keyExpr: "id",
  });
});
