$(function () {
  const popupContentTemplate = function () {
    const $scrollView = $("<div/>");

    $scrollView.append(
      $("<div/>").append(
        $("<div>").append(
          $(`<p>Full Name: <span>Deep </span>
                                 <span>Patel</span></p>`),
          $(`<p>Birth Date: <span>25/5/2003</span></p>`),
          $(`<p>Address: <span>Limbdi</span></p>`),
          $(`<p>Hire Date: <span>8 January, 2024</span></p>`),
          $(`<p>Position: <span>Full Stack Developer</span></p>`)
        )
      )
    );

    $scrollView.dxScrollView({
      width: "100%",
      height: "100%",
    });

    return $scrollView;
  };

  const btnOpen = $("#btnOpen")
    .dxButton({
      text: "Open",
      onClick() {
        popup.show();
      },
    })
    .dxButton("instance");

  const popup = $("#simplePopup")
    .dxPopup({
      contentTemplate: popupContentTemplate,
      closeOnOutsideClick: true,
      // container: $(".popupContainer"),
      // dragEnabled: true,
      // fullScreen: true,
      showCloseButton: true,
      showTitle: true,
      title: "Employee Data",
      //   titleTemplate(e) {
      //     console.log(e);
      //   },
      position: "center",
      shading: true,
      shadingColor: "rgba(0,0,0,0.4)",

      // resizeEnabled: true,
      //   maxHeight: 1000,
      //   minHeight: 200,
      //   maxWidth: 500,
      //   minWidth: 200,

      onShown(e) {
        console.log("Popup showed.", e);
      },
      onShowing(e) {
        console.log("Popup showing.", e);
      },
      onHidden(e) {
        console.log("Popup is hidden", e);
      },
      onHiding(e) {
        console.log("Popup is hiding.", e);
      },

      onTitleRendered(e) {
        console.log("Rendered successfully.");
      },

      onResize(e) {},
      onResizeStart(e) {},
      onResizeEnd(e) {},

      // Toolbar
      toolbarItems: [
        {
          widget: "dxButton",
          toolbar: "bottom",
          location: "after",
          options: {
            text: "Book",
            type: "default",
            stylingMode: "contained",
            onClick() {
              popup.hide();
            },
          },
        },
      ],

      // Common
      rtlEnabled: false,
      tabIndex: 2,
      deferRendering: false,
      accessKey: "a",
      disabled: false,
      elementAttr: {
        class: "deep",
      },
      focusStateEnabled: true,
      //   height: 200,
      //   width: 200,
      hint: "Employee Information",
      hoverStateEnabled: true,
      visible: false,

      onContentReady() {},
      onInitialized() {},
      onDisposing() {},
      onOptionChanged() {},
    })
    .dxPopup("instance");

  popup.show();
});
