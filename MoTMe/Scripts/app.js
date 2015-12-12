var app = angular.module('MoTMe', []);

app.controller('Controller', ["$scope", "$http", function ($scope, $http) {
    $scope.test = "test variable";
    console.log($scope.test);

    
    $http({
        url: "/MoTMeRepository/GetMessageById",
        method: "GET",
        params: { id: 1 }
    }).success(function (data) {
        console.log(data);
    }).error(function (error) {
        console.log(error);
    });
}]);