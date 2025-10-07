using Bogus;
using FluentAssertions;
using Moq;
using PB.Clientes.Application.CommandHandlers.Clientes;
using PB.Clientes.Domain.Clientes;
using PB.Clientes.Domain.Clientes.Commands;
using PB.Clientes.Domain.Clientes.Services;

namespace PB.Clientes.UnitTests.Application.Clientes
{
    public class CriarNovoClienteCommandHandlerTests
    {
        public CriarNovoClienteCommandHandlerTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _handler = new CriarNovoClienteCommandHandler(_clienteRepositoryMock.Object);
        }

        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly CriarNovoClienteCommandHandler _handler;
        private static readonly Faker Faker = new("pt_BR");

        [Fact(DisplayName = "Dado um comando válido, quando o email já estiver cadastrado, então deve retornar erro de validação")]
        public async Task Dado_um_comando_valido_quando_o_email_ja_estiver_cadastrado_entao_deve_retornar_erro_de_validacao()
        {
            // Arrange
            var command = new CriarNovoClienteCommand()
            {
                Email = Faker.Person.Email,
                Nome = Faker.Person.FullName,
                Score = Faker.Random.Int(0, 1000)
            };

            _clienteRepositoryMock
                .Setup(r => r.ValidaSeEmailEstaCadastradoAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull("O resultado da operação não deve ser nulo.");
            result.IsSuccess.Should().BeFalse("A operação não deve ser bem sucedida quando o email já está cadastrado.");
            result.IsFailure.Should().BeTrue("A operação deve ter falha quando o email já está cadastrado.");
            result.Errors.Should().NotBeNullOrEmpty("Deve conter erros na operação.");
            result.Errors.Should().ContainSingle(e => e.Key == "CLI-ER-001" && e.Message == "O email já está cadastrado no sistema.", "Deve conter o erro específico de email já cadastrado.");
        }

        [Fact(DisplayName = "Dado um comando válido, deve validar uma vez se o email já está cadastrado")]
        public async Task Dado_um_comando_valido_deve_validar_uma_vez_se_o_email_ja_esta_cadastrado()
        {
            // Arrange
            var command = new CriarNovoClienteCommand()
            {
                Email = Faker.Person.Email,
                Nome = Faker.Person.FullName,
                Score = Faker.Random.Int(0, 1000)
            };

            _clienteRepositoryMock
                .Setup(r => r.ValidaSeEmailEstaCadastradoAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            _ = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _clienteRepositoryMock.Verify(r => r.ValidaSeEmailEstaCadastradoAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once, "O método de validação de email deve ser chamado exatamente uma vez.");
        }

        [Fact(DisplayName = "Dado um comando válido, deve criar um novo cliente com sucesso")]
        public async Task Dado_um_comando_valido_deve_criar_um_novo_cliente_com_sucesso()
        {
            // Arrange
            var command = new CriarNovoClienteCommand()
            {
                Email = Faker.Person.Email,
                Nome = Faker.Person.FullName,
                Score = Faker.Random.Int(0, 1000)
            };

            _clienteRepositoryMock
                .Setup(r => r.ValidaSeEmailEstaCadastradoAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull("O resultado da operação não deve ser nulo.");
            result.IsSuccess.Should().BeTrue("A operação deve ser bem sucedida ao cadastrar um novo cliente.");
            result.IsFailure.Should().BeFalse("A operação não deve ter falha ao cadastrar um novo cliente.");
            result.Errors.Should().BeNullOrEmpty("Não deve conter erros na operação.");
        }

        [Fact(DisplayName = "Dado um comando válido, deve salvar o novo cliente no repositório")]
        public async Task Dado_um_comando_valido_deve_salvar_o_novo_cliente_no_repositorio()
        {
            // Arrange
            var command = new CriarNovoClienteCommand()
            {
                Email = Faker.Person.Email,
                Nome = Faker.Person.FullName,
                Score = Faker.Random.Int(0, 1000)
            };

            _clienteRepositoryMock
                .Setup(r => r.ValidaSeEmailEstaCadastradoAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            _ = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _clienteRepositoryMock.Verify(r => r.PersistirClienteAsync(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Once, "O método de adicionar cliente deve ser chamado exatamente uma vez.");
        }
    }
}
