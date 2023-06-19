var prev = 0;
var a;
var points = 0;
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
        if (response1 && prev < 2) {
            $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                $("#add").html(response)
            });
            points += 100;
        }
        else if (!response1 && prev < 2) {
            $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                $("#add").html(response)
            });
            points -= 10;
        }
        else {
            swal({
                title: "Good job!",
                text: "You Finished the quiz!",
                icon: "success",
            }).then(function () {
                console.log(points);
                //location.href = "/Home/Index"
            });
        }
    })
}