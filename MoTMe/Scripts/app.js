var app = angular.module('MoTMe', []);

app.controller('Controller', ["$scope", "$http", function ($scope, $http) {
    $scope.test = "test variable";
    console.log($scope.test);


    $scope.hello = function () {
        $scope.test = "Hello World";

        //$http.get("/Home/Get")
        //    .success(function (data) {
        //        $scope.test = data;
        //    })
        //    .error(function (error) { alert(error.error) });

        $http.get("/Manage/GetUserIdLink")
            .success(function (data) {
                $scope.test = data;
            })
            .error(function (error) { alert(error.error) });
    }
}]);