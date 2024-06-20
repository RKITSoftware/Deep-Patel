$(function () {
  const cbSimple = $("#cbSimple")
    .dxCheckBox({
      accessKey: "a", // Specifies the short cut key for checkbox
      activeStateEnabled: true, // Enables the effect when checking the checkbox
      disabled: false, // Disabled the checkbox for the user interaction
      // Adds the attribute to the checkbox html element
      elementAttr: {
        "area-label": "Check me!",
        class: "my-class", // Adding my-class to the checkbox
        name: "deep-patel", // Add the name attribute to the html element
        // id: "changeId", // It change the id of the checkbox and use it carefully after changing the id other features won't work.
      },
      focusStateEnabled: true, // Specified that component can be navigate through keyboard.
      // Specifies the height of the UI component
      // It can be in number | string |
      // Function that return number | string
      // height: "20px",
      hint: "Click me", // Gives hint on hover of checkbox
      hoverStateEnabled: true, // Handles the border color when hovering the checkbox
      isValid: true, // specifies the checkbox value is true or not according to status. True for pending and valid.
      name: "simple-checkbox", // specifies the name property for the form
      // Function that executes when content is ready and each time the content is changed.
      onContentReady: function (e) {
        console.log("checkbox is ready.");
        // console.log("Component", e.component); // Component gives the property value of the checkbox
        // console.log("Element", e.element); // Element gives the all html tags, events, etc.
        // console.log("Model", e.model);
      },
      onDisposing: function (e) {
        console.log("Checkbox is disposing");
        // all
      },
      onInitialized: function (e) {
        console.log("Checkbox is initialized");
        // component, element
      },
      onOptionChanged: function (e) {
        // Trigger when an event is occured like hover, click,
        // all
        if (e.name === "isActive") {
          console.log("isActive", e.value);
        }
        // console.log("Full name", e.fullName);
        // console.log("Name", e.name);
        // console.log("Value", e.value);
        // console.log("");
      },
      onValueChanged: function (e) {
        // Trigger event when value is changed.
        console.log("Previous Value :- ", e.previousValue);
        console.log("Current Value :- ", e.value);
      },
      readOnly: false, // specifies the checkbox value is for only reading purpose.
      rtlEnabled: false, // Switch the UI component toa right-to-left representation.
      tabIndex: 5, // Specifies the order using tab navigation
      value: true,
      // width: "20px", // Specifies the width of the UI Component
      visible: true, // specifie the UI component to visible
    })
    .dxCheckBox("instance");
  // cbSimple.dispose();

  $("#cbValidation").dxCheckBox({
    accessKey: "t",
    tabIndex: 1,
    text: "With Label",
    onValueChanged: function (e) {
      if (e.previousValue == false) {
        e.component.option("validationStatus", "valid");
        cbSimple.option("disabled", true);
      } else {
        e.component.option("validationStatus", "invalid");
        cbSimple.option("disabled", false);
      }
    },
    value: false,
    validationErrors: [
      // Specify validation rules
      {
        type: "required",
        message: "Error please check the box.",
      },
    ],
    validationStatus: "invalid", // "pending" "valid" sets the isValid flag.
  });

  // Testing

  // $("#cb1").dxCheckBox({
  //   text: "cb1",
  //   elementAttr: {
  //     class: "cb2",
  //   },
  // });

  // $(".cb2").dxCheckBox({
  //   text: "cb2",
  //   elementAttr: {
  //     class: "cb3",
  //   },
  // });

  // $(".cb3").dxCheckBox({
  //   text: "cb3",
  // });
});
