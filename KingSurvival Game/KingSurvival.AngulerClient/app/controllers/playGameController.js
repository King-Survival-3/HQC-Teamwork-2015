'use strict';
app.controller('playGameController', ['$scope', '$location', 'playGameService', function ($scope,$location, playGameService) {
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
                $location.path('/game');
            },
            function (response) {
                console.log(response)
            })
    };

    $scope.createGame = function () {
        playGameService.createGame().then(function (response) {
                $location.path('/game');
            },
            function (response) {
                console.log(response)
            })
    };

    $scope.activeGames();
}]);
