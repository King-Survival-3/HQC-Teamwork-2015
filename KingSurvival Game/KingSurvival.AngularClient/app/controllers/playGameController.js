'use strict';
app.controller('gameController',  ['$scope', '$window', function ($scope, $window) {

    var rooms = [];
    $.connection.hub.url = "http://localhost:60455/signalr";

   var  chat = $.connection.chat;
    $('#send-message').click(function () {

        var msg = $('#message').val();
        chat.server.sendMessage(msg);
    });

    $("#join-room").click(function () {

       var room = $('#room').val();

        chat.server.joinRoom(room)
    });

    $('#send-message-to-room').click(function () {

        var msg = $('#room-message').val();

        chat.server.sendMessageToRoom(msg, rooms);
    });

   chat.client.addMessage = addMessage;
    chat.client.joinRoom = joinRoom;
    $scope.addMessage = addMessage;

    function addMessage(message) {
        $('#messages').append('<div>' + message + '</div>');
    }

    function joinRoom(room) {
        rooms.push(room);
        $('#currentRooms').append('<div>' + room + '</div>');
    }

    $.connection.hub.start().done(function () {
    })  .fail(function (error) {
        console.log('Invocation of start failed. Error: ' + error)
    });

}]);
