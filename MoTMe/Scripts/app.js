var app = angular.module('MoTMe', []).run([
   "$rootScope", "$http",
  function ($rootScope, $http) {
      //nothing to run first yet
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
    $scope.$watch('user', function (newValue, oldValue) {
        //run everything that requires user inside here
        $scope.RefreshMessages();
    });

    $http.get("/Manage/GetAllUsersJSON")
        .success(function (data) {
            console.log(data);
            $scope.users = data;
            console.log($scope.users);
        })
        .error(function (error) { alert(error.error) });

    //$scope.RefreshMessages = function () {
    //    $http({
    //        url: "/Manage/GetMessagesByUserId/",
    //        method: "GET",
    //        params: {
    //            uid: $scope.user.Id
    //        }
    //    })
    //    .success(function (data) {
    //        console.log(data);
    //        $scope.messages = data;
    //    })
    //    .error(function (error) { alert(error.error) });
    //};

    $scope.RefreshMessages = function () {
        console.log($scope.user);
        $http({
            url: "/Manage/GetMessagesForOneContact/",
            method: "GET",
            params: {
                aid: $scope.user.Id,
                rid: 1
            }
        })
        .success(function (data) {
            console.log(data);
            $scope.messages = data;
        })
        .error(function (error) { alert(error.error) });
    };

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
    //THIS IS STILL A TESTING GROUND
    $scope.test = "click me";
    console.log($scope.test);

    $scope.hello = function () {
        console.log($scope.user);

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
    }
}]);