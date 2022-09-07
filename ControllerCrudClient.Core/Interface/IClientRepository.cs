namespace ControllerCrudClient.Core.Interface
{
    public interface IClientRepository
    {
        List<Client> GetClients();

        bool CreateClient(Client client);

        bool UpdateClient(string nome, string novoNome);

        bool DeleteClient(long index);

        Client GetClientByCpf(string cpf);

        Client GetClientByNome(string nome);

    }
}
