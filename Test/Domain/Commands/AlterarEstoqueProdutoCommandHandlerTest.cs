using Crosscutting.Enums;
using Crosscutting.Exceptions;
using Domain.Commands.Produto;
using Domain.Entities;
using Domain.Repositories;
 using FluentAssertions;
 using Moq;
 using Test.Domain.Builders;
 
 namespace Test.Domain.Commands;
 
 public class AlterarEstoqueProdutoCommandHandlerTest
 {
     private readonly Mock<IProdutoRepository> _repository = new();
     private readonly AlterarEstoqueCommandHandler _commandHandler;
 
     public AlterarEstoqueProdutoCommandHandlerTest()
     {
         _commandHandler = new AlterarEstoqueCommandHandler(_repository.Object);
     }

     [Fact]
     public async Task Handler_QuandoNaoExisteProduto_DeveLancarNaoEncontradoException()
     {
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = Guid.NewGuid(),
             TipoOperacao = TipoOperacao.Entrada,
             Quantidade = 10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync((Produto)null);
         
         Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);

         await act.Should().ThrowAsync<NaoEncontradoException>()
             .WithMessage("Produto não existe.");
     }
     
     [Fact]
     public async Task Handler_QuandoTipoOperacaoInvalido_DeveLancarArgumentException()
     {
         var produto = ProdutoBuilder.Novo().Build();
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = produto.Id,
             TipoOperacao = (TipoOperacao)999,
             Quantidade = 10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync(produto);
         
         Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);

         await act.Should().ThrowAsync<ArgumentException>()
             .WithMessage("Tipo de operação inválido.");
     }
     
     [Fact]
     public async Task Handler_QuandoEntradaValida_DeveAdicionarNoEstoque()
     {
         var produto = ProdutoBuilder.Novo().Build();
         var quantidadeOriginal = produto.QuantidadeEstoque.QuantidadeAtual;
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = produto.Id,
             TipoOperacao = TipoOperacao.Entrada,
             Quantidade = 10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync(produto);
         
         var result = await _commandHandler.Handle(command, CancellationToken.None);
 
         result.QuantidadeAtual.Should().Be(quantidadeOriginal+10);
     }

     [Fact]
     public async Task Handler_QuandoEntradaMenorOuIgualZero_DeveLancarValorInvalidoException()
     {
         var produto = ProdutoBuilder.Novo().Build();
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = produto.Id,
             TipoOperacao = TipoOperacao.Entrada,
             Quantidade = -10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync(produto);
         
         Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);

         await act.Should().ThrowAsync<ValorInvalidoException>()
             .WithMessage("Quantidade a adicionar não é um valor válido.");
     }
     
     [Fact]
     public async Task Handler_QuandoSaidaValida_DeveRemoverDoEstoque()
     {
         var produto = ProdutoBuilder.Novo().ComQuantidadeAtual(20).Build();
         var quantidadeOriginal = produto.QuantidadeEstoque.QuantidadeAtual;
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = produto.Id,
             TipoOperacao = TipoOperacao.Saida,
             Quantidade = 10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync(produto);
         
         var result = await _commandHandler.Handle(command, CancellationToken.None);
 
         result.QuantidadeAtual.Should().Be(quantidadeOriginal-10);
     }
     
     [Fact]
     public async Task Handler_QuandoSaidaMenorOuIgualZero_DeveLancarValorInvalidoException()
     {
         var produto = ProdutoBuilder.Novo().Build();
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = produto.Id,
             TipoOperacao = TipoOperacao.Saida,
             Quantidade = -10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync(produto);
         
         Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);

         await act.Should().ThrowAsync<ValorInvalidoException>()
             .WithMessage("Quantidade a retirar não é um valor válido.");
     }
     
     [Fact]
     public async Task Handler_QuandoSaidaMaiorQueQuantidadeAtual_DeveLancarQuantidadeInsuficienteException()
     {
         var produto = ProdutoBuilder.Novo().ComQuantidadeAtual(2).Build();
         var command = new AlterarEstoqueCommand
         {
             ProdutoId = produto.Id,
             TipoOperacao = TipoOperacao.Saida,
             Quantidade = 10
         };
         
         _repository.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync(produto);
         
         Func<Task> act = () => _commandHandler.Handle(command, CancellationToken.None);

         await act.Should().ThrowAsync<QuantidadeInsuficienteException>()
             .WithMessage("Quantidade insuficiente no estoque.");
     }
 }