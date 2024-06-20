import {
  onChangeEventHandler,
  onContentReadyEH,
  onCopyEH,
  onCutEH,
  onDisposingEH,
  onEnterKeyEH,
  onFocusInEH,
  onFocusOutEH,
  onInitializedHandler,
  onInputEH,
  onKeyDownEH,
  onKeyUpEH,
  onOptionChangedEH,
  onPasteEH,
  onValueChangedEH,
} from "../script/handler.js";

$(function () {
  const numberBoxInst = $("#numberbox")
    .dxNumberBox({
      accessKey: "n",
      activeStateEnabled: true,
      buttons: [
        {
          name: "minValue",
          location: "before",
          options: {
            icon: "minus",
            stylingMode: "filled",
            onClick: function () {
              numberBoxInst.option("value", numberBoxInst.option("min"));
            },
          },
        },
        {
          name: "maxValue",
          location: "after",
          options: {
            icon: "plus",
            stylingMode: "underlined",
            onClick: function () {
              numberBoxInst.option("value", numberBoxInst.option("max"));
            },
          },
        },
        "clear",
        "spins",
      ],
      disabled: false,
      elementAttr: {
        class: "style-class",
      },
      focusStateEnabled: true,
      // format: "decimal", // decimal, percent, currency, fixedPoint
      // height: 100,
      hint: "Choose number",
      hoverStateEnabled: true,
      inputAttr: {
        class: "inputTagClass",
      },
      invalidValueMessage: "Enter valid number",
      isValid: true,
      max: "10000.00000",
      min: "-10000.00000",
      mode: "text", // number, tel
      name: "salary",
      onContentReady: onContentReadyEH,
      onChange: onChangeEventHandler,
      onCut: onCutEH,
      onCopy: onCopyEH,
      onPaste: onPasteEH,
      onInitialized: onInitializedHandler,
      onDisposing: onDisposingEH,
      onEnterKey: onEnterKeyEH,
      onFocusIn: onFocusInEH,
      onFocusOut: onFocusOutEH,
      onInput: onInputEH,
      onKeyDown: (e) => onKeyDownEH(e),
      onKeyUp: (e) => onKeyUpEH(e),
      onOptionChanged: onOptionChangedEH,
      onValueChange: onValueChangedEH,
      placeholder: "enter salary",
      readOnly: false,
      rtlEnabled: false,
      step: 5,
      stylingMode: "underlined",
      showSpinButtons: true,
      showClearButton: true,
      tabIndex: 2,
      text: "None",
      useLargeSpinButtons: true,
      validationStatus: "valid",
      value: 1.01,
      // valueChangeEvent: "blur",
      visible: true,
      // width: 100
    })
    .dxNumberBox("instance");
});
