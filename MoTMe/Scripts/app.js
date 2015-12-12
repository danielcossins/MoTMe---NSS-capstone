var app = angular.module('MoTMe', []);

app.controller('Controller', ["$scope", "$http", function ($scope, $http) {
    $scope.test = "test variable";
    console.log($scope.test);

    
    //$http.get({
    //    url: "/api/Ajax",
    //    //method: "GET",
    //    //dataType: 'json',
    //    //params: { id: 1 }
    //}).success(function (data) {
    //    console.log(data);
    //}).error(function (error) {
    //    console.log(error);
    //});

    $scope.hello = function () {
        $scope.test = "Hello World";

        $http.get("/Home/Get")
            .success(function (data) {
                $scope.test = data;
            })
            .error(function (error) { alert(error.error) });

        $http.get("/Manage/GetUserId")
            .success(function (data) {
                $scope.test = data;
            })
            .error(function (error) { alert(error.error) });
    }
}]);