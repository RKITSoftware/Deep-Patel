$(function () {
  var $orderId = $("#orderId");
  var $name = $("#name");
  var $drink = $("#drink");

  function AddOrder(order) {
    $("#orderData").append(
      "<tr><td> " +
        order.id +
        "</td><td>" +
        order.name +
        "</td><td>" +
        order.drink +
        "</td><td>" +
        "<button data-id='" +
        order.id +
        "' class='deleteOrder'>Delete</button></td><td><button data-id='" +
        order.id +
        "' class='editOrder'>Edit</button></td></tr>"
    );
  }

  $.ajax({
    type: "GET",
    url: "https://localhost:7233/api/Order",
    success: function (response) {
      $.each(response.data, function (_i, order) {
        AddOrder(order);
      });
    },
  });

  $("#addOrder").on("click", function () {
    var order = {
      Id: $orderId.val(),
      Name: $name.val(),
      Drink: $drink.val(),
    };

    console.log(order);

    $.ajax({
      type: "POST",
      url: "https://localhost:7233/api/Order",
      contentType: "application/json",
      data: JSON.stringify(order),
      success: function (response) {
        AddOrder(response.data);
      },
    });
  });

  $("#orderData").delegate(".deleteOrder", "click", function () {
    var $self = $(this);
    var $DeleteId = $self.attr("data-id");
    var $tr = $self.closest("tr");

    $.ajax({
      type: "DELETE",
      url: "https://localhost:7233/api/Order/" + $DeleteId,
      success: function () {
        $tr.remove();
      },
    });
  });

  $("#orderData").delegate(".editOrder", "click", function () {
    var $self = $(this);
    var $EditId = $self.attr("data-id");

    $.ajax({
      type: "GET",
      url: "https://localhost:7233/api/Order/" + $EditId,
      success: function (response) {
        $("#editOrderId").val(response.data.id);
        $("#editName").val(response.data.name);
        $("#editDrink").val(response.data.drink);
      },
      error: function (error) {
        alert(error.statuscode);
      },
    });
  });

  $("#editSave").on("click", function () {
    var order = {
      Id: $("#editOrderId").val(),
      Name: $("#editName").val(),
      Drink: $("#editDrink").val(),
    };

    $.ajax({
      type: "PUT",
      url: "https://localhost:7233/api/Order",
      contentType: "application/json",
      data: JSON.stringify(order),
      success: function () {
        alert("Success");
      },
    });
  });
});
