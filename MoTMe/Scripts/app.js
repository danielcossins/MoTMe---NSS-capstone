var app = angular.module('MoTMe', []).run([
   "$rootScope", "$http",
  function ($rootScope, $http) {
      //nothing to run first yet
  }
]);

app.controller('RootCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    //$http.get("/Manage/GetUserObjectJSON")
    //    .success(function (data) {
    //        console.log(data);
    //        $scope.user = data;
    //        console.log($scope.user);
    //    })
    //    .error(function (error) { console.log(error.error) });
}])

app.controller('IndexCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    $scope.body = "";
    //Get contacts for the user
    //$http.get("/Manage/GetAllUsersJSON")
    //    .success(function (data) {
    //        console.log(data);
    //        $scope.contacts = data;
    //        console.log($scope.users);
    //    })
    //    .error(function (error) { console.log(error.error) });
    $http.get("/Manage/GetUserObjectJSON")
        .success(function (data) {
            console.log(data);
            $scope.user = data;
            console.log($scope.user);
    $http({
        url: "/Manage/GetContactUsersByUserId/",
        method: "GET",
        params: {
            uid: $scope.user.Id
        }
    })
        .success(function (data) {
            console.log(data);
            $scope.contacts = data;
            console.log($scope.contacts);
        })
        .error(function (error) { console.log(error.error) });
        })
        .error(function (error) { console.log(error.error) });
    

    //set which contact the user has clicked on
    $scope.setClickedContact = function (contact) {
        $scope.clickedContact = contact;
        console.log($scope.clickedContact);
        $scope.RefreshMessages();
        //window.scrollTo(0, document.body.scrollHeight);
    };
    
    //refresh the dom with new messages
    $scope.RefreshMessages = function () {
        console.log($scope.user);
        console.log($scope.clickedContact.Id);
        //Get a specific number of the latest messgaes between
        //the user and this certain contact
        $http({
            url: "/Manage/GetMessagesForOneContact_CertainNumber/",
            method: "GET",
            params: {
                aid: $scope.user.Id,
                rid: $scope.clickedContact.Id,
                number: 10
            }
        })
        .success(function (data) {
            console.log(data);
            ChangeDate(data);
            $scope.messages = data;
            console.log($scope.messages);
        })
        .error(function (error) { console.log(error.error) });
    };

    //add a message to the database
    $scope.AddMessage = function (message) {
        console.log($scope.clickedContact.Id);
        if (message != "" && message != null && message != undefined) {
            $http({
                url: "/Manage/AddMessage",
                method: "POST",
                data: {
                    body: message,
                    authorId: $scope.user.Id,
                    recieverId: $scope.clickedContact.Id
                }
            }).success(function () {
                //refresh database after message is added
                $scope.RefreshMessages();
                //set the input field to null again
                $scope.body = "";
            });
        }
        $scope.RefreshMessages();
    };

    //convert the json date to a javascript date
    function ChangeDate(messageArr) {
        for (var i = 0; i < messageArr.length; i++) {
            messageArr[i].Date = new Date(parseInt(messageArr[i].Date.replace('/Date(', ''))).toString();
        }
    }

    //This code will run every second
    //updates dom with the latest version of messages every second
    $(function () {
        setInterval(oneSecondFunction, 1000);
    });
    function oneSecondFunction() {
        //things to do every second
        $scope.RefreshMessages();
    }
    ////////////////////////////
}]);

app.controller('ContactCtrl', ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {
    $http.get("/Manage/GetAllUsersJSON")
        .success(function (data) {
            console.log("users", data);
            $scope.users = data;
        })
        .error(function (error) { alert(error.error) });

    $scope.AddToContacts = function (userId, otherUserId) {
        $http({
            url: "/Manage/AddContact",
            method: "POST",
            data: {
                userId: userId,
                contactId: otherUserId
            }
        }).success(function () {
            console.log("request sent");
        });
    };
}]);

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
            .error(function (error) { console.log(error.error) });

        $http.get("/Manage/GetUserId")
            .success(function (data) {
                $scope.UserId = data;
            })
            .error(function (error) { console.log(error.error) });
    }
}]);

app.directive('myEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });

                event.preventDefault();
            }
        });
    };
});