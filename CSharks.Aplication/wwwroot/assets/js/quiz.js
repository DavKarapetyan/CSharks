var prev = 0;
var a;
var count;
var score = 0;
var questionId = 0;
var answerId = 0;
var perScore = 0;
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
function check(b, c, el) {
    questionId = c;
    answerId = b;
    perScore = 0;
    if (prev < count) {
        var allAnswers = document.querySelectorAll(".answer");
        for (var i = 0; i < allAnswers.length; i++) {
            if (allAnswers[i] === el) {
                el.classList.add("active");
            }
            else {
                allAnswers[i].classList.remove("active");
            }
            allAnswers[i].classList.add("disabled");
        }
        el.classList.remove("disabled");
        $.post("/Quiz/CheckAnswer?questionAnswerId=" + b, function (response1) {
            explanation = $("#explan").data("text");
            console.log("Text: " + explanation);
            if (response1) {
                perScore = 100;
                $(el).addClass("correct");
            }
            if (!response1) {
                perScore = -10;
                $(el).addClass("false");
            }
        })
    }
    prev += 1;
}
function goToNext() {
    score += perScore;
    if (prev < count) {
        $.post("/Quiz/AddQuizScore", { questionId: questionId, questionAnswerId: answerId, quizTypeId: a, score: perScore }, function () {
            console.log("Score is added");
        });
        $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
            $("#add").html(response)
        });
    }
    else
    {
        location.href = `/Quiz/Finished?score=${score}&quizTypeId=${a}`;
    }
}


//function check(b, c) {
//    if (prev < count) {
//        $.post("/Quiz/CheckAnswer?questionAnswerId=" + b, function (response1) {
//            explanation = $("#explan").data("text");
//            console.log("Text: " + explanation);
//            if (response1) {
//                Swal.fire({
//                    title: "Good job!",
//                    text: "Correct answer",
//                    icon: "success"
//                }).then(function () {
//                    score = score + 100;
//                    $.post("/Quiz/AddQuizScore", { questionId: c, questionAnswerId: b, quizTypeId: a, score: 100 }, function () {
//                        console.log("Score is added");
//                    });
//                    $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
//                        $("#add").html(response)
//                    });
//                    if (prev >= count) {
//                        location.href = "/Quiz/Index";
//                    }
//                });
//            }
//            if (!response1) {
//                Swal.fire({
//                    title: "Oops!",
//                    text: explanation,
//                    icon: "error"
//                }).then(function () {
//                    score = score - 100;
//                    $.post("/Quiz/AddQuizScore", { questionId: c, questionAnswerId: b, quizTypeId: a, score: -10 }, function () {
//                        console.log("Score is added");
//                    });
//                    $.get("/Quiz/Question?prev=" + prev + "&quizType=" + a, function (response) {
//                        $("#add").html(response)
//                    });
//                    if (prev >= count) {
//                        location.href = `/Quiz/Finished?score=${score}`;
//                    }
//                });
//            }
//        })
//    }
//    prev += 1;
//}

const btn = document.getElementById("load");
const quizContainer = document.getElementById("quizContainer");
var clickCount = 0;
btn.addEventListener("click", () => {
    clickCount++;
    if (clickCount == 2) {
        btn.style.display = "none";
        quizContainer.style.display = "none";
    }
});