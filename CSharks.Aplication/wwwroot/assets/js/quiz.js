var prev = 0;
var a;
$(document).ready(function () {
    if (prev > 2) {
        swal({
            title: "Success",
            text: "You clicked the button!",
            icon: "success",
        }).then(function () {
            window.location.href = "/Home/Index"
        });
    }
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
        if (response1 && prev <= 2) {
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
        else if (!response1 && prev <= 2) {
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