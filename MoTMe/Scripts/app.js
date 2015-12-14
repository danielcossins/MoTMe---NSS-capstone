var app = angular.module('MoTMe', []).run([
   "$rootScope", "$http",
  function ($rootScope, $http) {
      //$http.get("/Manage/GetUserObjectJSON")
      //      .success(function (data) {
      //          console.log(data);
      //          $rootScope.user = data;
      //          console.log($rootScope);
      //          console.log($rootScope.user);
      //      })
      //      .error(function (error) { alert(error.error) });
  }
]);

app.controller('RootCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    $http.get("/Manage/GetUserObjectJSON")
            .success(function (data) {
                console.log(data);
                $scope.user = data;
                console.log($scope.user);
            })
            .error(function (error) { alert(error.error) });
}])

app.controller('IndexCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    console.log($scope.user);
    $scope.RefreshMessages = function () {
        console.log($scope.user);
        $http({
            url: "/Manage/GetMessagesByUserId/",
            method: "GET",
            params: {
                uid: $scope.user.Id
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
        console.log($scope.user);
        console.log(message);
        if (message != "" && message != null && message != undefined) {
            $http({
                url: "/Manage/AddMessage",
                method: "POST",
                data: {
                    body: message,
                    authorId: $scope.user.Id,
                    recieverId: 1
                }
            }).success(function () {
                $scope.RefreshMessages();
            });
        }
        $scope.RefreshMessages();
    };
}])

app.controller('AboutCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    $scope.test = "click me";
    console.log($scope.test);


    $scope.hello = function () {
        console.log($scope.user);

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