(function () {
    'use strict';

    angular.module('kingSurvival', ['nywton.chessboard'])
        .service('kingSurvivalGameService', ['$log', function kingSurvivalGameService($log) {


            this.onDragStart = function (gameState, source, piece, position, orientation) {
              //  $log.debug('lift piece ' + piece + ' from ' + source + ' - ' + position + ' - ' + orientation);

                console.log(gameState);
                if (gameState.PlayerFigure !== gameState.gameState ||
                    (gameState.PlayerFigure === 1 && piece !== 'wK') ||
                    (gameState.PlayerFigure === 2 && piece !== 'bP')) {
                    return false;
                }
                return true;
            };

            this.onSnapEnd = function (callback, gameId, playerId, board, source, target, piece) {
                callback(gameId, playerId, source, target);
             //   $log.debug('onSnapEnd ' + piece + ' from ' + source + ' to ' + target);
            };


        }])

        .directive('kingSurvivalGame', ['$window', '$log', 'kingSurvivalGameService', function ($window, $log, kingSurvivalGameService) {

            var directive = {
                restrict: 'E',
                template: '<div>' +
                '<nywton-chessboard  data-board="board" data-draggable="true"  data-position="{ a8: \'bP\',  c8: \'bP\',  e8: \'bP\',  g8: \'bP\',  d1: \'wK\'  }" on-change="onChange" on-drag-start-cb="onDragStart" on-snap-end="onSnapEnd" on-drop="onDrop"></nywton-chessboard>' +
                '</div>',
                replace: false,
                scope: true,
                controller: ['$scope', '$rootScope', function chessgame($scope, $rootScope) {

                    var gameState = $scope.$parent.gameState;

                    $.connection.hub.stop();

                    var kingSurvival = $.connection.kingSurvivalGame;

                    function move(fen) {
                        $scope.board.position(fen)
                    }

                    function updateState(updatedGameState) {
                        console.log(updatedGameState);
                        gameState.gameState = updatedGameState.gameState
                    }

                    kingSurvival.client.move = move;
                    kingSurvival.client.updateGameState = updateState;

                    $.connection.hub.start().done(function () {
                        kingSurvival.server.joinRoom(gameState.Id);
                    }).fail(function (error) {
                        console.log('Invocation of start failed. Error: ' + error)
                    });

                    this.board = function boardF() {
                        return $scope.board;
                    };

                    $scope.onDragStart = function onDragStartF(source, piece, position, orientation) {
                        return kingSurvivalGameService.onDragStart(gameState, source, piece, position, orientation);
                    };
                    $scope.onSnapEnd = function onSnapEndF(source, target, piece) {
                        return kingSurvivalGameService.onSnapEnd(kingSurvival.server.gameEngine, gameState.Id, gameState.playerId, $scope.board, source, target, piece);
                    };
                }],
            };

            return directive;
        }])
})();
