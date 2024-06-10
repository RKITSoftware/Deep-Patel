$(() => {
  // Created a demo button for the testing of the devExtreme CDN
  $("#buttonContainer").dxButton({
    text: "Click Me!", // Show the text to user
    onClick: function () {
      console.log("Click me button clicked."); // log the text when button clicked.
    },
    onInitialized: function () {
      console.log("Button initiated.");
    },
    onContentReady: function () {
      console.log("Content ready.");
    },
  });

  // Get & Set Properties
  // Getting a UI component Instance
  let buttonInst = $("#buttonContainer").dxButton("instance");

  // Two ways to get the property.

  // Using instance
  let btnVal1 = buttonInst.option("text");
  console.log("With Instance ", btnVal1);

  // Without instance
  let btnVal2 = $("#buttonContainer").dxButton("option", "text");
  console.log("Without instance", btnVal2);

  // Get all properties
  let buttonOptions = buttonInst.option();
  const _ = $("#buttonContainer").dxButton("option");
  console.log("All properties", buttonOptions);

  // Set the property
  buttonInst.option("text", "value set by instance.");

  // Set the property without instance
  $("#buttonContainer").dxButton("option", "text", "Without instance");

  // Set all properties
  buttonInst.option({
    text: "Setting all properties using instance.",
    onClick: function () {
      console.log("New updated event is clicked.");
    },
  });

  // Without instance
  $("#buttonContainer").dxButton({
    text: "Setting all properties without instance.",
    onClick: function () {
      console.log("New updated event is clicked.");
    },
  });

  // Call Methods
  $("#buttonContainer").dxButton().click();
  $("#buttonContainer").dxButton().click(function(){console.log("from call methods")});

  // binding event using on
  buttonInst.on({
    click: function () {
      console.log("button clicked using multiple event setter.");
    },
  });

  buttonInst.on("click", clickHandler1).on("click", clickHandler2);

  function clickHandler1() {
    console.log("Click handler 1");
  }

  function clickHandler2() {
    console.log("Click handler 2");
  }

  // unsubscribe the function which is given as second argument.
  buttonInst.off("click", clickHandler1);

  // unsubscribe all click event of button/
  // buttonInst.off("click");

  // unsubscribe using undefiend
  buttonInst.option("onClick", undefined);

  // dispose the button using instance
  // buttonInst.dispose();
  // $("#buttonContainer").dxButton("dispose");
  // $("#buttonContainer").remove();
});
