var app = angular.module('AngularAuthApp',
    ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'nywton.chessboard']);

app.config(function ($routeProvider) {

  $routeProvider.when("/home", {
    controller: "homeController",
    templateUrl: "/app/views/home.html"
  });

  $routeProvider.when("/login", {
    controller: "loginController",
    templateUrl: "/app/views/login.html"
  });

  $routeProvider.when("/signup", {
    controller: "signupController",
    templateUrl: "/app/views/signup.html"
  });

  $routeProvider.when("/playgame", {
    controller: "playGameController",
    templateUrl: "/app/views/playgame.html"
  });
  $routeProvider.when("/game", {
    //controller: "playGameController",
    templateUrl: "/app/views/game.html"
  });

  $routeProvider.otherwise({ redirectTo: "/home" });
});

app.config(function ($httpProvider) {
  $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
  authService.fillAuthData();
}]);