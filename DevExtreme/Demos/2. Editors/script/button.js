import {
  onContentReadyEH,
  onDisposingEH,
  onInitializedHandler,
  onOptionChangedEH,
} from "../script/handler.js";

$(function () {
  const btnSimpleInst = $("#btnSimple")
    .dxButton({
      accessKey: "b",
      activeStateEnabled: true,
      disabled: false,
      elementAttr: {
        class: "btn-primary",
      },
      focusStateEnabled: true,
      // height: "100px",
      hint: "Hello world",
      hoverStateEnabled: true,
      icon: "user",
      onClick: function (e) {
        confirm("Hello, how are you?");
      },
      onContentReady: onContentReadyEH,
      onDisposing: onDisposingEH,
      onInitialized: onInitializedHandler,
      onOptionChanged: onOptionChangedEH,
      rtlEnabled: false,
      stylingMode: "outlined", // text, contained
      tabIndex: 2,
      text: "Click me!",
      type: "normal", // back, danger, success, default, normal
      useSubmitBehaviour: true,
      visible: true,
      // width: "100px",
    })
    .dxButton("instance");

  btnSimpleInst.on("click", function () {
    setTimeout(() => {
      window.location.replace("https://google.com");
    }, 5000);
  });
});
