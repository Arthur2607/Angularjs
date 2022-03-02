funcionarioApp.service('funcionarioService', function ($http) { 

//Serviço responsavel por adcionar um funcionario: CREATE
    this.adcionarUmFuncionario = function (funcionario) {
        var request = $http({
            method: 'post',
            url: '/Funcionario/adcionarFuncionario',
            data: funcionario

        });
        return request;
    }

   

});