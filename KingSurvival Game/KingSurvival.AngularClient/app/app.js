var app = angular.module('AngularAuthApp',
    ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'nywton.chessboard', 'kingSurvival']);

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

  $routeProvider.when("/gameMenu", {
    controller: "gameMenuController",
    templateUrl: "/app/views/gameMenu.html"
  });
  $routeProvider.when("/game", {
    controller: "gameController",
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