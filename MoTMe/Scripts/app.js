var app = angular.module('MoTMe', []);

app.controller('Controller', ["$scope", "$http", function ($scope, $http) {
    $scope.test = "test variable";
    console.log($scope.test);
}]);