$(function () {
  const simpleIndicatorInst = $("#simpleIndicator")
    .dxLoadIndicator({
      elementAttr: { class: "deep" },
      visible: true,
      //   indicatorSrc: "./gif/giphy.webp",
      // rtlEnabled: true,
      hint: "Getting the data",
      onContentReady() {},
      onInitialized() {},
      onDisposing() {},
      onOptionChanged() {},
    })
    .dxLoadIndicator("instance");

  const btnSourceInst = $("#btnSource")
    .dxButton({
      text: "Use GIF",
      onClick() {
        if (btnSourceInst.option("text").includes("Use")) {
          btnSourceInst.option("text", "Remove GIF");
          simpleIndicatorInst.option("indicatorSrc", "./gif/giphy.webp");
        } else {
          btnSourceInst.option("text", "Use GIF");
          simpleIndicatorInst.option("indicatorSrc", "");
        }
      },
    })
    .dxButton("instance");

  const rgSize = $("#rgSize")
    .dxRadioGroup({
      dataSource: [
        {
          id: 1,
          size: "Small",
        },
        {
          id: 2,
          size: "Medium",
        },
        {
          id: 3,
          size: "Big",
        },
      ],
      displayExpr: "size",
      valueExpr: "id",
      value: 1,
      layout: "horizontal",
      onValueChanged(e) {
        let height, width;

        if (e.value === 1) {
          height = "20px";
          width = "20px";
        } else if (e.value === 2) {
          height = "40px";
          width = "40px";
        } else if (e.value === 3) {
          height = "60px";
          width = "60px";
        }

        simpleIndicatorInst.option({
          height: height,
          width: width,
        });
      },
    })
    .dxRadioGroup("instance");
});
