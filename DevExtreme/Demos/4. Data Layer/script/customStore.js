import { userData } from "../data/user.js";

$(function () {
  var store = new DevExpress.data.CustomStore({
    key: "id",
    cacheRawData: false,
    loadMode: "raw",
    useDefaultSearch: true,
    totalCount: (options) => {
      console.log(options);
      return userData.length;
    },
    errorHandler: (err) => {
      console.error(err);
    },
    byKey: function (key) {
      return userData.find((val) => val.id === key);
    },
    load: function (loadOptions) {
      return userData;
    },
    insert: function (values) {
      userData.push({ id: 10, name: values.name });
    },
    update: function (key, values) {
      let user = userData.find((val) => val.id === key);
      user.name = values.name;
      return user;
    },
    remove: function (key) {
      userData.pop({ id: key });
    },
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
      console.log("Temp", store);
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
      console.log(`Data is updated on this key ${key} and value is ${values}`);
    },
    onUpdating: (key, values) => {
      console.log(`Data is updating on this key ${key} and value is ${values}`);
    },
  });

  $("#sbCustomStore").dxSelectBox({
    acceptCustomValue: true,
    dataSource: store,
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
      store.insert({ id: 10, name: e.text });
      e.customItem = null;
    },
    useItemTextAsTitle: true,
    showDataBeforeSearch: false,
    searchTimeout: 0,
  });
});
