import { onInitializedHandler, onValueChangedEH } from "../script/handler.js";

$(function () {
  const userData = [
    {
      id: 1,
      name: "Deep Patel",
      age: 21,
    },
    {
      id: 2,
      name: "Jeet Sorathiya",
      age: 21,
    },
    {
      id: 3,
      name: "Prajval Gahine",
      age: 21,
    },
  ];

  const ddSingle = $("#ddSingle")
    .dxDropDownBox({
      acceptCustomValue: true,
      accessKey: "s",
      activeStateEnabled: true,
      buttons: [
        {
          name: "Clear",
          location: "after",

          options: {
            text: "Clear",
            stylingMode: "text",
            onClick() {
              ddSingle.option("value", undefined);
            },
          },
        },
        "dropDown",
      ],
      contentTemplate: (e) => {
        const $list = $("<div>").dxList({
          allowItemDeleting: true,
          dataSource: e.component.getDataSource(),
          itemTemplate: function (item) {
            return $("<div>").text(item.name);
          },
          itemDeleteMode: "swipe",
          onItemDeleting: function (e) {
            const d = $.Deferred();
            DevExpress.ui.dialog
              .confirm("Do you really want to delete the item?")
              .done(function (value) {
                d.resolve(!value);
              })
              .fail(d.reject);
            e.cancel = d.promise();
          },
          selectionMode: "single",
          onItemClick: (selected) => {
            e.component.option("value", selected.itemData.id);
            e.component.close();
          },
          selectByClick: true,
        });
        return $list;
      },
      dataSource: userData,
      // items: userData,
      deferRendering: true, // load data immediately
      valueExpr: "id",
      displayExpr: "name", // display name property to the user
      displayValueFormatter: function (val) {
        return Array.isArray(val) ? val.join("; ") : val;
      },
      dropDownOptions: {
        showTitle: "Deep",
        title: "Select user",
        dragEnabled: true,
      },
      disabled: false,
      elementAttr: {
        class: "new-class",
      },
      focusStateEnabled: true,
      fieldTemplate: function (value, fieldElement) {
        const result = $("<div class='custom-item'>");
        let user = userData.find((val) => {
          return val.id == value;
        });
        result.dxTextBox({
          value: user?.name,
          readOnly: true,
        });
        fieldElement.append(result);
      },
      // height: "100px",
      hint: "Single dropdown box",
      hoverStateEnabled: true,
      inputAttr: {
        name: "userId",
      },
      isValid: true,
      maxLength: 10,
      name: "single-ddbox",
      onInitialized: (e) => onInitializedHandler(e),
      onValueChanged: (e) => onValueChangedEH(e),
      openOnFieldClick: true,
      opened: false,
      placeholder: "Choose user",
      readOnly: false,
      rtlEnabled: false,
      stylingMode: "outlined", // filled, underlined
      showClearButton: true,
      showDropDownButton: true,
      tabIndex: 2,
      text: "Hello user.",
      // value: null,
      valueChangeEvent: "blur",
      visible: true,
    })
    .dxDropDownBox("instance");

  const ddMulti = $("#ddMulti")
    .dxDropDownBox({
      acceptCustomValue: true,
      accessKey: "m",
      contentTemplate: (e) => {
        const $list = $("<div>").dxList({
          dataSource: e.component.getDataSource(),
          itemTemplate: function (item) {
            return $("<div>").text(item.name);
          },
          selectionMode: "multiple",
          showSelectionControls: true,
          onItemClick: (selected) => {
            e.component.option("value", selected.itemData.id);
            e.component.close();
          },
          onSelectionChanged: (args) => {
            let arr = args.component
              .option("selectedItems")
              .map((val) => val.name);
            let name = Array.isArray(arr) ? arr.join(", ") : arr;
            e.component.option("value", name);
          },
          selectByClick: true,
        });
        return $list;
      },
      dataSource: userData,
      // items: userData,
      deferRendering: true, // load data immediately
      valueExpr: "id",
      displayExpr: "name", // display name property to the user
      displayValueFormatter: function (val) {
        return val;
      },
      elementAttr: {
        class: "new-class",
      },
      focusStateEnabled: true,
      hint: "Multiple dropdown box",
      inputAttr: {
        name: "multipleuser",
      },
      isValid: true,
      maxLength: 10,
      openOnFieldClick: true,
      placeholder: "Choose multiple option",
      stylingMode: "underlined", // filled, underlined
      showClearButton: true,
      tabIndex: 4,
    })
    .dxDropDownBox("instance");
});
