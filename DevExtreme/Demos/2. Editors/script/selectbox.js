import products from "../data/products.js";
import states from "../data/states.js";
import {
  onChangeEventHandler,
  onClosedEventHandler,
  onContentReadyEH,
  onCopyEH,
  onCutEH,
  onDisposingEH,
  onEnterKeyEH,
  onFocusInEH,
  onFocusOutEH,
  onInitializedHandler,
  onInputEH,
  onItemClickEH,
  onKeyDownEH,
  onKeyUpEH,
  onOpenedEH,
  onOptionChangedEH,
  onPasteEH,
  onSelectionChangedEH,
  onValueChangedEH,
} from "../script/handler.js";

$(function () {
  const selectBoxInst = $("#selectbox")
    .dxSelectBox({
      acceptCustomValue: true,
      accessKey: "s",
      activeStateEnabled: true,
      dataSource: states,
      displayExpr: "name",
      valueExpr: "id",
      deferRendering: false,
      disabled: false,
      elementAttr: {
        class: "my-class",
      },
      showClearButton: true,
      showDropDownButton: true,
      buttons: [
        {
          name: "Add",
          location: "after",
          options: {
            icon: "plus",
            onClick: function () {
              states.push(selectBoxInst.option("value"));
              states.sort();
              selectBoxInst.option("items", states);
            },
          },
        },
        "clear",
        "dropDown",
      ],
      focusStateEnabled: true,
      // height: 10,
      hint: "Choose state..",
      hoverStateEnabled: true,
      inputAttr: {
        class: "input-style",
      },
      isValid: true,
      itemTemplate: function (val) {
        return val.name;
      },
      maxLength: 20,
      name: "inpState",
      noDataText: "Empty",
      opened: false,
      openOnFieldClick: true,
      placeholder: "choose state from select box..",
      readOnly: false,
      rtlEnabled: false,
      searchEnabled: true,
      searchMode: "contains", // startsWith
      tabIndex: 2,
      text: "select state",
      stylingMode: "underlined",
      spellcheck: true,
      visible: true,
      // width: 100,
      wrapItemText: true,
      dropDownButtonTemplate: function (icon, text) {
        icon = "chevrondown"; // The button's icon.
        text.prepend(`<span class="dx-icon-${icon}"></span>`); // The button's text.
      },
      //   fieldTemplate: function (data, container) {
      //     var result = $(`<div class='custom-item'>
      //                             <div class='product-name'></div>
      //                             <div class='product-price'>Category : ${
      //                               data ? data.Category : 0
      //                             }</div>
      //                         </div>`);
      //     result.find(".product-name").dxTextBox({
      //       value: data && data.Name,
      //       readOnly: true,
      //     });
      //     container.append(result);
      //   },
      onCustomItemCreating: function (data) {
        states.push({ id: states.length + 1, name: data.text });
        states.sort((val) => val.id);
        selectBoxInst.option("items", states);
        data.customItem = null;
      },
      useItemTextAsTitle: true,
      showDataBeforeSearch: false,
      searchTimeout: 0,
      onChange: onChangeEventHandler,
      onOpened: onOpenedEH,
      onInitialized: onInitializedHandler,
      onDisposing: onDisposingEH,
      onEnterKey: onEnterKeyEH,
      onFocusIn: onFocusInEH,
      onFocusOut: onFocusOutEH,
      onClosed: onClosedEventHandler,
      onContentReady: onContentReadyEH,
      onCut: onCutEH,
      onCopy: onCopyEH,
      onPaste: onPasteEH,
      onInput: onInputEH,
      onOptionChanged: onOptionChangedEH,
      onKeyDown: (e) => onKeyDownEH(e),
      onKeyup: (e) => onKeyUpEH(e),
      onItemClick: (e) => onItemClickEH(e),
      onSelectionChanged: (e) => onSelectionChangedEH(e),
      onValueChanged: (e) => onValueChangedEH(e),
      showSelectionControls: true,
      // valueChangeEvent: "blur",
    })
    .dxSelectBox("instance");

  const groupedProductData = new DevExpress.data.DataSource({
    store: {
      type: "array",
      data: products,
      key: "ID",
    },
    group: "Category",
  });

  const groupselectbox = $("#groupselectbox")
    .dxSelectBox({
      acceptCustomValue: true,
      accessKey: "g",
      dataSource: groupedProductData,
      grouped: true,
      groupTemplate(data) {
        return $(
          `<div class='custom-icon'>
            <span class='dx-icon-home icon' style="color:aqua;"></span>
                ${data.key}
            </div>`
        );
      },
      searchExpr: ["Price"],
      displayExpr: "Name",
      valueExpr: "ID",
      deferRendering: false,
      elementAttr: {
        class: "my-class",
      },
      showClearButton: true,
      showDropDownButton: true,
      hint: "Choose product.",
      isValid: true,
      itemTemplate: function (val) {
        return val.Name;
      },
      maxLength: 20,
      name: "inpProduct",
      noDataText: "No Products",
      openOnFieldClick: true,
      placeholder: "choose product",
      searchMode: "contains", // startsWith
      tabIndex: 4,
      text: "select product",
      stylingMode: "underlined",
      wrapItemText: true,
      onItemClick: (e) => onItemClickEH(e),
      onSelectionChanged: (e) => onSelectionChangedEH(e),
      onValueChanged: (e) => onValueChangedEH(e),
      showSelectionControls: true,
    })
    .dxSelectBox("instance");
});
