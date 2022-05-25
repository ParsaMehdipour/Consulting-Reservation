"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();

connection.on("ReceiveMessageHandler", function (message, messageFlag, messageHour, filePath, notReadCount, chatUserId) {

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var body = $("#messages-Body-Expert").html();
    var body2 = $("#messages-Body-Consumer").html();

    if (messageFlag === 0) {

        if (filePath == null) {
            body += '<li class="media sent">';
            body += '<div class="media-body">';
            body += '<div class="msg-box">';
            body += '<div>';
            body += '<p>';
            body += msg;
            body += '</p>';
            body += '<ul class="chat-msg-info">';
            body += '<li>';
            body += '<div class="chat-time">';
            body += '<span>';
            body += messageHour;
            body += '</span>';
            body += '</div>';
            body += '</li>';
            body += '</ul>';
            body += '</div>';
            body += '</div>';
            body += '</div>';
            body += '</li>';
        }
        else {
            body += '<li class="media sent">';
            body += '<div class="media-body">';
            body += '<div class="msg-box">';
            body += '<div>';
            body += '<div class="chat-msg-attachments">';
            body += '<div class="chat-attachment">';
            body += '<img src="/' + filePath + '" alt="Attachment">';
            body += '<div class="chat-attach-caption"></div>';
            body += '<a href="/' + filePath + '" class="chat-attach-download" download>';
            body += '<i class="fas fa-download"></i>';
            body += '</a>';
            body += '</div>';
            body += '</div>';
            body += '<ul class="chat-msg-info">';
            body += '<li>';
            body += '<div class="chat-time">';
            body += '<span>' + messageHour + '</span>';
            body += '</div>';
            body += '</li>';
            body += '</ul>';
            body += '</div>';
            body += '</div>';
            body += '</div>';
            body += '</li>';
        }

        if (notReadCount > 0) {
            $("#NotReadExpert_" + chatUserId).text(notReadCount);
        }
        $("#messages-Body-Expert").html("");
        $("#messages-Body-Expert").html(body);
        $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);

    }
    else {

        if (filePath == null) {
            body2 += '<li class="media sent">';
            body2 += '<div class="media-body">';
            body2 += '<div class="msg-box">';
            body2 += '<div>';
            body2 += '<p>';
            body2 += msg;
            body2 += '</p>';
            body2 += '<ul class="chat-msg-info">';
            body2 += '<li>';
            body2 += '<div class="chat-time">';
            body2 += '<span>';
            body2 += messageHour;
            body2 += '</span>';
            body2 += '</div>';
            body2 += '</li>';
            body2 += '</ul>';
            body2 += '</div>';
            body2 += '</div>';
            body2 += '</div>';
            body2 += '</li>';
        }
        else {
            body2 += '<li class="media sent">';
            body2 += '<div class="media-body">';
            body2 += '<div class="msg-box">';
            body2 += '<div>';
            body2 += '<div class="chat-msg-attachments">';
            body2 += '<div class="chat-attachment">';
            body2 += '<img src="/' + filePath + '" alt="Attachment">';
            body2 += '<div class="chat-attach-caption"></div>';
            body2 += '<a href="/' + filePath + '" class="chat-attach-download" download>';
            body2 += '<i class="fas fa-download"></i>';
            body2 += '</a>';
            body2 += '</div>';
            body2 += '</div>';
            body2 += '<ul class="chat-msg-info">';
            body2 += '<li>';
            body2 += '<div class="chat-time">';
            body2 += '<span>' + messageHour + '</span>';
            body2 += '</div>';
            body2 += '</li>';
            body2 += '</ul>';
            body2 += '</div>';
            body2 += '</div>';
            body2 += '</div>';
            body2 += '</li>';
        }

        if (notReadCount > 0) {
            $("#NotReadConsumer_" + chatUserId).text(notReadCount);
        }
        $("#messages-Body-Consumer").html("");
        $("#messages-Body-Consumer").html(body2);
        $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);
    }
});

connection.on("ReceiveVoiceHandler", function (messageFlag, messageHour, filePath) {

    var body = $("#messages-Body-Expert").html();
    var body2 = $("#messages-Body-Consumer").html();

    if (messageFlag === 0) {
        body += '<li class="media sent">';
        body += '<div class="media-body">';
        body += '<div class="msg-box">';
        body += '<div>';
        body += '<div class="chat-msg-attachments">';
        body += '<audio controls>';
        body += '<source src="/' + filePath + '" type="audio/ogg">';
        body += '</audio>';
        body += '</div>';
        body += '<ul class="chat-msg-info">';
        body += '<li>';
        body += '<div class="chat-time">';
        body += '<span>' + messageHour + '</span>';
        body += '</div>';
        body += '</li>';
        body += '</ul>';
        body += '</div>';
        body += '</div>';
        body += '</div>';
        body += '</li>';

        if (notReadCount > 0) {
            $("#NotReadExpert_" + chatUserId).text(notReadCount);
        }
        $("#messages-Body-Expert").html("");
        $("#messages-Body-Expert").html(body);
        $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);

    }
    else {
        body2 += '<li class="media sent">';
        body2 += '<div class="media-body">';
        body2 += '<div class="msg-box">';
        body2 += '<div>';
        body2 += '<div class="chat-msg-attachments">';
        body2 += '<audio controls>';
        body2 += '<source src="/' + filePath + '" type="audio/ogg">';
        body2 += '</audio>';
        body2 += '</div>';
        body2 += '<ul class="chat-msg-info">';
        body2 += '<li>';
        body2 += '<div class="chat-time">';
        body2 += '<span>' + messageHour + '</span>';
        body2 += '</div>';
        body2 += '</li>';
        body2 += '</ul>';
        body2 += '</div>';
        body2 += '</div>';
        body2 += '</div>';
        body2 += '</li>';

        if (notReadCount > 0) {
            $("#NotReadConsumer_" + chatUserId).text(notReadCount);
        }
        $("#messages-Body-Consumer").html("");
        $("#messages-Body-Consumer").html(body2);
        $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);
    }
});

connection.on("UserLogOut", function (userFlag) {

    var body = $("#messages-Body-Expert").html();
    var body2 = $("#messages-Body-Consumer").html();

    if (userFlag === "کاربر") {
        body += '<li class="media sent">';
        body += '<div class="media-body">';
        body += '<div class="msg-box">';
        body += '<div>';
        body += '<p>';
        body += 'کاربر خارج شد';
        body += '</p>';
        body += '<ul class="chat-msg-info">';
        body += '<li>';
        body += '<div class="chat-time">';
        body += '</div>';
        body += '</li>';
        body += '</ul>';
        body += '</div>';
        body += '</div>';
        body += '</div>';
        body += '</li>';

        $("#messages-Body-Expert").html("");
        $("#messages-Body-Expert").html(body);
        $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);
    }
    else if (userFlag === "مشاور") {
        body2 += '<li class="media sent">';
        body2 += '<div class="media-body">';
        body2 += '<div class="msg-box">';
        body2 += '<div>';
        body2 += '<p>';
        body2 += 'مشاور خارج شد';
        body2 += '</p>';
        body2 += '<ul class="chat-msg-info">';
        body2 += '<li>';
        body2 += '<div class="chat-time">';
        body2 += '</div>';
        body2 += '</li>';
        body2 += '</ul>';
        body2 += '</div>';
        body2 += '</div>';
        body2 += '</div>';
        body2 += '</li>';

        $("#messages-Body-Consumer").html("");
        $("#messages-Body-Consumer").html(body2);
        $("#chatBody").scrollTop($("#chatBody")[0].scrollHeight);
    }
});