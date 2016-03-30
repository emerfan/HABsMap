var SampleApp = angular.module('SampleApp', [])
SampleApp.controller('SampleController', function ($scope, $http)
    {
        var url = "/Sample/GetSamples";
            $http.get(url).success(function (response)
                {
                    $scope.samples = response;
                });

                $scope.orderByField = 'Location';
                $scope.reverseSort = false;
});

SampleApp.filter("mydate", function () {
    var re = /\\\/Date\(([0-9]*)\)\\\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(x.substr(6)));
        else return null;
    };
});
