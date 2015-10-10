'use strict';
app.controller('gameMenuController', ['$rootScope', '$scope', '$location', 'gameMenuService', function ($rootScope, $scope, $location, gameMenuService) {
    $scope.currentActiveGames = [];

    $rootScope.gameState = {};

    $scope.activeGames = function () {
        gameMenuService.activeGames().then(function (response) {
                var data = response.data
                $scope.currentActiveGames = data.map(function (game) {
                    return {
                        id: game.Id,
                        firstPlayerUserName: game.FirstPlayerUserName,
                        creationDate: new Date(game.CreationDate).toUTCString()
                    }
                });
            },
            function (response) {
            });
    };

    $scope.joinGame = function (event) {
        $scope.gameId = $(event.target).attr('data-game-id');
        gameMenuService.joinGame($scope.gameId).then(function (response) {
                $rootScope.gameState = response.data;
                $location.path('/game');
            },
            function (response) {
                console.log(response)
            })
    };

    $scope.createGame = function () {
        gameMenuService.createGame().then(function (response) {
                $rootScope.gameState = response.data;
                console.log($scope);
                $location.path('/game');
            },
            function (response) {
                console.log(response)
            })
    };

    $scope.activeGames();
}]);
