import { menuItems } from "./data.js";
import { generateRandomId, notify } from "./helper.js";

$(function () {
  const storageKey = "dx-data-localStore-NavDemo";
  let tbName, nbAge, tbCity;

  const localStore = new DevExpress.data.LocalStore({
    key: "id",
    name: storageKey,
  });

  const navbarMenu = $("#navbarMenu")
    .dxMenu({
      items: menuItems,
      itemsExpr: "items",
      displayExpr: "text",
      orientation: "horizontal",
      // disabledExpr: "disabled",
      hideSubmenuOnMouseLeave: true,
      submenuDirection: "auto",
      showFirstSubmenuMode: "onHover",
      selectByClick: true,
      selectionMode: "single",

      // Events
      onItemClick(e) {
        navbarMenu.option("selectedItem", e.itemData);

        if (e.itemData.text === "Create") {
          addPopUpEmployee.show();
        }
      },

      // Default
      accessKey: "n",
      activeStateEnabled: true,
      adaptivityEnabled: true,
      disabled: false,
      elementAttr: {
        class: "finalDemoNavbar",
      },
      focusStateEnabled: true,
      hint: "Navbar",
      hoverStateEnabled: true,
      rtlEnabled: false,
      visible: true,
      tabIndex: 1,
    })
    .dxMenu("instance");

  const employeeDataGrid = $("#employeeDataGrid")
    .dxDataGrid({
      dataSource: localStore,
      editing: {
        allowDeleting: true,
        confirmDelete: false,
      },
    })
    .dxDataGrid("instance");

  const addPopUpEmployee = $("#addPopUpEmployee")
    .dxPopup({
      contentTemplate: function (contentElement) {
        contentElement.append(
          `<div class="dx-fieldset">
            <div class="dx-field">
              <div class="dx-field-label">Name</div>
              <div class="dx-field-value">
                <div id="tbName"></div>
              </div>
            </div>
            <div class="dx-field">
              <div class="dx-field-label">Age</div>
              <div class="dx-field-value">
                <div id="nbAge"></div>
              </div>
            </div>
            <div class="dx-field">
              <div class="dx-field-label">City</div>
              <div class="dx-field-value">
                <div id="tbCity"></div>
              </div>
            </div>
            <div id='termsandconditions'><span id='cbTermsConditions' style:'margin-right: 10px;'></span>Terms & Conditions</div>
            <div id='popover'>Rules</div>
          </div>`
        );

        $("#cbTermsConditions").dxCheckBox();

        $("#popover").dxPopover({
          target: "#termsandconditions",
          showEvent: "dxclick",
          animation: {
            show: { type: "fade", from: 0, to: 1 },
            hide: { type: "fade", from: 1, to: 0 },
          },
          position: {
            of: "#cbTermsConditions",
            at: "bootom right",
          },
          contentTemplate: function (popoverContent) {
            popoverContent.append(
              `<div>
                    <p>Terms & Conditions content goes here...</p>
                    <p>Additional details...</p>
                </div>`
            );
          },
        });

        tbName = $("#tbName")
          .dxTextBox({
            placeholder: "Enter name",
          })
          .dxTextBox("instance");

        nbAge = $("#nbAge")
          .dxNumberBox({
            placeholder: "Enter age",
            min: 0,
            max: 120,
            showSpinButtons: true,
          })
          .dxNumberBox("instance");

        tbCity = $("#tbCity")
          .dxTextBox({
            placeholder: "Enter city",
          })
          .dxTextBox("instance");
      },

      closeOnOutsideClick: true,
      showTitle: true,
      title: "Employee Form",
      position: "center",
      shading: true,
      shadingColor: "rgba(0,0,0,0.4)",

      toolbarItems: [
        {
          widget: "dxButton",
          toolbar: "bottom",
          location: "after",
          options: {
            text: "Add",
            type: "default",
            stylingMode: "contained",
            onClick: async () => {
              try {
                loadPanel.show();
                await new Promise((resolve) => {
                  setTimeout(() => {
                    resolve();
                  }, 1000);
                });
                await addEmployee();
                addPopUpEmployee.hide();
              } catch (error) {
                console.error("Error adding employee:", error);
              } finally {
                loadPanel.hide();
              }
            },
          },
        },
        {
          widget: "dxButton",
          toolbar: "bottom",
          location: "after",
          options: {
            text: "Cancel",
            type: "default",
            stylingMode: "contained",
            onClick() {
              addPopUpEmployee.hide();
            },
          },
        },
      ],

      // Common
      rtlEnabled: false,
      deferRendering: true,
      focusStateEnabled: true,
      hint: "Employee Form",
      hoverStateEnabled: true,
      visible: false,
    })
    .dxPopup("instance");

  const toast = $("#toast")
    .dxToast({
      deferRendering: true,
      focusStateEnabled: true,
      height: 50,
      width: 300,
      hint: "Toast Notification",
      hoverStateEnabled: true,
      type: "info", // 'custom' | 'error' | 'info' | 'success' | 'warning'
      displayTime: 1000,

      position: "top right",

      // Close Properties
      closeOnClick: true,
      closeOnOutsideClick: false,

      // Resize Properties
      maxHeight: "200px",
      maxWidth: "1000px",
      minHeight: "50px",
      minWidth: "600px",
    })
    .dxToast("instance");

  const loadPanel = $("#loadPanel")
    .dxLoadPanel({
      contentTemplate: function (container) {
        $("<div>")
          .dxLoadIndicator({
            indicatorSrc: "/assets/1473.gif",
          })
          .appendTo(container);
      },
      shading: true,
      visible: false,
      showPane: true,
      shadingColor: "rgba(0,0,0,0.4)",

      position: "center",
      message: "Loading...",
      delay: 0,

      onShown() {
        setTimeout(() => {
          loadPanel.hide();
        }, 5000);
      },
      onHidden: function () {},

      focusStateEnabled: false,
      hoverStateEnabled: true,
      hint: "Loading the data",
    })
    .dxLoadPanel("instance");

  async function addEmployee() {
    return new Promise((resolve, reject) => {
      let employeeData = {
        id: generateRandomId(), // Assuming generateRandomId() is defined
        name: tbName.option("value"),
        age: nbAge.option("value"),
        city: tbCity.option("value"),
      };

      localStore
        .insert(employeeData)
        .done(function () {
          console.log("Employee added successfully");
          employeeDataGrid.refresh();
          notify(toast, "Success.", "success");
          resolve();
        })
        .fail(function (error) {
          console.error("Error adding employee:", error);
          reject(error); // Reject the promise on failure
        });
    });
  }
});
