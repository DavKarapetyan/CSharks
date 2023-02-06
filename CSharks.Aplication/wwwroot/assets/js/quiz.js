$(document).ready(function () {
    var a;

    $("#load").click(function () {
        a = $("#load").data("id");
        console.log(a);
        $.get("/Quiz/Question?prev=0&quizType=" + a, function (response) {
            $("#add").html(response)
        });
    });
});

function check(a) {
    $.post("/Quiz/CheckAnswer?questionAnswerId=" + a, function (response1) {
        if (response1) {
            swal({
                title: "Good job!",
                text: "You clicked the button!",
                icon: "success",
            }).then(function () {
                window.location = "/Home/Index";
            });
        }
        else {
            swal("Oops", "Something went wrong!", "error")
        }
    })
}