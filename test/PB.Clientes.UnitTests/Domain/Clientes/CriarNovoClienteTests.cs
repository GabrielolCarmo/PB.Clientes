using Bogus;
using FluentAssertions;
using PB.Clientes.Domain.Clientes;
using PB.Clientes.Domain.Clientes.Commands;
using PB.Clientes.Domain.Clientes.Events;

namespace PB.Clientes.UnityTests.Domain.Clientes
{
    public class CriarNovoClienteTests
    {
        private static readonly Faker Faker = new("pt_BR");

        [Fact(DisplayName = "Ao criar um novo cliente deve publicar um evento de NOVO_CLIENTE_CRIADO")]
        public void Ao_criar_um_novo_cliente_deve_publicar_um_evento_de_NOVO_CLIENTE_CRIADO()
        {
            // Arrange
            var command = new CriarNovoClienteCommand()
            {
                Email = Faker.Person.Email,
                Nome = Faker.Person.FullName,
                Score = Faker.Random.Int(0, 1000)
            };

            // Act
            var cliente = Cliente.Factory.CriarNovoCliente(command);

            // Assert

            cliente.Events.Should().NotBeNullOrEmpty("A lista de eventos está vazia ou nula");
            cliente.Events[0].Should().BeOfType<NovoClienteCriadoEvent>("Não foi encontrado o evento NovoClienteCriadoEvent na lista de eventos.");
        }

        [Fact(DisplayName = "Dado um comando válido, quando criar novo cliente")]
        public void Dado_um_comando_valido_quando_criar_novo_cliente()
        {
            // Arrange
            var command = new CriarNovoClienteCommand()
            {
                Email = Faker.Person.Email,
                Nome = Faker.Person.FullName,
                Score = Faker.Random.Int(0, 1000)
            };

            // Act
            var cliente = Cliente.Factory.CriarNovoCliente(command);

            // Assert
            cliente.Should().NotBeNull("O cliente não foi criado.");
            cliente.Id.Should().NotBeEmpty("O Id do cliente não foi gerado.");
            cliente.Nome.Should().Be(command.Nome, "O nome do cliente não corresponde ao comando.");
            cliente.Email.Should().Be(command.Email, "O email do cliente não corresponde ao comando.");
            cliente.Score.Should().Be(command.Score, "O score do cliente não corresponde ao comando.");
        }
    }
}
