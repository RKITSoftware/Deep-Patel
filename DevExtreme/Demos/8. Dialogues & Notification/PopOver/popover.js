$(function () {
  $("#popover1").dxPopover({
    target: "#link1",

    showEvent: "dxhoverstart",
    hideEvent: "dxhoverend",

    showTitle: true,
    title: "Custom Data",
    // titleTemplate() {
    //   return "Custom 2";
    // },
    onTitleRendered(e) {
      console.log("Title rendered successfully.", e);
    },

    position: "left",
    shading: true,
    shadingColor: "rgba(0,0,0,0.4)",

    // onShown(e) {
    //   console.log("Popup showed.", e);
    // },
    // onShowing(e) {
    //   console.log("Popup showing.", e);
    // },
    // onHidden(e) {
    //   console.log("Popup is hidden", e);
    // },
    // onHiding(e) {
    //   console.log("Popup is hiding.", e);
    // },

    // maxHeight: 100,
    // maxWidth: 100,
    // minHeight: 100,
    // minWidth: 100,

    // animation : {
    //
    // }

    // closeOnOutsideClick: true,
    showCloseButton: true,

    toolbarItems: [
      {
        widget: "dxButton",
        toolbar: "bottom",
        location: "before",
        options: {
          text: "Book",
          type: "default",
          stylingMode: "contained",
          onClick() {},
        },
      },
    ],

    // container: $("#containerPO"),
    // containerTemplate() {
    //   return "Custom";
    // },

    // Default
    visible: false,
    // rtlEnabled: true,
    disabled: true,
    elementAttr: {
      class: "my-class",
    },
    // height: 100,
    hint: "CD",
    hoverStateEnabled: true,
    deferRendering: true,

    onContentReady(e) {},
    onDisposing(e) {},
    onInitialized(e) {},
    onOptionChanged(e) {},
  });
});
