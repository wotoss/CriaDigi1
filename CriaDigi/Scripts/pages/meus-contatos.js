//woto-> carrega todos os contatos ao entrarna pagina..
$(function () {
    CarregarTodosContato();
});

//woto-> manipula o id via renfild->hdnCodigoId
function ManipulandoIdModal() {
    if ($('#nome').val() == "" || $('#telefone').val() == "") 
        $('#hdnCodigoId').val(0);
}


function CarregarTodosContato() {
    return $.ajax({
        url: '/Home/BuscarTodosContatos/',
        xhrFields: {
            withCredentials: false
        },
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',  
        success: function (result) {
            $('#tblMeusContatoCorpo>tbody').empty();
            $(result).each(function (index, value) {                 
                MontarLinhaContato(value);               
            });
        },
        error: function (error) {
            CarregarErros(error.responseJSON.errors);
        }
    });
    
}


function MontarLinhaContato(objHistoricoFamiliar) {
    let dataDoBanco = objHistoricoFamiliar.Data;
    let timestamp = parseInt(dataDoBanco.match(/\d+/)[0]);
    let dataFormatada = new Date(timestamp);
    let opcoesFormato = {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
    }
    let dataFormatadaString = dataFormatada.toLocaleDateString('pt-BR', opcoesFormato);
    let inicioLinha = '<tr>';
    let fimLinha = '/<tr>';
    let linha = inicioLinha + 
        '</td><td>' + (objHistoricoFamiliar.Nome) +
        '</td><td>' + (objHistoricoFamiliar.Telefone) +
        '</td><td>' + (dataFormatadaString).replace(/,/g, '') + '</td>' +
        '</td><td>' + '<a id="sendo" class="btn btn-warning" codigo="' + objHistoricoFamiliar.Id + '" onclick="EditarEventoExterno(' + objHistoricoFamiliar.Id + ')" >Editar</button>' + '</td>' +
        '</td><td>' + '<button type="button" class="btn btn-danger" codigo="' + objHistoricoFamiliar.Id + '" onclick="ExcluirContato(' + objHistoricoFamiliar.Id + ')" >Deletar</button>' + '</td>'  + fimLinha;
    $('#tblMeusContatoCorpo>tbody').append(linha);
}

//woto-> validação verifica se o modal esta preenchido
//woto-> no back-end - fiz tambem a mesma validação
function SalvarContato() {
    if ($('#nome').val() == "" || $('#telefone').val() == "") {
        alert('Por favor preencha todos os campos');
        return;
    }
    $.ajax({
        url: '/Home/SalvarContato',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(objetoSalvar()),
        success: function (result) {
                alert('Contato cadastrado com sucesso');
            CarregarTodosContato();
            $('#fecharModal').trigger('click');
        },
        error: function () {
            //woto-> lógica
            console.log("Erro ao salvar os dados.");
        }
    });
}

function objetoSalvar ()  {
    var obj = {};

    obj.nome = $("#nome").val();
    obj.telefone = $("#telefone").val();

    return obj;
}


function EditarEventoExterno(recebendoId) {
    return $.ajax({
        url: '/Home/PopularFormContato/', 
        type: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        data: { recebendoId: recebendoId},
        success: function (result) {
                $('#hdnCodigoId').val(recebendoId);
                $('#nome').val(result.Nome);
                $('#telefone').val(result.Telefone);
                $('#botaoClick').click();
        },
        error: function (error) {
            CarregarErros(error.responseJSON.errors);
        }
    });
}
function LimparForm() {
    $('#nome').val('');
    $('#telefone').val('');
}

//woto-> verifica se atualiza os dados ou cadastro 
function SalvarDadosFamiliar() {
    if ($('#hdnCodigoId').val() == 0)
        SalvarContato();
    else
        AtualizarDadosContato();
}

function AtualizarDadosContato() {
    return $.ajax({
        url: '/Home/AtualizarContato/',
        xhrFields: {
            withCredentials: false
        },
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(MontarObjetoEditarContato()),
        success: function (result) {
            alert("Contato atualizados com sucesso.");
            CarregarTodosContato();          
            $('#fecharModal').trigger('click');
        },
        error: function (error) {
            //CarregarErros(error.responseJSON.errors);
        }
    }).done(function () {
        //esconderLoader();
    });
}

function MontarObjetoEditarContato() {
    var objEditarContato = {};

    objEditarContato.Id = $('#hdnCodigoId').val();
    objEditarContato.Nome = $('#nome').val();
    objEditarContato.Telefone = $('#telefone').val();
   
    return objEditarContato;
}


//DELETE
function ExcluirContato(contatoId) {
  
    let confirmacao = confirm('Tem certeza, deseja excluir o contato ?');

    if (!confirmacao) {
        return;
    }
    $.ajax({
        url: '/Home/DeletarContato/',
        type: 'POST',
        data: { contatoId: contatoId },
        success: function (data) {
            if (data.success) {
                alert(data.message); 
                CarregarTodosContato();
            } else {
                alert(data.message); 
            }
        },
        error: function () {
            alert('Erro ao realizar a requisição.');
        }
    });
}







