/* Variáveis */

var enderecoTemp = "http://localhost:5035/Produtos/Produto/";
var enderecoGerarVenda = "http://localhost:5035/Vendas/GerarVenda";
var produto;
var compra = [];
var __totalVenda__ = 0.0;

/* Inicio */

$("#posvenda").hide();
atualizarTotal();

/* Funções */

function atualizarTotal() {
    $("#totalVenda").html(__totalVenda__);
}

function preencherFormulario(dadosProduto) {
    $("#campoNome").val(dadosProduto.nome);
    $("#campoCategoria").val(dadosProduto.categoria.nome);
    $("#campoFornecedor").val(dadosProduto.fornecedor.nome);
    $("#campoVenda").val(dadosProduto.precoDeVenda);
}

function zerarFormulario() {
    $("#campoNome").val("");
    $("#campoCategoria").val("");
    $("#campoFornecedor").val("");
    $("#campoVenda").val("");
    $("#campoQtd").val("");
}

function adicionarNaTabela(p, q) {
    var produtoTemp = {};

    Object.assign(produtoTemp, produto);

    var venda = { produto: produtoTemp, quantidade: q, subtotal: produtoTemp.precoDeVenda * q };

    __totalVenda__ += venda.subtotal;
    atualizarTotal();

    compra.push(venda);

    $("#compras")
        .append(`<tr>
                    <td>${p.id}</td>
                    <td>${p.nome}</td>
                    <td>${q}</td>
                    <td>R$ ${p.precoDeVenda}</td>
                    <td>${p.medicao}</td>
                    <td>R$ ${p.precoDeVenda * q}</td>
                    <td>
                        <button class="btn btn-danger">Remover</button>
                    </td>
                </tr>`);
}

/* Ajax */

$("#produtoForm").on("submit", function (event) {
    event.preventDefault();
    var produtoParaTabela = produto;
    var qtd = $("#campoQtd").val();

    // console.log(produtoParaTabela);
    // console.log(qtd);
    adicionarNaTabela(produtoParaTabela, qtd);
    //var produto = undefined;
    zerarFormulario();
});

$("#pesquisar").click(function () {
    var codProduto = $("#codProduto").val();
    var endereco = enderecoTemp + codProduto;
    $.post(endereco, function (dados, status) {
        produto = dados;

        var med = "";
        switch (produto.medicao) {
            case 0:
                med = "L";
                break;
            case 1:
                med = "Q";
                break;
            case 2:
                med = "U";
                break;
            default:
                med = "U";
                break;
        }

        produto.medicao = med;

        preencherFormulario(produto);
        // console.log(produto);
    }).fail(function () {
        alert("Produto sem estoque");
    });
});

/* Fim */

$("#finalizarVendaBtn").click(function () {
    if (__totalVenda__ <= 0) {
        alert("Compra inválida. Nenhum produto adicionado");
        return;
    }
    var valorPago = $("#valorPago").val();
    if (!(isNaN(valorPago))) {
        valorPago = parseFloat(valorPago);
        if (valorPago >= __totalVenda__) {

            $("#posvenda").show();
            $("#prevenda").hide();
            $("#valorPago").prop("disabled", true);
            var troco = valorPago - __totalVenda__;
            $("#troco").val(troco);

            //array
            compra.forEach(elemento => {
                elemento.produto = elemento.produto.id;
            });

            //preparar um novo objeto
            var _venda = { total: __totalVenda__, troco: troco, produtos: compra };

            //backend
            $.ajax({
                type: "POST",
                url: enderecoGerarVenda,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(_venda),
                success: function (data) {
                    console.log("dados enviados com sucesso");
                    console.log(data);
                }
            });
            zerarFormulario();
        } else {
            alert("Compra inválida. Valor pago menor que o total");
            return;
        }
    } else {
        alert("Valor pago inválido");
        return;
    }

});

function restaurarModal() {

    $("#posvenda").hide();
    $("#prevenda").show();
    $("#valorPago").prop("disabled", false);
    $("#troco").val("");
    $("#valorPago").val("");
}

$("#fecharModal").click(function () {
    restaurarModal();
});