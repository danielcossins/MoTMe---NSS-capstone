var app = angular.module('MoTMe', []).run([
   "$rootScope", "$http",
  function ($rootScope, $http) {
      $http.get("/Manage/GetUserObjectJSON")
            .success(function (data) {
                $rootScope.User = data;
                console.log($rootScope);
            })
            .error(function (error) { alert(error.error) });
  }
]);

app.controller('RootCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    console.log($rootScope);
    //$http.get("/Manage/GetUserObjectJSON")
    //        .success(function (data) {
    //            $rootScope.User = data;
    //            console.log($rootScope);
    //        })
    //        .error(function (error) { alert(error.error) });
}])

app.controller('IndexCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    console.log("Index reached");


    $scope.RefreshMessages = function () {
        console.log($rootScope);
        console.log($rootScope.User);
        console.log($rootScope.User.Id);
        $http({
            url: "/Manage/GetMessagesByUserId/",
            method: "GET",
            params: {
                uid: $rootScope.User.Id
            }
        })
            .success(function (data) {
                console.log(data);
                $scope.Messages = data;
            })
            .error(function (error) { alert(error.error) });
    };
    //$scope.RefreshMessages();

    $scope.AddMessage = function (message) {
        console.log(message);
        if (message != "" && message != null && message != undefined) {
            $http({
                url: "/Manage/AddMessage",
                method: "POST",
                data: {
                    body: message,
                    authorId: $rootScope.User.Id,
                    recieverId: 1
                }
            }).success(function () {
                $scope.RefreshMessages();
            });
        }
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