var userMails = [];
var userID = [];

$(function () {
    $("#SendMailDialog").dialog({
        autoOpen: false,
        height: "auto",
        width: 340,
        modal: true,
        resizable: false,
        buttons: {
            "Send": function () {
                var subj = $("#subject").val();
                var msg = $("#message").val();
                if ((/^.*(?:[А-Яа-яA-Za-z0-9].*){5,500}$/).test(msg) === false) {
                    showValidError();
                    $('.ui-dialog :button').blur();
                } else {
                    alert("Your messages are processing. Wait for finish notification");
                    sendMail(userMails, subj, msg);
                    resetCheckBoxes();
                    $(".hiddenBtn").hide();
                    $(this).dialog("close");
                    scrollToPageTop();
                }
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            $('#message').val("");
            $('#subject').val("");
            $("#message").css("border", "");
            $("#valid-error").hide();
        },
        open: function () {
            $(this).parent().appendTo($("form:first"));
        }
    });

    $("#message").bind('input propertychange', function () {
        $("#valid-error").hide(400);
        $("#message").css("border", "");
    });

    /*check mail for count new messages*/
    setInterval(checkMail, 5000);
});



$("#btnSendMsg").live("click", function (e) {
    userMails = [];
    $("[id*=chkRow]:checked").each(function () {
        userMails.push($(this).closest("tr").find(":nth-child(9)").text().trim());
    });
    $("#SendMailDialog").dialog("open");
    return false;
});

$("[id*=chkSelectAll]").live("change", function () {
    if ($(this).is(":checked")) {
        $("[id*=chkRow]").each(function () {
            $(this).attr("checked", true);
        });
        $(".hiddenBtn").show();
        scrollToBtns();
    } else {
        $("[id*=chkRow]").each(function () {
            $(this).attr("checked", false);
        });
        $(".hiddenBtn").hide();
        scrollToPageTop();
    }
});

$("[id*=chkRow]").live("change", function () {
    if ($(this).is(":checked")) {
        if ($("[id*=chkRow]:checked").length >= 1) {
            $(".hiddenBtn").show();
            scrollToBtns();
        }
    } else {
        if ($("[id*=chkSelectAll]").is(":checked")) {
            $("[id*=chkSelectAll]").attr("checked", false);
        }
        if ($("[id*=chkRow]:checked").length == 0) {
            $(".hiddenBtn").hide();
            scrollToPageTop();
        }
    }
});

function sendMail(userMails, subj, msg) {
    var dataValue = JSON.stringify({ receivers: userMails, subject: subj, message: msg });
    $.ajax({
        type: "POST",
        url: "../Secure/AdminPage.aspx/SendMail",
        data: dataValue,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            alert(msg.d);
        }
    });
}

function showValidError() {
    $("#valid-error").hide();
    $("#message").css("border", "");
    $("#valid-error").show(700);
    $("#message").animate({ "border-color": "red" }, 700);
}

function resetCheckBoxes() {
    $("[id*=chkSelectAll]").attr("checked", false);
    $("[id*=chkRow]:checked").each(function () {
        $(this).attr("checked", false);
    });
}

function scrollToBtns() {
    $('html, body').animate({
        scrollTop: $(".hiddenBtn").offset().top
    }, 1300);
}

function scrollToPageTop() {
    $('html, body').animate({
        scrollTop: $("html").offset().top
    }, 1000);
}


/*check mail for new messages*/
function checkMail() {
    $.ajax({
        type: "POST",
        url: "../Secure/AdminPage.aspx/GetInputMessagInfo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#messagesInfo').text(msg.d);
        }
    });
}
