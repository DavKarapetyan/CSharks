var prev = 0;
var a;
var count;

$(document).ready(function () {
    $("#load").click(function () {
        a = $("#load").data("id");
        console.log(a);
        $.get("/Quiz/GetQuestionCount?quizTypeId=" + a, function (response) {
            count = response;
        });
        if (prev < count) {
            $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                $("#add").html(response)
            });
        }
        else {
            console.log("errorrrrrrrrr");
        }
    });
});

function check(b, c) {
    if (prev < count) {
        $.post("/Quiz/CheckAnswer?questionAnswerId=" + b, function (response1) {
            explanation = $("#explan").data("text");
            console.log("Text: " + explanation);
            if (response1) {
                Swal.fire({
                    title: "Good job!",
                    text: "Correct answer",
                    icon: "success"
                }).then(function () {
                    $.post("/Quiz/AddQuizScore", { questionId: c, questionAnswerId: b, quizTypeId: a, score: 100 }, function () {
                        console.log("Score is added");
                    });
                    $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                        $("#add").html(response)
                    });
                    if (prev >= count) {
                        location.href = "/Quiz/Index";
                    }
                });
            }
            if (!response1) {
                Swal.fire({
                    title: "Oops!",
                    text: explanation,
                    icon: "error"
                }).then(function () {
                    $.post("/Quiz/AddQuizScore", { questionId: c, questionAnswerId: b, quizTypeId: a, score: -10 }, function () {
                        console.log("Score is added");
                    });
                    $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                        $("#add").html(response)
                    });
                    if (prev >= count) {
                        location.href = "/Quiz/Index";
                    }
                });
            }
        })
    }
    prev += 1;
}