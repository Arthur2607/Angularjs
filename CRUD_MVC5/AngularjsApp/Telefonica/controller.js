contatoApp.controller('contatoCtrl', function ($scope, contatoService) {

    carregarContatos();
    //Metodo para carregar os contatos na tabela
    function carregarContatos() {
        var listarContatos = contatoService.getTodosContatos();
        listarContatos.then(function (d) {
            //se tudo der certo
            $scope.Contatos = d.data;

        }, function () {
            alert("Ocorreu um erro ao tentar listar todos os contatos!");

        });
    }

    //Metodo para criar um novo contato
    $scope.criarContatos = function () {
        var contato = {
            contatoId: $scope.contatoId,
            nome: $scope.nome,
            telefone: $scope.telefone,
            operadora: $scope.operadora,
            genero: $scope.genero,
            fk_profissoes: $scope.drop

        };

        var adcionarInfos = contatoService.adcionarUmContato(contato)

        adcionarInfos.then(function (d) {
            if (d.data.sucess === true) {
                carregarContatos();
                alert("Contato adcionado com sucesso!");

                $scope.limparDados();

            } else { alert("Contato não adcionado!"); }


        },
            function () {
                alert("Erro ocorrido ao tentar adcionar um novo contato");
            });
    }

    //Limpar os campos após adcionar no DB
    $scope.limparDados = function () {

        $scope.contatoId = '',
            $scope.nome = '',
            $scope.telefone = '',
            $scope.operadora = '',
            $scope.genero = '',
            RdnNao.checked = false,
            RdnSim.checked = false,
            $scope.drop = '';




    }

    //Metodo para atualizar contato por ID
    $scope.atualizarContatoPorID = function (contato) {
        $scope.AtualizadoContatoID = contato.contatosId,
            $scope.AtualizadoNome = contato.nome,
            $scope.AtualizadoTelefone = contato.telefone,
            $scope.AtualizadoOperadora = contato.operadora,
            $scope.AtualizadoGenero = contato.genero,
        AtualizadoProfissoes(contato);
        AttRadio(contato);
        carregaProfissoes();
           
        

    }

    //Metodo para resgatar dados para exclusão do contato
    $scope.excluirContatoPorID = function (contato) {
        $scope.AtualizadoContatoID = contato.contatosId;
        $scope.AtualizadoNome = contato.nome;
    }

    //Metodo responsavel por atualizar dados do contato
    $scope.atualizarContatos = function () {
        var contato = {
            contatosId: $scope.AtualizadoContatoID,
            nome: $scope.AtualizadoNome,
            telefone: $scope.AtualizadoTelefone,
            operadora: $scope.AtualizadoOperadora,
            genero: $scope.AtualizadoGenero,
            fk_profissoes: $scope.AtualizadoProfi
        };

        var atualizarInfos = contatoService.atualizarContatos(contato);
        atualizarInfos.then(function (d) {

            if (d.data.sucess === true) {
                carregarContatos();
                alert("Contato Atualizado com sucesso!");
                $scope.limparDadosAtualizados();
            } else {
                alert("Contato não foi atualizado!");
            }


        }, function () {
            alert("Erro ocorrido ao tentar atualizar um novo contato");
        });
    }

    //Metodo responsavel por limpar os dados depois de atualizar:
    $scope.limparDadosAtualizados = function () {
        $scope.AtualizadoContatoID = '';
        $scope.AtualizadoNome = '';
        $scope.AtualizadoTelefone = '';
        $scope.AtualizadoOperadora = '';
        $scope.AtualizadoGenero = 'Selecione:';
        RdnNao.checked = false;
        RdnSim.checked = false;
        
        
    }

    //Metodo responsavel por deletar um contato:
    $scope.excluirContato = function (AtualizadoContatoID) {
        var excluirInfos = contatoService.excluirContato($scope.AtualizadoContatoID);
        excluirInfos.then(function (d) {
            if (d.data.sucess === true) {
                carregarContatos();

                alert("Contato excluido com sucesso!");

            } else {
                alert("Contato não excluido!");
            }
        }, function () {

            alert("ocorreu um erro ao tentar excluir o Contato!")
        });
    }

    //Carregar Profissões no select:
    function carregaProfissoes() {
        var listarProf = contatoService.getTodasProfissoes();
        listarProf.then(function (d) {
            //se tudo der certo
            $scope.Profissoes1 = d.data;
        }, function () {
            alert("Ocorreu um erro ao tentar listar todos as Profissões!");

        });
    }

    //Quando a modal adcionar se abre:
    $scope.onModalOpen = function () {
        carregaProfissoes();
        $scope.limparDados();
        $scope.prof = true;
        $scope.prof = 'nao';



    }

    //Limpa o Drop do banco quando nao selecionado
    $scope.limpaDrop = function () {
        $scope.drop = '';
        
    }

    // Funçao que carrega o valor de select para atualizar:
    function AtualizadoProfissoes(contato) {
        if (contato.fk_profissoes == null)
        {
            $scope.AtualizadoProfi = '';
        } else
        {
            $scope.AtualizadoProfi = contato.fk_profissoes.toString();
            
        }
    }

    // Validação do Atualizar Prof:
    function AttRadio(contato) {
        if (contato.fk_profissoes == null)
        {
            $scope.prof = "nao"
        } else
        {
            $scope.prof = "sim"
        }
    }

    //Metodo para criar uma nova Profissão:
    $scope.criarProf = function () {
        var profissoes = {
            profissaoId: $scope.profissaoId,
            profissaoNome: $scope.profissaoNome
        };
        var adcionaInfosProf = contatoService.adcionarUmaProfissao(profissoes)

        adcionaInfosProf.then(function (d) {
            if (d.data.sucess === true) {
                alert("Profissão adcionada com sucesso!");
                $scope.limparProf();
                carregaProfissoes();
            } else { alert("Profissão nao adcionada!"); }
        },
            function () {
                alert("Erro ocorrido ao tentar adcionar uma nova Profissão")
            });
    }

    // Metodo para limpar os campos das Profissoes
    $scope.limparProf = function () {
        $scope.profissaoId = '',
            $scope.profissaoNome = '';
    }
   

});

