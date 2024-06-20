import { data } from "./data.js";

$(function () {
  const simpleMenuInst = $("#simpleMenu")
    .dxMenu({
      // dataSource: data,
      displayExpr: "name",

      items: data,
      itemsExpr: "nestedItem",
      itemTemplate: function (item) {
        console.log(item);
        return item.name;
      },

      animation: {
        hide: {
          type: "fade",
          from: 1,
          to: 0,
          duration: 50,
        },
        show: {
          type: "fade",
          from: 0,
          to: 1,
          duration: 50,
        },
      },

      accessKey: "m",
      // activeStateEnabled: true,
      adaptivityEnabled: true,
      cssClass: "customStyle",
      // disabled: true,
      // disabledExpr: "id",
      elementAttr: {
        class: "my-class",
      },
      // focusStateEnabled: true,
      // height: "100px",
      hideSubmenuOnMouseLeave: true,
      hint: "Menu Bar",
      // hoverStateEnabled: true,
      // orientation: "vertical",
      rtlEnabled: false,
      // width: "500px",
      // visible: false,
      tabIndex: 4,
      subMenuDirection: "auto", // ??
      showFirstSubmenuMode: "onClick",
      showSubmenuMode: { name: "onHover", delay: { show: 0, hide: 0 } },

      selectionMode: "single",
      selectByClick: true,
      // selectedExpr: "name",

      // onMethods
      // onContentReady: function (e) {
      //   console.log("Content is Ready.");
      // },
      // onDisposing: function (e) {
      //   console.log("Content is Disposing");
      // },
      // onInitialized: function (e) {
      //   console.log("Navigation Menu is initialized.");
      // },
      onItemClick: function (e) {
        console.log("Item Clicked :- ", e.itemData);
      },
      onItemContextMenu: function (e) {
        alert("ItemContextMenu Opened.");
      },
      // onOptionChanged: function (e) {},
      // onItemRendered: function (e) {
      //   alert("Successfully rendered");
      // },
      onSelectionChanged: function (e) {
        simpleMenuInst.option("selectedItem", e.addedItems[0] ?? null);
        console.log("Selected Item", simpleMenuInst.option("selectedItem"));
      },

      // onSubmenuHidden: function (e) {
      //   console.log("Submenu is hidden.");
      // },
      // onSubmenuHiding: function (e) {
      //   console.log("Submenu is hiding.");
      // },
      // onSubmenuShowing: function (e) {
      //   console.log("Submenu is showing");
      // },
      // onSubmenuShown: function (e) {
      //   console.log("Submenu is shown.");
      // },
    })
    .dxMenu("instance");

  $("#showFirstSubmenuMode").dxRadioGroup({
    dataSource: [
      {
        value: "onClick",
        text: "OnClick",
      },
      {
        value: "onHover",
        text: "OnHover",
      },
    ],
    displayExpr: "text",
    valueExpr: "value",
    value: "onClick",
    onValueChanged: function (e) {
      simpleMenuInst.option("showFirstSubmenuMode", e.value);
    },
  });
});
