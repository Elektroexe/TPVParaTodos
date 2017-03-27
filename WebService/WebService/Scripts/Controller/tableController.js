angular.module('tpvpt').controller('tableController', ['$scope', tableController]);

function tableController($scope) {
    $scope.tables = [];

    var Connection = $.hubConnection();
    var hubProxy = Connection.createHubProxy('tableHub');

    console.log(Connection);
    console.log(hubProxy);

    hubProxy.on('refresh', function (tablesJson) {
        console.log(tablesJson);
    });

    //Connection.client.refresh = function (tablesJson) {
    //    $scope.tables = JSON.parse(tablesJson);
    //    console.log($scope.tables);
    //}

    Connection.start().done(function () {
        console.log("Conexion establecida");
        hubProxy.invoke('getAll');
        //Connection.server.getAll();
    });
}