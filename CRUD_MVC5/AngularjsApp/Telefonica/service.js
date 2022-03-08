contatoApp.service('contatoService', function ($http) {

    //Metodo responsavel por listar todos os contatos: READ
    this.getTodosContatos = function () {
        return $http.get("/contato/GetContatos");
    }

    //Serviço responsavel por adcionar um contato: CREATE
    this.adcionarUmContato = function (contato) {
        var request = $http({
            method: 'post',
            url: '/Contato/adcionarContato',
            data: contato
        });
        return request;

    }

    //Serviço responsavel por atualizar um contato: UPDATE
    this.atualizarContatos = function (contato) {
        var request = $http({
            method: 'post',
            url: '/Contato/atualizarContatos',
            data: contato

        });
        return request;
    }

    //Serviço responsavel por Deletar um contato: DELETE
    this.excluirContato = function (AtualizadoContatoID) {
        return $http.post('/Contato/excluirContato/' + AtualizadoContatoID);
    }

    //Serviço responsavel por gerir a pagina login
   


});