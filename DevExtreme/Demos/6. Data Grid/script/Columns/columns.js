import { generateData } from "../../data/generate.js";

$(function () {
  const dataGridInst = $("#columnsDG")
    .dxDataGrid({
      dataSource: generateData(1000),
      showBorders: true,
      keyExpr: "id",
      editing: {
        allowUpdating: true,
        allowAdding: true,
        allowDeleting: true,
        mode: "popup", // 'batch' | 'cell' | 'form' | 'popup'
      },

      // Columns Properties
      allowColumnReordering: true,
      allowColumnResizing: true,
      columnAutoWidth: true,
      columnFixing: {
        enabled: true,
      },
      showBorders: true,
      columnChooser: {
        enabled: true,
      },
      columnHidingEnabled: true,
      columnMinWidth: 250,
      customizeColumns: function (columns) {
        columns[0].width = 100;
        columns[1].width = 100;
      },
      columns: [
        {
          dataField: "id",
          caption: "Id",
          dataType: "number",
          fixed: true,
          allowEditing: true,
          allowExporting: false,
          allowFiltering: false,
          allowFixing: true,
          //   allowGoruping: true,
          alignment: "left",
          allowHeaderFiltering: true,
          //   allowHiding: true,
          allowReordering: true,
          allowResizing: true,
          allowSearch: true,
          allowSorting: true,
          //   autoExpandGroup: true,
          calculateDisplayValue: "id",
          calculateFilterExpression: function (
            filterValue,
            selectedFilterOperation
          ) {},
          calculateGroupValue: function (rowData) {},
          calculateSortValue: function (rowData) {},
          cssClass: "my-class",
          encodeHtml: true,
          falseText: "False Text",
          filterOperations: [
            "contains",
            "notcontains",
            "startswith",
            "endswith",
            "=",
            "<>",
          ],
          //   filterType: "include",
          //   filterValue: "",
          //   filterValues: [],
          //   fixedPosition: "left",
          //   format: {
          //       type: "number",
          //     },
          formItem: {
            colSpan: 2,
            label: {
              location: "top",
            },
          },
          groupCellTemplate: function (element, options) {},
          //   groupIndex: 0,
          isBand: true,
        },
        {
          caption: "Name",
          columns: [
            {
              dataField: "firstName",
              caption: "First Name",
              dataType: "string",
              width: 100,
              allowResizing: false,
              allowReordering: false,
              customizeText: function (cellInfo) {
                return cellInfo.value + " $";
              },
            },
            {
              dataField: "lastName",
              caption: "Last Name",
              visible: true,
              minWidth: 50,
            },
          ],
        },
        {
          caption: "Full Name",
          calculateCellValue: function (rowData) {
            return rowData.firstName + " " + rowData.lastName;
          },
          cellTemplate: function (element, info) {
            element.append("<div>" + info.text + "</div>").css("color", "blue");
          },
        },
        {
          dataField: "gender",
          caption: "Gender",
          //   type: "adaptive",
          //   width: 50,
        },
        {
          dataField: "birthDate",
          caption: "Date of Birth",
          dataType: "date",
          hidingPriority: 0,
        },
        {
          type: "buttons",
          buttons: ["edit", "delete"],
        },
        {
          type: "buttons",
          buttons: [
            {
              text: "save",
              icon: "save",
              hint: "Save",
              onClick: function (e) {
                // Execute your command here
              },
            },
          ],
        },
      ],
    })
    .dxDataGrid("instance");

  // Hide the column using API
  // dataGridInst.option("columnOption", "gender", "visible", false);

  // For Band Column
  //   customizeColumns: function(columns) {
  //     columns.push({ // Pushes the "Contacts" band column into the "columns" array
  //         caption: "Contacts",
  //         isBand: true
  //     });

  //     var contactsFields = ["Email", "Mobile_Phone", "Skype"];
  //     for (var i = 0; i < columns.length - 1; i++) {
  //         if (contactsFields.indexOf(columns[i].dataField) > -1) // If the column belongs to "Contacts",
  //             columns[i].ownerBand = columns.length - 1; // assigns "Contacts" as the owner band column
  //     }
  // },

  // lookup column
});
