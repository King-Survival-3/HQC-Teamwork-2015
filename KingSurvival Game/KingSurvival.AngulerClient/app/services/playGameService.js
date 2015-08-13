'use strict';
app.factory('playGameService', ['$http', '$q', function ($http, $q){
    var serviceBase = 'http://localhost:60455/';
    var playGameServiceFactory = {};

    var createGame = function(){
        return $http.post(serviceBase + 'api/games/create').then(function(results){
            return results;
        });
    };

    var joinGame = function(gameId){
        return $http.post(serviceBase + 'api/games/join?gameId=' + gameId ).then(function(result){
            return result;
        });
    };

    var activeGames = function(){
        return $http.get(serviceBase  + 'api/games/activeGames').then(function(result){
            return result;
        });
    };

    playGameServiceFactory.createGame = createGame;
    playGameServiceFactory.joinGame = joinGame;
    playGameServiceFactory.activeGames = activeGames;

    return playGameServiceFactory;
}]);
