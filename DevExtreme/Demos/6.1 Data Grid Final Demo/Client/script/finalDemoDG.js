import { detailColumns, masterColumns } from "./columns.js";
import { GetEducationDetails, StudentCustomStore } from "./data.js";
import { excelMaster, excelMasterDetail, pdfMaster } from "./exportMethods.js";

$(function () {
  const finalDataGridInst = $("#finalDataGrid")
    .dxDataGrid({
      // Basic Properties
      activeStateEnabled: true,
      cacheEnabled: true,
      keyExpr: "U01F01",
      focusedRowEnabled: true,
      focusStateEnabled: true,
      hint: "Final Data Grid Demo",
      hoverStateEnabled: true,
      loadPanel: {
        enabled: true,
        shadingColor: "blue",
      },
      repaintChangesOnly: true,

      // Methods
      onDataErrorOccurred: (e) => {
        console.log("Data Error Occured :- ", e.error);
      },

      // Data Binding
      dataSource: StudentCustomStore(),

      // Paging
      paging: {
        enabled: true,
        pageIndex: 0,
        pageSize: 10,
      },
      pager: {
        allowedPageSizes: [10, 20, 25],
        displayMode: "full",
        // infoText: "Page {0} of {1} ({2} items)",
        showInfo: true,
        showNavigationButtons: true,
        showPageSizeSelector: true,
        visible: true,
      },

      // Scrolling
      scrolling: {
        mode: "standard",
        useNative: true,
      },

      // Editing
      editing: {
        allowAdding: true,
        allowDeleting: true,
        allowUpdating: true,
        confirmDelete: true,
        mode: "popup",
        popup: {
          title: "Student Form",
          showTitle: true,
        },
        form: {
          items: [
            {
              itemType: "group",
              caption: "Personal Data",
              items: ["U01F02", "U01F03", "U01F06"],
            },
            {
              itemType: "group",
              caption: "Contact Details",
              items: ["U01F04", "U01F05"],
            },
          ],
        },
        useIcons: true,
      },

      // Grouping
      grouping: {
        contextMenuEnabled: true,
        autoExpandAll: true,
      },
      groupPanel: {
        visible: true,
      },

      // Filtering
      headerFilter: {
        visible: true,
        allowSearch: true,
        searchTimeout: 0,
      },
      filterPanel: {
        visible: true,
        filterEnabled: true,
        texts: {
          createFilter: "Create Custom Filter",
        },
      },
      filterRow: {
        visible: true,
        applyFilter: "auto",
      },
      filterBuilder: {
        groupOperationDescriptions: {
          and: "&",
        },
      },
      filterBuilderPopup: {
        position: {
          of: window,
          at: "top",
          my: "top",
          offset: { y: 10 },
        },
        width: "500px",
        height: "300px",
      },

      // Sorting
      sorting: {
        mode: "multiple",
        showSortIndexes: true,
      },

      // Searching
      searchPanel: {
        visible: true,
        placeholder: "Search..",
        // highlightCaseSensitive: true,
        highlightSearchText: true,
        searchVisibleColumnsOnly: true,
      },

      // Selection
      selection: {
        mode: "multiple",
        selectAllMode: "allPages",
        showCheckBoxesMode: "always",
      },

      // Columns
      columns: masterColumns,
      columnAutoWidth: true,
      columnMinWidth: 100,
      allowColumnResizing: true,
      allowColumnReordering: true,
      columnResizingMode: "nextColumn",
      columnHidingEnabled: true,
      columnFixing: {
        enabled: true,
      },
      columnChooser: {
        allowSearch: true,
        cacheEnabled: "Drag any column for hiding.",
        searchTimeout: 0,
        enabled: true,
      },

      // State Storing
      stateStoring: {
        enabled: true,
        savingTimeout: 1000,
        storageKey: "finalDG",
        type: "localStorage",
      },

      // Appearence
      rowAlternationEnabled: true,
      showBorders: true,
      showColumnLines: false,
      showRowLines: true,

      // Summary
      summary: {
        recalculateWhileEditing: true,
        groupItems: [
          {
            column: "Id",
            summaryType: "custom",
            name: "GroupCount",
            customizeText: function (itemInfo) {
              return `Count is ${itemInfo.value}`;
            },
            showInGroupFooter: true,
          },
          {
            column: "Age",
            summaryType: "min",
            alignment: "right",
          },
          {
            column: "Age",
            summaryType: "max",
            alignment: "right",
          },
        ],
        calculateCustomSummary: function (options) {
          if (options.name === "GroupCount") {
            if (options.summaryProcess === "start") {
              options.totalValue = 0;
            }
            if (options.summaryProcess === "calculate") {
              options.totalValue += 1;
            }
          }
        },
        totalItems: [
          {
            column: "Age",
            summaryType: "min",
            skipEmptyValues: false,
            alignment: "right",
          },
          {
            column: "Age",
            summaryType: "max",
            alignment: "right",
          },
        ],
      },

      // Master Detail
      masterDetail: {
        enabled: true,
        autoExpandAll: false,
        template: function (container, options) {
          const { data } = options;
          const heading = `<div><h4>${data.U01F02}'s Education</h4></div>`;
          container.append(heading);

          $("<div>")
            .dxDataGrid({
              dataSource: GetEducationDetails(data.U01F01),
              columns: detailColumns,
            })
            .appendTo(container);
        },
      },

      // Tool Bar
      onToolbarPreparing: function (e) {
        let toolbarItems = e.toolbarOptions.items;

        toolbarItems.push({
          widget: "dxButton",
          options: {
            icon: "palette",
            onClick: function () {
              if ($("#finalDemoTheme").attr("href").includes("light")) {
                $("#finalDemoTheme").attr(
                  "href",
                  "https://cdnjs.cloudflare.com/ajax/libs/devextreme/21.1.11/css/dx.material.blue.dark.compact.min.css"
                );
              } else {
                $("#finalDemoTheme").attr(
                  "href",
                  "https://cdnjs.cloudflare.com/ajax/libs/devextreme/21.1.11/css/dx.material.blue.light.compact.min.css"
                );
              }
            },
          },
          location: "after",
        });
      },

      // Exporting
      export: {
        enabled: true,
        allowExportSelectedData: true,
      },
      onExporting: function (e) {
        let exportMode = sbExportOptionsInst.option("value");
        switch (exportMode) {
          case "excelMasterDetail":
            excelMasterDetail(e);
            break;
          case "pdfMaster":
            pdfMaster(e);
            break;
          case "excelMaster":
            excelMaster(e);
            break;
        }
      },
    })
    .dxDataGrid("instance");

  const sbExportOptionsInst = $("#sbExportOptions")
    .dxSelectBox({
      dataSource: [
        {
          text: "Excel Mode - Master",
          mode: "excelMaster",
        },
        {
          text: "PDF - Master",
          mode: "pdfMaster",
        },
        {
          text: "Excel - Master Detail",
          mode: "excelMasterDetail",
        },
      ],
      displayExpr: "text",
      valueExpr: "mode",
      value: "excelMaster",
    })
    .dxSelectBox("instance");
});
