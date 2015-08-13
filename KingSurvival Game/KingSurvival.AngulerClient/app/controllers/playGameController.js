'use strict';
app.controller('playGameController', ['$scope', 'playGameService', function ($scope, playGameService) {
    $scope.currentActiveGames = [];


    $scope.activeGames = function () {
        playGameService.activeGames().then(function (response) {
                console.log(response.data);
                $scope.currentActiveGames = response.data;
            },
            function (response) {

            });
    };

    $scope.joinGame = function (event) {
        var gameId = $(event.target).attr('data-game-id');
        playGameService.joinGame(gameId).then(function (response) {
                console.log(response)
            },
            function (response) {
                console.log(response)
            })
    };

    $scope.createGame = function () {
        playGameService.createGame().then(function (response) {
                console.log(response)
            },
            function (response) {
                console.log(response)
            })
    }
    $scope.activeGames();


}]);
