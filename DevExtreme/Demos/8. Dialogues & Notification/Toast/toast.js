$(function () {
  const cbSubscribe = $("#cbSubscribe")
    .dxCheckBox({
      text: "Subscribe",
      onValueChanged(e) {
        let message = `${cbSubscribe.option("text")} successfully.`;
        toast.option("message", message);

        if (e.value) {
          toast.option("type", "success");
          cbSubscribe.option("text", "Unsubscribe");
        } else {
          toast.option("type", "error");
          cbSubscribe.option("text", "Subscribe");
        }

        toast.show();
      },
    })
    .dxCheckBox("instance");

  const toast = $("#toast")
    .dxToast({
      // Default
      accessKey: "m",
      deferRendering: true,
      elementAttr: {
        class: "custom-class",
      },
      focusStateEnabled: true,
      height: 50,
      width: 600,
      hint: "Toast Notification",
      hoverStateEnabled: true,
      visible: false,
      tabIndex: 2,
      rtlEnabled: false,

      // Common
      onContentReady(e) {},
      onDisposing(e) {},
      onInitialized(e) {},
      onOptionChanged(e) {},

      // Toast
      // animation : // Default
      type: "info", // 'custom' | 'error' | 'info' | 'success' | 'warning'
      displayTime: 5000,
      //   shading: true,
      //   shadingColor: "rgba(0, 0, 0, 0.2)",
      position: { of: ".notificationContainer" },

      // Close Properties
      closeOnClick: false,
      closeOnOutsideClick: false,
      closeOnSwipe: true,

      // Resize Properties
      maxHeight: "200px",
      maxWidth: "1000px",
      minHeight: "50px",
      minWidth: "600px",

      // Show Hide Methods
      onHidden(e) {
        console.log("Toast is hidden.", e);
      },
      onHiding(e) {
        console.log("Toast is hiding.", e);
      },
      onShowing(e) {
        console.log("Toast is Showing.", e);
      },
      onShown(e) {
        console.log("Toast is shown.", e);
      },
    })
    .dxToast("instance");
});
