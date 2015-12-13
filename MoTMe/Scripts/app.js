var app = angular.module('MoTMe', []);

app.controller('RootCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    console.log($rootScope);
    $http.get("/Manage/GetUserObjectJSON")
            .success(function (data) {
                $rootScope.User = data;
                console.log($rootScope);
            })
            .error(function (error) { alert(error.error) });
}])

app.controller('IndexCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    console.log("Index reached");

    $scope.AddMessage = function (message) {
        var User = {
            
        };
    };
}])

app.controller('AboutCtrl', ["$scope", "$http", function ($scope, $http) {
    $scope.test = "click me";
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
                $scope.UserIdLink = data;
            })
            .error(function (error) { alert(error.error) });

        $http.get("/Manage/GetUserId")
            .success(function (data) {
                $scope.UserId = data;
            })
            .error(function (error) { alert(error.error) });

        //$http.get("/User/GetUserId_Int")
        //    .success(function (data) {
        //        $scope.UserId = data;
        //    })
        //    .error(function (error) { alert(error.error) });
    }
}]);