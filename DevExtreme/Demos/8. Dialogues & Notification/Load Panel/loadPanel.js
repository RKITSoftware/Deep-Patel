$(function () {
  const btnLoadPanelInst = $("#btnLoadPanel").dxButton({
    text: "Load",
    onClick() {
      $("#name").text("");
      $("#age").text("");
      simpleLoadPanelInst.show();
    },
  });

  const simpleLoadPanelInst = $("#simpleLoadPanel")
    .dxLoadPanel({
      shading: true,
      showIndicator: true,
      showPane: true,
      shadingColor: "rgba(0,0,0,0.4)",
      closeOnOutsideClick: true,

      // container: $(".loadPanel"),
      position: { of: ".loadPanel" },

      indicatorSrc: "./1473.gif",
      message: "Loading...",
      delay: 0,
      // deferRendering: false,

      // Max min height width
      //   maxHeight: "200px",
      //   maxWidth: "200px",
      //   minHeight: "50px",
      //   minWidth: "50px",

      //   animation: {
      //     hide: {
      //       type: "fade",
      //       from: 1,
      //       to: 0,
      //       delay: 2000,
      //     },
      //     show: {
      //       type: "fade",
      //       from: 0,
      //       to: 1,
      //       delay: 2000,
      //     },
      //   },

      onShown() {
        setTimeout(() => {
          //   simpleLoadPanelInst.option("visible", false);
          simpleLoadPanelInst.hide();
        }, 5000);
      },
      onShowing() {
        console.log("onShowing");
      },

      onHiding() {
        console.log("onHiding");
      },
      onHidden() {
        $("#name").text("Deep Patel");
        $("#age").text("21");
      },

      // Default
      elementAttr: {
        class: "deep",
      },
      focusStateEnabled: false,
      hoverStateEnabled: true,
      hint: "Loading the data",
      //   width: "300px",
      //   height: "300px",
      // rtlEnabled: true,

      onContentReady: () => {},
      onDisposing() {},
      onInitialized() {},
      onOptionChanged() {},
    })
    .dxLoadPanel("instance");

  simpleLoadPanelInst.show();
});
