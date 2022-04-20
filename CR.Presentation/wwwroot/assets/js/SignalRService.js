"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();

connection.on("ReceiveMessageHandler", function (message, messageFlag, messageHour) {

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");

    var body = $("#messages-Body-Expert").html();
    var body2 = $("#messages-Body-Consumer").html();

    if (messageFlag === 0) {
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
        $("#messages-Body-Expert").html("");
        $("#messages-Body-Expert").html(body);

    }
    else {

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
        $("#messages-Body-Consumer").html("");
        $("#messages-Body-Consumer").html(body2);
    }
});
