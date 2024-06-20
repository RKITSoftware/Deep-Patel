$(function () {
  DevExpress.ui.dxFileUploader.defaultOptions({
    device: { deviceType: "desktop" },
    options: {
      accept: "*",
      abortUpload: function (file, uploadInfo) {
        console.log("File:- ", file);
        console.log("Upload Info :- ", uploadInfo);
      },
      allowedFileExtensions: [".jpeg", ".png", ".xlsx", ".js"],
      uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload",
      invalidFileExtensionMessage:
        "Invalid file extention choose format from .jpeg, .xlsx, .png",
      invalidMaxFileSizeMessage: "Lower the size of the file",
      invalidMinFileSizeMessage: "Higher the size of the file",
      maxFileSize: 1000000,
      minFileSize: 1000,
      name: "fileUploader",
      onInitialized: function (e) {},
      onContentReady: function (e) {},
      onDisposing: function (e) {},
      onOptionChanged: function (e) {},
      onDropZoneEnter: function (e) {
        console.log("file entered in dropzone");
      },
      onDropZoneLeave: function (e) {
        console.log("file left the dropzone");
      },
      uploadMethod: "POST", // PUT
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
      onValueChanged: function (e) {},
    },
  });

  const fuInstant = $("#fuInstant")
    .dxFileUploader({
      dialogTrigger: $("#dialogTrigger"),
      accessKey: "a",
      activeStateEnabled: true,
      allowCanceling: true,
      dropZone: $("#dropzone"),
      chunkSize: 1000,
      uploadMode: "instantly",
      visible: true,
      //   width: "100px",
      rtlEnabled: false,
      isValid: true,
      labelText: "drop files in dropZone",
      readOnly: false,
      disabled: false,
      elementAttr: {
        class: "my-class",
      },
      tabIndex: 1,
      focusStateEnabled: true,
      //   height: "100px",
      hint: "instantlt file uploader",
      hoverStateEnabled: true,
      onBeforeSend: function (e) {
        // calls for the every chunk file send.
        // alert("Before send");
      },
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
    .dxFileUploader("instance");

  const fuButton = $("#fuButton")
    .dxFileUploader({
      accessKey: "b",
      activeStateEnabled: false,
      chunkSize: 125000,
      uploadMode: "useButtons",
      multiple: true,
      readyToUploadMessage: "Files are successfully ready to upload.",
      hint: "use button file uploader.",
      showFileList: true,
    })
    .dxFileUploader("instance");

  const fuForm = $("#fuForm")
    .dxFileUploader({
      accessKey: "c",
      uploadMode: "useForm",
      hint: "form file uploader.",
      inputAttr: {
        name: "fileFromForm",
      },
      uploadCustomData: {
        __RequestVerificationToken: document.getElementsByName(
          "__RequestVerificationToken"
        )[0].value,
      },
    })
    .dxFileUploader("instance");

  const btnSubmit = $("#btnSubmit")
    .dxButton({
      text: "Submit",
      useSubmitBehaviour: true,
      onClick: function () {
        $("#form").submit();
      },
    })
    .dxButton("instance");
});
