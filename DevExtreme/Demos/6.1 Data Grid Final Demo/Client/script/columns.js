import { CityCustomStore, studentAPIUrl } from "./data.js";

const masterColumns = [
  {
    allowEditing: false,
    caption: "Id",
    dataField: "U01F01",
    dataType: "number",
    allowGrouping: false,
    allowSorting: false,
    allowReordering: false,
    allowResizing: false,
    fixed: true,
    fixedPosition: "left",
  },
  {
    caption: "Name",
    dataField: "U01F02",
    dataType: "string",
    alignment: "right",
    validationRules: [
      {
        type: "required",
        message: "Name is required.",
      },
      {
        type: "stringLength",
        min: 5,
        max: 30,
        message: "Name length must between 5 to 30 characters.",
      },
    ],
  },
  {
    caption: "Age",
    dataField: "U01F03",
    dataType: "number",
    validationRules: [
      {
        type: "required",
        message: "Age is required.",
      },
      {
        type: "numeric",
        message: "Age must be numeric.",
      },
    ],
  },
  {
    caption: "Contact Details",
    alignment: "center",
    columns: [
      {
        allowGrouping: false,
        dataField: "U01F05",
        dataType: "number",
        caption: "Mobile No.",
        customizeText: function (cellInfo) {
          return cellInfo.value !== undefined ? `+91 ${cellInfo.value}` : null;
        },
        hidingPriority: 2,
        validationRules: [
          {
            type: "required",
            message: "Mobile number is required.",
          },
          {
            type: "pattern",
            pattern: /[6-9][0-9]{9}/,
            message:
              "Mobile number first digit is starts with 6 to 9 and length must be 10 characters.",
          },
          {
            type: "async",
            validationCallback: function (e) {
              return new Promise((resolve, reject) => {
                const { data } = e;
                $.ajax({
                  type: "GET",
                  url: `${studentAPIUrl}/ValidateMN/?id=${
                    data.U01F01 || 0
                  }&number=${data.U01F05}`,
                  dataType: "json",
                  success: function (response) {
                    if (response.isValid) {
                      resolve("Number is Unique.");
                    } else {
                      reject("Choose another mobile number");
                    }
                  },
                });
              });
            },
          },
        ],
      },
      {
        dataField: "U01F04",
        dataType: "string",
        caption: "Email Address",
        alignment: "right",
        hidingPriority: 1,
        validationRules: [
          {
            type: "required",
            message: "Email is required.",
          },
          {
            type: "email",
          },
        ],
      },
    ],
  },
  {
    dataField: "U01F06",
    dataType: "number",
    caption: "City",
    alignment: "right",
    hidingPriority: 0,
    lookup: {
      allowClearing: true,
      dataSource: CityCustomStore(),
      valueExpr: "Y01F01",
      displayExpr: "Y01F02",
    },
  },
  {
    type: "buttons",
    buttons: ["edit", "delete"],
  },
  {
    type: "adaptive",
    width: 50,
  },
];

const detailColumns = [
  {
    dataField: "C01F02",
    dataType: "string",
    caption: "School Name",
  },
  {
    dataField: "C01F03",
    dataType: "string",
    caption: "Qualification",
  },
  {
    dataField: "C01F04",
    dataType: "percentage",
    caption: "Percentage",
    customizeText: function (cellInfo) {
      return cellInfo.value !== undefined ? `${cellInfo.value}%` : null;
    },
  },
];

export { detailColumns, masterColumns };
