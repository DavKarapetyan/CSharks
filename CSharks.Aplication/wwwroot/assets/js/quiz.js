var prev = 0;
var a;


function getInfo(quizTypeId, quizType) {
    Swal.fire({
        title: "Info",
        text: "If your are not registered your result doesnt save! For register click join now button!",
        icon: "info",
        showCancelButton: true,
        confirmButtonText: 'GO',
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = "/Quiz/GetQuestions?quizTypeId=" + quizTypeId + "&" + "QuizType=" + quizType
        }
    });
}

$(document).ready(function () {
    $("#load").click(function () {
        a = $("#load").data("id");
        console.log(a);
        if (prev < 2) {
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
    if (prev < 2) {
        $.post("/Quiz/CheckAnswer?questionAnswerId=" + b, function (response1) {
            if (response1) {
                $.post("/Quiz/AddQuizScore", { questionId: c, questionAnswerId: b, quizTypeId: a, score: 100 }, function () {
                    console.log("Score is added");
                });
                $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                    $("#add").html(response)
                });
            }
            if (!response1) {
                $.post("/Quiz/AddQuizScore", { questionId: c, questionAnswerId: b, quizTypeId: a, score: -10 }, function () {
                    console.log("Score is added");
                });
                $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
                    $("#add").html(response)
                });
            }
        })
    }
    else {
        Swal.fire({
            title: "Good job!",
            text: "You Finished the quiz!",
            icon: "success",
        }).then(function () {
            console.log(points);
            location.href = "/Home/Index"
        });
    }
    prev += 1;
}