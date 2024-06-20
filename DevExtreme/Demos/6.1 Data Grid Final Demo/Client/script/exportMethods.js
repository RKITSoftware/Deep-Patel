import { getEducation, studentArray } from "./data.js";

window.jsPDF = window.jspdf.jsPDF;

function pdfMaster(e) {
  const doc = new jsPDF();

  DevExpress.pdfExporter
    .exportDataGrid({
      jsPDFDocument: doc,
      component: e.component,
      indent: 5,
    })
    .then(() => {
      doc.save("StudentsMasterData.pdf");
    });
}

function excelMaster(e) {
  var workbook = new ExcelJS.Workbook();
  var worksheet = workbook.addWorksheet("Main sheet");
  DevExpress.excelExporter
    .exportDataGrid({
      worksheet: worksheet,
      component: e.component,
      customizeCell: function (options) {
        var excelCell = options;
        excelCell.font = { name: "Arial", size: 12 };
        excelCell.alignment = { horizontal: "left" };
      },
    })
    .then(function () {
      workbook.xlsx.writeBuffer().then(function (buffer) {
        saveAs(
          new Blob([buffer], { type: "application/octet-stream" }),
          "StudentsMasterData.xlsx"
        );
      });
    });
  e.cancel = true;
}

function excelMasterDetail(e) {
  var workbook = new ExcelJS.Workbook();
  var worksheet = workbook.addWorksheet("Student");

  const exportAndProcessDataGrid = async () => {
    const borderStyle = { style: "thin", color: { argb: "FF7E7E7E" } };
    let offset = 0;
    const masterRows = [];

    const insertRow = (index, offset, outlineLevel) => {
      const currentIndex = index + offset;
      const row = worksheet.insertRow(currentIndex, [], "n");

      for (let j = worksheet.rowCount + 1; j > currentIndex; j--) {
        worksheet.getRow(j).outlineLevel = worksheet.getRow(j - 1).outlineLevel;
      }

      row.outlineLevel = outlineLevel;

      return row;
    };

    try {
      const cellRange = await DevExpress.excelExporter.exportDataGrid({
        component: e.component,
        worksheet: worksheet,
        topLeftCell: { row: 2, column: 2 },
        customizeCell: function ({ gridCell, excelCell }) {
          if (gridCell.column.index === 1 && gridCell.rowType === "data") {
            masterRows.push({
              rowIndex: excelCell.fullAddress.row + 1,
              data: gridCell.data,
            });
          }
        },
      });

      for (let i = 0; i < masterRows.length; i++) {
        let row = insertRow(masterRows[i].rowIndex + i, offset++, 2);
        let columnIndex = cellRange.from.column + 1;
        row.height = 40;

        let studentData = studentArray.find(
          (item) => item.U01F01 === masterRows[i].data.U01F01
        );

        const columns = ["Name", "Qualification", "Percentage"];
        columns.forEach((columnName, currentColumnIndex) => {
          Object.assign(row.getCell(columnIndex + currentColumnIndex), {
            value: columnName,
            fill: {
              type: "pattern",
              pattern: "solid",
              fgColor: { argb: "BEDFE6" },
            },
            font: { bold: true },
            border: {
              bottom: borderStyle,
              left: borderStyle,
              right: borderStyle,
              top: borderStyle,
            },
          });
        });

        let arr = await getEducation(studentData.U01F01);
        const columnValue = ["C01F02", "C01F03", "C01F04"];

        for (const edu of arr) {
          row = insertRow(masterRows[i].rowIndex + i, offset++, 2);

          columnValue.forEach((columnName, currentColumnIndex) => {
            Object.assign(row.getCell(columnIndex + currentColumnIndex), {
              value: edu[columnName],
              fill: {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "BEDFE6" },
              },
              border: {
                bottom: borderStyle,
                left: borderStyle,
                right: borderStyle,
                top: borderStyle,
              },
            });
          });
        }
        row = insertRow(masterRows[i].rowIndex + i, offset++, 2);
        offset--;
      }

      const buffer = await workbook.xlsx.writeBuffer();
      saveAs(
        new Blob([buffer], { type: "application/octet-stream" }),
        "Students.xlsx"
      );
    } catch (error) {
      console.error("An error occurred during export or processing:", error);
    }
  };

  // Usage
  exportAndProcessDataGrid();

  e.cancel = true;
}

export { excelMaster, excelMasterDetail, pdfMaster };
