var prev = 0;
var a;
$(document).ready(function () {

    $("#load").click(function () {
        a = $("#load").data("id");
        console.log(a);
        $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
            $("#add").html(response)
        });
    });
});

function check(b) {
    $.post("/Quiz/CheckAnswer?questionAnswerId=" + b, function (response1) {
        prev += 1;
        if (response1) {
            swal({
                title: "Good job!",
                text: "You clicked the button!",
                icon: "success",
            }).then(function () {
                $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                    $("#add").html(response)
                });
            });
        }
        else {
            swal({
                title: "Oops",
                text: "You clicked the button!",
                icon: "error",
            }).then(function () {
                $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                    $("#add").html(response)
                });
            });
        }
    })
}