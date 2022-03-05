var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();

function Init() {
    var NewMessageForm = $("#NewMessageForm");
    NewMessageForm.on("submit", function (e) {

        e.preventDefault();
        var message = e.target[0].value;
        e.target[0].value = '';
        sendMessage(message);
    });

}

function sendMessage(text) {
    connection.invoke('SendNewMessage', " بازدید کننده ", text);
}

$(document).ready(function () {
    Init();
});
