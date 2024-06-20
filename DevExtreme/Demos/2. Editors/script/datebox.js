$(function () {
  const disabledDateOfSimpleDB = [
    new Date(2017, 0, 1),
    new Date(2017, 0, 2),
    new Date(2017, 0, 16),
    new Date(2017, 1, 20),
    new Date(2017, 4, 29),
    new Date(2017, 6, 4),
    new Date(2017, 8, 4),
    new Date(2017, 9, 9),
    new Date(2017, 10, 11),
    new Date(2017, 10, 23),
    new Date(2017, 11, 25),
  ];

  const dbSimpleInst = $("#dbSimple")
    .dxDateBox({
      acceptCustomValue: true, // Turn off the keyboard write for custom date. Only start the choose option.
      accessKey: "a",
      activeStateEnabled: true,
      adaptivityEnabled: true, // ?
      applyButtonText: "Confirm",
      cancelButtonText: "Cancel.",
      applyValueMode: "useButtons", // 'instantly'
      buttons: [
        {
          name: "today",
          location: "before",

          options: {
            text: "Today",
            stylingMode: "text",
            onClick() {
              dbSimpleInst.option("value", new Date());
            },
          },
        },
        "dropDown",
      ],

      // Only applies when picker type is calender
      dateSerializationFormat: "yyyy-MM-dd",
      // yyyy-MM-ddTHH:mm:ss
      // yyyy-MM-ddTHH:mm:ssZ
      // yyyy-MM-ddTHH:mm:ssx
      dateOutOfRangeMessage: "Choose date in min and max.",
      deferRendering: true, // it is uses for the dropdown field's content when it is diaplayed. if false then the content is rendered immediately.
      disabled: false,
      disabledDates: disabledDateOfSimpleDB, // an Array of Date or Function that returns an array of date.
      displayFormat: "yyyy-MM-d", // "'Year': yyyy", // "shortdate",
      elementAttr: {
        class: "class-name",
      },
      focusStateEnabled: true,
      // height : 10,
      hint: "Choose day.",
      hoverStateEnabled: true,
      inputAttr: {
        id: "inpCalender",
      },
      invalidDateMessage: "Enter date in correct format.",
      isValid: true,
      //   max: Date.now(),
      maxLength: 10,
      //     min: new Date(2003, 0, 1),
      name: "calender",
      onChange: function (e) {
        // e contains component, element, event, model
        console.log("value changed in datebox");
      },
      onClosed: function (e) {
        // component, element, model
        console.log("Drop down is closed.");
      },
      onContentReady: function (e) {
        // component, element, model
        console.log("Calender's content is ready.");
      },
      onCopy: function (e) {
        // all
        console.log("Data is copied.");
      },
      onCut: function (e) {
        console.log("Data is cut and ready to paste.");
      },
      onDisposing: function (e) {
        console.log("Calender is disposed.");
      },
      onEnterKey: function (e) {
        console.log("enter key is pressed.");
      },
      onFocusIn: function (e) {
        console.log("focus in ");
      },
      onFocusOut: function (e) {
        console.log("focus out");
      },
      onInitialized: function (e) {
        console.log("calender box is successfully initialized");
      },
      onInput: function (e) {
        console.log("Input is changed while focus is on.");
      },
      onKeyDown: function (e) {
        console.log("KeyDown", e.event.originalEvent);
      },
      onKeyUp: function (e) {
        console.log("KeyUP", e.event.originalEvent);
      },
      onOpened: function (e) {
        console.log("dropdown is opened.");
      },
      onOptionChanged: function (e) {
        console.log("Option changed", e);
      },
      onPaste: function (e) {
        console.log("Data is pasted.");
      },
      onValueChanged: function (e) {
        console.log("Previous Value", e.previousValue);
        console.log("Current Value", e.value);
      },
      opened: false,
      openOnFieldClick: true,
      pickerType: "calender", // roller, list, native
      placeholder: "Choose date",
      spellcheck: true,
      stylingMode: "outlined",
      useMaskBehavior: true,
      valueChangeEvent: "keyup",
      visible: true,
      // width: 0
    })
    .dxDateBox("instance");

  // dbSimpleInst.dispose();

  // Calender is only use for date or datetime
  // roller is only use for date and time.
  // list is only used to pick time and type is also type for list.

  const dbNativeInst = $("#dbNative")
    .dxDateBox({
      type: "date", // datetime
      pickerType: "native",
      value: new Date(2007, 0, 1),
      text: "Readonly field",
      readOnly: true,
      rtlEnabled: false,
    })
    .dxDateBox("instance");

  const dbListInst = $("#dbList")
    .dxDateBox({
      type: "time",
      pickerType: "list",
      interval: 10,
      stylingMode: "underlined",
    })
    .dxDateBox("instance");

  const dbRollersInst = $("#dbRollers")
    .dxDateBox({
      type: "time", // works for all date, datetime, time
      pickerType: "rollers",
      stylingMode: "filled",
    })
    .dxDateBox("instance");

  const dbAnalogInst = $("#dbAnalog")
    .dxDateBox({
      pickerType: "calender",
      type: "datetime",
      showAnalogClock: true,
      showClearButton: true,
      showDropDownButton: false,
      tabindex: 10,
      openOnFieldClick: true,
    })
    .dxDateBox("instance");
});
