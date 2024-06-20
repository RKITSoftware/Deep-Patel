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
  const tbSimpleInst = $("#tbSimple")
    .dxTextBox({
      accessKey: "t",
      activeStateEnabled: true,
      buttons: [
        "clear",
        {
          name: "today",
          location: "after",
          options: {
            icon: "plus",
            onClick: function () {
              tbSimpleInst.option("value", "Hello world");
            },
          },
        },
      ],
      disabled: false,
      elementAttr: {
        class: "new-style",
        "area-label": "Deep Patel",
      },
      focusStateEnabled: true,
      // height: 100,
      hint: "TextBox",
      hoverStateEnabled: true,
      inputAttr: {
        id: "username",
      },
      isValid: true,
      maxLength: 15,
      mode: "text", // email, password, search, tel, url
      name: "username",
      onChange: onChangeEventHandler,
      onContentReady: onContentReadyEH,
      onCopy: onCopyEH,
      onCut: onCutEH,
      onDisposing: onDisposingEH,
      onEnterKey: onEnterKeyEH,
      onFocusIn: onFocusInEH,
      onFocusOut: onFocusOutEH,
      onInitialized: onInitializedHandler,
      onInput: onInputEH,
      onKeyDown: (e) => onKeyDownEH(e),
      onKeyUp: (e) => onKeyUpEH(e),
      onOptionChanged: onOptionChangedEH,
      onPaste: onPasteEH,
      onValueChanged: (e) => onValueChangedEH(e),
      placeholder: "Enter username",
      readOnly: false,
      rtlEnabled: true,
      showClearButton: true,
      spellcheck: true,
      stylingMode: "underlined",
      tabIndex: 2,
      text: "inner text",
      value: "deep-patel",
      // valueChangeEvent: 'blur',
      visible: true,
      //width: 100,
    })
    .dxTextBox("instance");

  const tbMaskInst = $("#tbMask")
    .dxTextBox({
      text: "maskedTextBox",
      hint: "Masked Text box",
      mask: "GJ-00-KK-0000",
      maskValidationMessage: "Value is not in correct format.",
      maskChar: "#",
      maskRules: {
        K: /[A-Z]/,
      },
      useMaskedValue: true,
      showMaskMode: "onFocus",
    })
    .dxTextBox("instance");
});
