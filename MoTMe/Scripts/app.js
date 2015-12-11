var app = angular.module('MoTMe', []);

app.controller('Controller', ["$scope", "$http", function ($scope, $http) {
    $scope.test = "test variable";
    console.log($scope.test);

    $scope.hello = function () {
        //$scope.test = "Hello World";

        $http.get("/api/Messages/1")
            .success(function (data) {
                console.log(data);
            })
            .error(function (error) { alert(error.error) });
    }
}]);