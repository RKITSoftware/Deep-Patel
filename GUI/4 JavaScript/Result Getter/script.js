async function getResult(startEnrollment, endEnrollment) {
  const resultData = [];

  for (
    let enrollmentNumber = startEnrollment;
    enrollmentNumber <= endEnrollment;
    enrollmentNumber++
  ) {
    const formData = new FormData();
    formData.append("__EVENTTARGET", "");
    formData.append("__EVENTARGUMENT", "");
    formData.append("__VIEWSTATE", "");
    formData.append("__VIEWSTATEGENERATOR", "CA0B0334");
    formData.append("ddlbatch", "4107$W2023$2024-03-21$current$0");
    formData.append("txtenroll", enrollmentNumber);
    formData.append("txtSheetNo", "");
    formData.append("CodeNumberTextBox", "4147c0"); // capcha code
    formData.append("btnSearch", "Search");

    const response = await fetch("https://www.gturesults.in/", {
      method: "POST",
      body: formData,
      withCredentials: true,
    });

    const responseElement = document.createElement("div");
    responseElement.innerHTML = await response.text();
    const studentName = responseElement.querySelector("#lblName");
    const statusMessage = responseElement.querySelector("#lblmsg");
    const spiValue = responseElement.querySelector("#lblSPI");
    const cpiValue = responseElement.querySelector("#lblCPI");
    let subjectGrade = "";

    let etcGrade = "";
    let pasGrade = "";
    let icgrade = "";
    let deGrade = "";
    let dbmsGrade = "";
    let dfGrade = "";
    let dsaGrade = "";

    // Get the table with class "Rgrid" which contains the subject details
    var table = responseElement.getElementsByClassName("Rgrid")[1]; // Second instance of the "Rgrid" table

    // Get the rows of the table
    var rows = table.getElementsByTagName("tr");

    // Iterate through each row to find the subject name "Probability and Statistics"
    for (var i = 0; i < rows.length; i++) {
      var cells = rows[i].getElementsByTagName("td");
      var subjectName = cells[1].textContent.trim(); // Get the subject name from the second column

      if (subjectName === "Effective Technical Communication") {
        etcGrade = cells[6].textContent.trim();
      }
      if (subjectName === "Probability and Statistics") {
        pasGrade = cells[6].textContent.trim();
      }
      if (subjectName === "Indian Constitution") {
        icgrade = cells[6].textContent.trim();
      }
      if (subjectName === "Design Engineering - I A") {
        deGrade = cells[6].textContent.trim();
      }
      if (subjectName === "Database Management System") {
        dbmsGrade = cells[6].textContent.trim();
      }
      if (subjectName === "Digital Fundamentals") {
        dfGrade = cells[6].textContent.trim();
      }
      if (subjectName === "Data Structures and Algorithms") {
        dsaGrade = cells[6].textContent.trim();
      }
    }

    if (spiValue !== null) {
      resultData.push({
        enrollment: enrollmentNumber,
        name: studentName.innerHTML,
        status: statusMessage.innerHTML,
        spi: spiValue.innerHTML,
        cpi: cpiValue.innerHTML,
        etcGrade: etcGrade,
        pasGrade: pasGrade,
        icgrade: icgrade,
        deGrade: deGrade,
        dbmsGrade: dbmsGrade,
        dfGrade: dfGrade,
        dsaGrade: dsaGrade,
      });
    }
  }

  console.log(resultData);
}

// Example usage:
// getResult(1001, 1010);
