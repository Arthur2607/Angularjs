funcionarioApp.controller('funcionarioCtrl', function ($scope, funcionarioService) {
    //Metodo para criar um novo Funcionario
    $scope.criarFuncionario = function () {
        var funcionario = {
            funcionarioId: $scope.funcionarioId,
            funcionarioNome: $scope.funcionarioNome,
            funcionarioCPF: $scope.funcionarioCPF,
            funcionarioLogin: $scope.funcionarioLogin,
            funcionarioSenha: $scope.funcionarioSenha
        };

        var adcionarInfosFun = funcionarioService.adcionarUmFuncionario(funcionario)

        adcionarInfosFun.then(function (d) {
            if (d.data.sucess === true) {
                
                alert("Funcionario adcionado com sucesso!");

                $scope.limparDadosFun();

            } else { alert("Funcionario não adcionado!"); }


        },
            function () {
                alert("Erro ocorrido ao tentar adcionar um novo Funcionario");
            });
    }

    //Limpar os campos após adcionar no DB
    $scope.limparDadosFun = function () {

        $scope.funcionarioId = '',
        $scope.funcionarioNome = '',
        $scope.funcionarioCPF = '',
        $scope.funcionarioLogin = '',
        $scope.funcionarioSenha = '';

    }

    //Metodo para mandar acessar a pagina de contato telefonico
   
});