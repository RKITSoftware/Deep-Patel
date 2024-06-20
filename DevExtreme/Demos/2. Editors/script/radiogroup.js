$(function () {
  const gradeList = ["AA", "BB", "CC", "DD", "FF"];
  const userList = [
    {
      id: 1,
      category: "Grocery",
    },
    {
      id: 2,
      category: "Household item",
    },
    {
      id: 3,
      category: "Electronics",
    },
    {
      id: 4,
      category: "Washing",
    },
  ];

  DevExpress.ui.dxRadioGroup.defaultOptions({
    activeStateEnabled: true,
    disabled: false,
    elementAttr: {
      class: "my-style",
    },
    focusStateEnabled: true,
    // height: "100px",
    hoverStateEnabled: true,
    onContentReady: function () {},
    onInitialized: function () {},
    onDisposing: function () {},
    onOptionChanged: function () {},
    onValueChanged: function (e) {
      console.log("Previous value :- ", e.previousValue);
      console.log("Value :- ", e.value);
    },
    readOnly: false,
    rtlEnabled: false,
    // width: "1000px",
    visible: true,
  });

  const rgHorizontalInst = $("#rgHorizontal")
    .dxRadioGroup({
      accessKey: "h",
      dataSource: gradeList,
      hint: "Choose grade..",
      isValid: true,
      value: "AA",
      layout: "horizontal",
      name: "grade",
      tabIndex: 2,
    })
    .dxRadioGroup("instance");

  const rgVerticalInst = $("#rgVertical")
    .dxRadioGroup({
      accessKey: "v",
      items: userList,
      displayExpr: "category",
      hint: "Choose category",
      valueExpr: "id",
      isValid: false,
      itemTemplate: function (data) {
        return "Category :- " + data.category;
      },
      layout: "vertical",
      name: "category",
    })
    .dxRadioGroup("instance");
});
