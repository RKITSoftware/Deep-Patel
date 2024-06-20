$(function () {
  const myValidateGroup = "myValidateGroup";

  const tbNameInst = $("#tbName")
    .dxTextBox({
      accessKey: "n",
      hint: "Enter your name...",
      inputAttr: {
        name: "inpName",
      },
      maxLength: 30,
      placeholder: "Enter your name...",
      showClearButton: true,
      spellcheck: true,
      stylingMode: "underlined",
      tabIndex: 1,
      valueChangeEvent: "blur",
      mode: "text",
      buttons: ["clear"],
      onCut: function () {
        alert("Name successfully cut.");
      },
      onCopy: function () {
        alert("Name successfully copied.");
      },
      onPaste: function () {
        alert("Data successfully pasted.");
      },
      onFocusIn: function (e) {
        console.log("Name is", e.component.option("value"));
      },
    })
    .dxValidator({
      name: "Name",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
        {
          type: "stringLength",
          ignoreEmptyValue: false,
          min: 5,
          max: 30,
          trim: true,
        },
      ],
    })
    .dxTextBox("instance");

  const dbDOBInst = $("#dbDOB")
    .dxDateBox({
      tabIndex: 2,
      accessKey: "d",
      adaptivityEnabled: true,
      applyButtonText: "Confirm",
      applyValueMode: "useButtons",
      buttons: [
        {
          name: "Clear",
          location: "after",

          options: {
            icon: "clear",
            onClick() {
              dbDOBInst.option("value", null);
            },
          },
        },
        "dropDown",
      ],
      dateSerializationFormat: "yyyy-MM-dd",
      dateOutOfRangeMessage: "Choose date in min and max.",
      deferRendering: true,
      hint: "Choose date of birth.",
      inputAttr: {
        name: "inpDOB",
      },
      invalidDateMessage: "Enter date in correct format.",
      max: Date.now(),
      maxLength: 10,
      min: new Date(1960, 0, 1),
      name: "inpDOB",
      openOnFieldClick: true,
      pickerType: "calender",
      placeholder: "Choose date of birth",
      stylingMode: "underlined",
    })
    .dxValidator({
      name: "Date of Birth",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
      ],
    })
    .dxDateBox("instance");

  const nbAgeInst = $("#nbAge")
    .dxNumberBox({
      accessKey: "a",
      buttons: ["clear", "spins"],
      hint: "Enter age...",
      inputAttr: {
        name: "inpAge",
      },
      invalidValueMessage: "Enter valid number",
      min: 0,
      max: 100,
      mode: "number",
      onKeyDown: function (e) {
        let originalEvent = e.event.originalEvent;

        if (originalEvent.key === "Enter") {
          if (originalEvent.ctrlKey === true) {
            nbAgeInst.option("value", nbAgeInst.option("value") - 1);
          } else {
            nbAgeInst.option("value", nbAgeInst.option("value") + 1);
          }
        }
      },
      placeholder: "Enter age...",
      stylingMode: "underlined",
      showSpinButtons: true,
      showClearButton: true,
      tabIndex: 2,
      value: 0,
    })
    .dxValidator({
      name: "Age",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
        {
          type: "numeric",
        },
        {
          type: "range",
          min: 0,
          max: 100,
          reevaluate: true,
        },
      ],
    })
    .dxNumberBox("instance");

  const genderList = ["Male", "Female", "Other"];
  const rgGenderInst = $("#rgGender")
    .dxRadioGroup({
      accessKey: "g",
      dataSource: genderList,
      hint: "Choose gender",
      value: "Male",
      layout: "horizontal",
      name: "inpGender",
      tabIndex: 4,
    })
    .dxValidator({
      name: "Gender",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
      ],
    })
    .dxRadioGroup("instance");

  const tbUsernameInst = $("#tbUsername")
    .dxTextBox({
      accessKey: "u",
      hint: "Enter username",
      inputAttr: {
        name: "inpUsername",
      },
      maxLength: 10,
      placeholder: "Enter username...",
      stylingMode: "underlined",
      tabIndex: 5,
      valueChangeEvent: "blur",
      mode: "text",
      buttons: ["clear"],
    })
    .dxValidator({
      name: "Username",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
        {
          type: "async",
          reevaluate: true,
          validationCallback: function (e) {
            return new Promise((resolve, reject) => {
              setTimeout(() => {
                if (isNaN(e.value.charAt(0))) {
                  resolve("Username is valid");
                } else {
                  reject("Username is invalid.");
                }
              }, 2000);
            });
          },
        },
      ],
    })
    .dxTextBox("instance");

  const tbEmailInst = $("#tbEmail")
    .dxTextBox({
      accessKey: "e",
      hint: "Enter your email...",
      inputAttr: {
        name: "inpEmail",
      },
      maxLength: 20,
      placeholder: "Enter your email...",
      spellcheck: true,
      stylingMode: "underlined",
      tabIndex: 6,
      valueChangeEvent: "blur",
      mode: "email",
    })
    .dxValidator({
      name: "Email",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "email",
        },
      ],
    })
    .dxTextBox("instance");

  const tbPasswordInst = $("#tbPassword")
    .dxTextBox({
      accessKey: "p",
      hint: "Enter your password...",
      inputAttr: {
        name: "inpPassword",
      },
      maxLength: 20,
      placeholder: "Enter your password...",
      spellcheck: true,
      stylingMode: "underlined",
      tabIndex: 7,
      mode: "password",
    })
    .dxValidator({
      name: "Password",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
        {
          type: "pattern",
          pattern:
            /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,15}$/,
        },
      ],
    })
    .dxTextBox("instance");

  $("#tbConfirmPassword")
    .dxTextBox({
      accessKey: "c",
      hint: "Confirm your password...",
      inputAttr: {
        name: "inpConfirmPassword",
      },
      maxLength: 20,
      placeholder: "Enter your password...",
      spellcheck: true,
      stylingMode: "underlined",
      tabIndex: 8,
    })
    .dxValidator({
      name: "Confirm Password",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "compare",
          comparisonType: "===",
          comparisonTarget: function () {
            return tbPasswordInst.option("value");
          },
        },
      ],
    });

  const educationList = [
    {
      id: 1,
      education: "B.E.",
    },
    {
      id: 2,
      education: "M.E",
    },
    {
      id: 3,
      education: "B.Sc.",
    },
    {
      id: 4,
      education: "M.Sc.",
    },
    {
      id: 5,
      education: "B.Com.",
    },
    {
      id: 6,
      education: "M.Com.",
    },
  ];

  const ddEducationInst = $("#ddEducation")
    .dxDropDownBox({
      acceptCustomValue: true,
      accessKey: "e",
      contentTemplate: (e) => {
        const $list = $("<div>").dxList({
          dataSource: e.component.getDataSource(),
          itemTemplate: function (item) {
            return $("<div>").text(item.education);
          },
          selectionMode: "multiple",
          showSelectionControls: true,
          onItemClick: (selected) => {
            e.component.option("value", selected.itemData.id);
            e.component.close();
          },
          onSelectionChanged: (args) => {
            let arr = args.component.option("selectedItems");
            Array.isArray(arr) ? arr.join(", ") : arr;
            e.component.option("value", arr);
          },
          selectByClick: true,
        });
        return $list;
      },
      dataSource: educationList,
      deferRendering: true,
      valueExpr: "id",
      displayExpr: "education",
      displayValueFormatter: function (val) {
        return Array.isArray(val) ? val.join(", ") : val;
      },
      hint: "Choose Education",
      inputAttr: {
        name: "inpEducation",
      },
      openOnFieldClick: true,
      placeholder: "Choose Education",
      stylingMode: "underlined",
      showClearButton: true,
      tabIndex: 9,
    })
    .dxValidator({
      name: "Education",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
      ],
    })
    .dxDropDownBox("instance");

  const languageList = [
    {
      id: 1,
      language: "Gujarati",
    },
    {
      id: 2,
      language: "Hindi",
    },
    {
      id: 3,
      language: "English",
    },
  ];

  const sbLanguageInst = $("#sbLanguage")
    .dxSelectBox({
      accessKey: "l",
      dataSource: languageList,
      displayExpr: "language",
      valueExpr: "id",
      deferRendering: false,
      showClearButton: true,
      showDropDownButton: true,
      buttons: ["clear", "dropDown"],
      hint: "Choose language..",
      inputAttr: {
        name: "inpLanguage",
      },
      itemTemplate: function (val) {
        return val.language;
      },
      noDataText: "Empty",
      openOnFieldClick: true,
      placeholder: "Choose language.",
      searchEnabled: true,
      searchMode: "contains", // startsWith
      tabIndex: 10,
      text: "select language.",
      stylingMode: "underlined",
      spellcheck: true,
      wrapItemText: true,
      dropDownButtonTemplate: function (icon, text) {
        icon = "chevrondown"; // The button's icon.
        text.prepend(`<span class="dx-icon-${icon}"></span>`); // The button's text.
      },
      useItemTextAsTitle: true,
      showDataBeforeSearch: false,
      searchTimeout: 0,
      showSelectionControls: true,
    })
    .dxValidator({
      name: "Language",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "custom",
          reevaluate: true,
          validationCallback: function (e) {
            return e.value !== undefined && e.value !== null;
          },
        },
      ],
    })
    .dxSelectBox("instance");

  const taAddressInst = $("#taAddress")
    .dxTextArea({
      accessKey: "s",
      autoResizeEnabled: true,
      hint: "Enter the address.",
      inputAttr: {
        name: "inpAddress",
      },
      placeholder: "Enter address",
      spellcheck: true,
      stylingMode: "underlined",
      tabIndex: 10,
      text: "Home Address",
      valueChangeEvent: "blur",
    })
    .dxValidator({
      name: "Address",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
          trim: true,
        },
        {
          type: "stringLength",
          min: 5,
          max: 1000,
          trim: true,
        },
      ],
    })
    .dxTextArea("instance");

  DevExpress.ui.dxFileUploader.defaultOptions({
    device: { deviceType: "desktop" },
    options: {
      accept: "*",
      abortUpload: function (file, uploadInfo) {
        console.log("File:- ", file);
        console.log("Upload Info :- ", uploadInfo);
      },
      allowedFileExtensions: [".jpeg", ".png", ".jpg"],
      uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
      invalidFileExtensionMessage:
        "Invalid file extention choose format from .jpeg, .png, .jpg",
      invalidMaxFileSizeMessage: "Lower the size of the file",
      invalidMinFileSizeMessage: "Higher the size of the file",
      minFileSize: 20000,
      maxFileSize: 4000000,
      onDropZoneEnter: function (e) {
        console.log("file entered in dropzone");
      },
      onDropZoneLeave: function (e) {
        console.log("file left the dropzone");
      },
      uploadMethod: "POST",
      uploadHeaders: {
        "My-Name": "deep-patel",
      },
      uploadAbortedMessage: "Abort the sending file operation",
      uploadedMessage: "File uploaded successfully.",
      uploadFailedMessage: "File can't be uploaded because of server error.",
      selectButtonText: "Choose",
      uploadButtonText: "My Upload",
      onFilesUploaded: function (e) {
        alert("files uploaded successfully from onFilesUploaded");
      },
    },
  });

  const fuProfilePicInst = $("#fuProfilePic")
    .dxFileUploader({
      accessKey: "a",
      uploadMode: "useForm",
      hint: "form file uploader.",
      inputAttr: {
        name: "inpProfilePic",
      },
      chunkSize: 10000,
      labelText: "drop files in dropZone",
      tabIndex: 11,
      hint: "Profile pic.",
      onUploadAborted: function (e) {
        console.log("Aborted file :- ", e.file);
        console.log("Message :- ", e.message);
      },
      onUploaded: function (e) {
        console.log("File upload successfully message :- ", e.message);
      },
      onUploadStarted: function (e) {
        console.log("File :- ", e.file);
        console.log("Request :- ", e.request);
      },
      onUploadError: function (e) {
        console.log("Error occured during file uploading of ", e.file);
      },
      onProgress: function (e) {
        console.log("Bytes Loaded ", e.bytesLoaded);
        console.log("Bytes Totel ", e.bytesTotal);
        console.log("Progress :- ", fuInstant.option("progress"));
      },
      uploadChunk: function (file, uploadInfo) {
        console.log("from uplpoad chunk", file, uploadInfo);
      },
    })
    .dxValidator({
      name: "Profile pic",
      validationGroup: myValidateGroup,
      validationRules: [
        {
          type: "required",
        },
      ],
    })
    .dxFileUploader("instance");

  const cbAcceptTCInst = $("#cbAcceptTC")
    .dxCheckBox({
      accessKey: "t",
      hint: "Accept terms & conditions.",
      name: "inp-TandC",
      onValueChanged: function (e) {
        console.log("Previous Value :- ", e.previousValue);
        console.log("Current Value :- ", e.value);
      },
      tabIndex: 12,
      text: "Accept terms & conditions.",
    })
    .dxCheckBox("instance");

  const btnResetInst = $("#btnReset")
    .dxButton({
      accessKey: "r",
      hint: "Reset",
      icon: "clear",
      stylingMode: "outlined",
      tabIndex: 13,
      text: "Reset",
      type: "danger",
    })
    .dxButton("instance");

  btnResetInst.on("click", function () {
    $("#userForm")[0].reset();
  });

  $("#btnSubmit").dxButton({
    accessKey: "s",
    hint: "Submit",
    onClick: function () {
      let result = DevExpress.validationEngine.validateGroup(myValidateGroup);
      if (result.isValid) {
        let userData = {
          name: tbNameInst.option("value"),
          dateOfBirth: dbDOBInst.option("value"),
          age: nbAgeInst.option("value"),
          gender: rgGenderInst.option("value"),
          username: tbUsernameInst.option("value"),
          email: tbEmailInst.option("value"),
          password: tbPasswordInst.option("value"),
          education: ddEducationInst.option("value"),
          language: sbLanguageInst.option("value"),
          address: taAddressInst.option("value"),
          profilePic: fuProfilePicInst.option("value"),
          "t&c": cbAcceptTCInst.option("value"),
        };

        sessionStorage.setItem("userData", JSON.stringify(userData));
       alert("Form submitted successfully.");
       $("#userForm")[0].reset();
      } else {
        console.log(result.brokenRules);
      }
    },
    stylingMode: "outlined",
    tabIndex: 14,
    text: "Submit",
    type: "success",
    useSubmitBehaviour: true,
  });
});
