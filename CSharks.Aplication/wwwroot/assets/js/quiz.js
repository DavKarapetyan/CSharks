﻿//$(document).ready(() => {
//    $("#load").on("click", function () {
//        $.ajax({
//            url: "/Quiz/GetData",
//            success: function (data) {
//                console.log(data);
//                for (let i = 0; i < data.length; i++) {
//                    for (let j = 0; j < data[i].answers.length; j++) {
//                        document.getElementById("add").innerHTML += "<div class='col-lg-6 mt-3'> <button class='submit-btn w-100' id='btn' onclick='alert("
//                            + data[i].answers[j].isCorrect + ");'>" + data[i].answers[j].text + "</button></div>"
//                    }
//                }
//            },
//            error: function (er) {
//                console.log(er);
//            }
//        })
//    });
//});

$("#load").on("click", function () {
    $.get("/Quiz/Question?prev=10&quizType=2", function (response) {
        $("#add").html(response);
        $.post("/Quiz/CheckAnswer?questionAnswerId=1", function (response) {
            console.log(response);
        })
    })
});