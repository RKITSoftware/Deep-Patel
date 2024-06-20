import { userData } from "../data/user.js";

$(function () {
  let store = new DevExpress.data.ArrayStore({
    key: "id", // string || Array[string]
    data: userData,
    errorHandler: function (err) {
      console.log("Error Handler", err);
    },
  });

  let dataSource = new DevExpress.data.DataSource({
    store: {
      type: "array",
      key: "id",
      data: userData,
      onInserting: function (val) {
        console.log("Before Inserting", val);
      },
      onInserted: function (val) {
        console.log("Inserted data", val);
      },
      //   onLoaded: function (result) {
      //     console.log("Result", result);
      //   },
      //   onLoading: function (loadOptions) {
      //     loadOptions["skip"] = 1;
      //     console.log("LoadOptions", loadOptions);
      //   },
      onModified: () => {
        console.log("Data is successfully added, updated or removed.");
      },
      onModifying: () => {
        console.log("Data is modifying.");
      },
      onPush: (changes) => {
        console.log("changes", changes);
      },
      onRemoved: (key) => {
        console.log(`Data is ${key} removed.`);
      },
      onRemoving: (key) => {
        console.log(`Data of this ${key} is removing.`);
      },
      onUpdated: (key, values) => {
        console.log(
          `Data is updated on this key ${key} and value is ${values}`
        );
      },
      onUpdating: (key, values) => {
        console.log(
          `Data is updating on this key ${key} and value is ${values}`
        );
      },
    },
  });

  console.log("store", store);
  console.log("datasource", dataSource);

  $("#sbArrayStore").dxSelectBox({
    acceptCustomValue: true,
    dataSource: dataSource,
    displayExpr: "name",
    valueExpr: "id",
    showClearButton: true,
    showDropDownButton: true,
    hint: "Choose name..",
    itemTemplate: function (val) {
      return val.name;
    },
    maxLength: 20,
    noDataText: "Empty",
    openOnFieldClick: true,
    placeholder: "choose name from select box..",
    searchEnabled: true,
    searchMode: "contains",
    tabIndex: 2,
    text: "select name",
    stylingMode: "underlined",
    spellcheck: true,
    wrapItemText: true,
    dropDownButtonTemplate: function (icon, text) {
      icon = "chevrondown"; // The button's icon.
      text.prepend(`<span class="dx-icon-${icon}"></span>`); // The button's text.
    },
    onCustomItemCreating: function (e) {
      dataSource.store().insert({ id: 10, name: e.text });
      e.customItem = null;
    },
    useItemTextAsTitle: true,
    showDataBeforeSearch: false,
    searchTimeout: 0,
  });
});
