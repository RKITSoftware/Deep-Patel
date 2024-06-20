import { hieararchicalData } from "./hieararchicalData.js";
import { plainData } from "./plainData.js";

$(function () {
  const simpleTreeViewInst = $("#simpleTreeView")
    .dxTreeView({
      items: hieararchicalData,
      dataStructure: "tree",
      displayExpr: "name",
      itemsExpr: "items",
      itemTemplate(item) {
        return item.id + " " + item.name;
      },
      disabledExpr: "disabledItem",
      expandedExpr: "expanded",
      keyExpr: "id",
      // parentIdExpr: 'parentId', // If dataStructure is plain.
      rootValue: "0",

      noDataText: "Data is Empty",
      hint: "Hiearchical Data",

      elementAttr: {
        class: "my-class",
      },

      accessKey: "t",
      // disabled: true,
      visible: true,
      activeStateEnabled: true,
      animationEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      expandAllEnabled: true,
      rtlEnabled: false,
      scrollDirection: "both",
      useNativeScrolling: true,

      expandEvent: "click", // dblClick
      // expandNodesRecursive: false,

      tabIndex: 2,
      // height: "100px",
      // width: "100px",

      //   onContentReady(e) {
      //     console.info("Content is ready");
      //   },
      //   onDisposing(e) {
      //     console.log("TreeView is disposing.");
      //   },
      //   onInitialized(e) {
      //     console.log("TreeView is initialized.");
      //   },
      onOptionChanged(e) {},

      onItemClick(e) {
        console.log("Item Clicked ", e.itemData);
      },
      //   onItemRendered(e) {
      //     console.log("Item Rendered Successfully.", e.itemData);
      //   },
      //   onItemExpanded(e) {
      //     console.log("Expanded ", e.itemData);
      //   },
      //   onItemCollapsed(e) {
      //     console.log("Collapsed", e.itemData);
      //   },
      onItemContextMenu(e) {
        alert("Context Menu is Clicked.");
      },
      //   onItemSelectionChanged(e) {
      //     console.log("Item Selected :- ", e.itemData);
      //   },
      //   onSelectionChanged(e) {
      //     console.log("Selection Changed ", e);
      //   },
      onSelectAllValueChanged(e) {
        console.log("Select all value changed ", e);
      },

      itemHoldTimeout: 1000,
      onItemHold(e) {
        console.log("Item Holded", e.itemData);
      },

      // Search
      searchEnabled: true,
      searchExpr: "name",
      searchTimeout: 0,
      // searchValue: "Asus",
      searchMode: "contains", // 'startsWith', 'equals'
      searchEditorOptions: {
        // dxTextBox Properties
        placeholder: "Search...",
      },

      // Select CheckBox
      selectByClick: true,
      //   selectedExpr: "name", // ??
      selectionMode: "muliiple",
      showCheckBoxesMode: "selectAll", // normal
      selectAllText: "Select All Items.",
      selectNodesRecursive: true,
    })
    .dxTreeView("instance");

  const plainTreeViewInst = $("#plainTreeView")
    .dxTreeView({
      dataStructure: "plain",
      displayExpr: "name",
      itemTemplate(item) {
        return item.id + " " + item.name;
      },
      keyExpr: "id",
      parentIdExpr: "parentId", // If dataStructure is plain.
      rootValue: null,
      expandEvent: "click",

      // Select CheckBox
      selectByClick: true,
      //   selectedExpr: "name", // ??
      selectionMode: "multiple",
      showCheckBoxesMode: "selectAll", // normal
      selectAllText: "Select All Items.",
      selectNodesRecursive: true,

      virtualModeEnabled: true,
      createChildren(parent) {
        let parentId = parent ? parent.itemData.id : null;
        return new Promise((resolve, reject) => {
          setTimeout(() => {
            let nodes = plainData.filter((val) => val.parentId == parentId);
            if (nodes) {
              resolve(nodes);
            } else {
              reject("Error");
            }
          }, 2000);
        });
      },
    })
    .dxTreeView("instance");
});
