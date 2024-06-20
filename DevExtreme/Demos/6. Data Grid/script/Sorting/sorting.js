import { generateData } from "../../data/generate.js";

$(function () {
  const dataGridInst = $("#sortingDataGrid")
    .dxDataGrid({
      dataSource: generateData(1000),
      keyExpr: "id",
      sorting: {
        mode: "multiple", // single // none
        ascendingText: "Ascending",
        descendingText: "Descending",
        clearText: "Clear Sorting",
        showSortIndexes: true,
      },
      columns: [
        "id",
        {
          dataField: "firstName",
          caption: "First Name",
          sortIndex: 0,
          sortOrder: "asc",
        },
        {
          dataField: "lastName",
          caption: "Last Name",
          sortIndex: 1,
          sortOrder: "asc",
          // allowSorting: false,
          // Custom sorting Method
          //   sortingMethod: function (value1, value2) {
          //     // Handling null values
          //     if (!value1 && value2) return -1;
          //     if (!value1 && !value2) return 0;
          //     if (value1 && !value2) return 1;
          //     // Determines whether two strings are equivalent in the current locale
          //     return value1.localeCompare(value2);
          //   },
        },
        {
          dataField: "gender",
          caption: "Gender",
          //   calculateSortValue: function (rowData) {
          //     if (rowData.gender == "Female") {
          //       return dataGridInst.columnOption("gender", "sortOrder") == "asc"
          //         ? "aaa"
          //         : "zzz";
          //     } else {
          //       return rowData.gender;
          //     }
          //   },
        },
        "birthDate",
      ],
    })
    .dxDataGrid("instance");

  //   dataGridInst.columnOption("gender", {
  //     sortIndex: 2,
  //     sortOrder: "desc",
  //   });

  const btnClearInst = $("#btnClear")
    .dxButton({
      text: "Clear Sorting",
      onClick: () => {
        dataGridInst.clearSorting();
      },
    })
    .dxButton("instance");
});
