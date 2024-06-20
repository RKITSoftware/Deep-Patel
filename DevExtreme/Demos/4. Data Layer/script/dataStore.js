import { userData } from "../data/user.js";

$(function () {
  let dataSource = new DevExpress.data.DataSource({
    store: userData,
    // filter: ["name", "startsWith", "D"],
    // group: "state",
    // sort: "state",
    // map: function (dataItem) {
    //   return {
    //     name: dataItem.name + " " + dataItem.state,
    //   };
    // },
    onChanged: function () {
      console.log("value changed.");
    },
    onLoadError: (err) => {
      console.error(err);
    },
    onLoadingChanged: (isLoading) => {
      console.log("isLoading", isLoading);
    },
    paginate: true,
    pageSize: 3,
    requireTotalCount: true,
    reshapeOnPush: true,
    searchExpr: ["name"],
    select: ["name"],
    // searchValue: "Deep",
    // searchOperation: "endswith",
    // postProcess: function (data) {
    //   console.log("data");
    // },
    // pushAggregationTimeout: 1,
    // expand: "name",
    // customQueryParams: {
    //   author: "deep-patel",
    // },
  });

  dataSource.load().done(function (data, extra) {
    console.log(data, extra);
  });

  $("#sbDataStore").dxSelectBox({
    acceptCustomValue: true,
    dataSource: dataSource,
    displayExpr: "name",
    valueExpr: "id",
    showClearButton: true,
    showDropDownButton: true,
    hint: "Choose name..",
    itemTemplate: function (val) {
      return val.name;
      // return val.key;
    },
    maxLength: 20,
    noDataText: "Empty",
    openOnFieldClick: true,
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
