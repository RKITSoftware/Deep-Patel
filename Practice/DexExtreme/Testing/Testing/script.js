$(function () {
    const checkbox = $("#temp").dxCheckBox({
        accessKey: "c",
        text: "Simple Check",
        name: "MyCheckBox",
        elementAttr: {
            class: "checkMe"
        },
        height: "50px",
        width: "120px",
        enableThreeStateBehavior: false,
        value: true,
        focusStateEnabled: true,
        hint: "You Need To Check This",
        rtlEnabled: true,
        readOnly: false,
        visible: true
    }).dxCheckBox("instance");
});