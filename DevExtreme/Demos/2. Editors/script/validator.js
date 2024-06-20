$(function () {
  $("#name")
    .dxTextBox({
      placeholder: "Enter the name",
      accessKey: "n",
      mode: "text",
      name: "firstName",
      onFocusOut: function (e) {
        e.element.dxValidator("instance").validate();
      },
    })
    .dxValidator({
      adapter: {
        getValue: function () {
          return $("#name").dxTextBox("option", "value");
        },
      },
      name: "First Name",
      elementAttr: {
        class: "test",
      },
      validationRules: [
        {
          type: "required",
          // message: "",
          // message: "name is required",
          trim: true, // false
        },
        {
          ignoreEmptyValue: false, // true
          type: "stringLength",
          min: 3,
          max: 10,
          // message: "",
          // message: "The length must be between 3 to 10 characters.",
          trim: true, // false
        },
      ],
    });

  $("#email")
    .dxTextBox({
      placeholder: "Enter the email",
      accessKey: "e",
      // mode: "email",
    })
    .dxValidator({
      name: "User Email",
      validationRules: [
        {
          type: "email",
          ignoreEmptyValue: false,
        },
      ],
    });

  $("#number")
    .dxTextBox({
      placeholder: "Enter the mobile number",
    })
    .dxValidator({
      name: "Mobile Number",
      validationRules: [
        {
          type: "numeric",
          ignoreEmptyValue: true,
          // message: "",
        },
        {
          ignoreEmptyValue: false,
          type: "pattern",
          pattern: /^[6-9]\d{9}$/,
          message: "Mobile number not in correct patten.",
        },
      ],
    });

  $("#age")
    .dxTextBox({
      placeholder: "Enter the age",
    })
    .dxValidator({
      name: "Age",
      validationRules: [
        {
          type: "numeric",
        },
        {
          type: "range",
          min: 1,
          max: 100,
          reevaluate: true,
        },
      ],
    });

  const usernameList = ["Deep", "Admin", "Root"];
  $("#username")
    .dxTextBox({
      placeholder: "Enter the username",
    })
    .dxValidator({
      name: "Username",
      validationRules: [
        {
          type: "custom",
          reevaluate: true,
          ignoreEmptyValue: false,
          message: "Username doesn't exist",
          validationGroup: "passwordTesting",
          validationCallback: function (e) {
            // console.log("Column ", e.column);
            // console.log("Data ", e.data);
            // console.log("Form item ", e.formItem);
            // console.log("Rule ", e.rule);
            // console.log("Validator ", e.validator);
            // console.log("Value ", e.value);

            return usernameList.includes(e.value);
          },
        },
      ],
    });

  $("#confirmUsername")
    .dxTextBox({
      placeholder: "Enter the username again.",
    })
    .dxValidator({
      validationGroup: "passwordTesting",
      validationRules: [
        {
          type: "compare",
          comparisonType: "===",
          comparisonTarget: function () {
            return $("#username").dxTextBox("option", "value");
          },
        },
      ],
    });

  $("#btnValidateGroup").dxButton({
    text: "Validate",
    validateGroup: "passwordTesting",
    onClick: function (e) {
      // let result = e.validationGroup.validate();
      // console.log(result);
      let result = DevExpress.validationEngine.validateGroup("passwordTesting");
      console.log(result);
    },
  });

  $("#newUsername")
    .dxTextBox({
      placeholder: "Enter new username",
    })
    .dxValidator({
      validationRules: [
        {
          type: "async",
          reevaluate: true,
          message: "Username already exists.",
          validationCallback: function (e) {
            return new Promise((resolve, reject) => {
              setTimeout(() => {
                if (usernameList.includes(e.value)) {
                  reject("Username already exists");
                } else {
                  resolve("Username is available.");
                }
              }, 500);
            });
          },
        },
      ],
    });

  $("#summary").dxValidationSummary({
    validationGroup: "passwordTesting",
  });
});
