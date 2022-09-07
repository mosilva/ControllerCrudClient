namespace ControllerCrudClient.Core.Interface
{
    public interface IClientService
    {
        List<Client> GetClients();

        bool CreateClient(Client cliente);

        bool UpdateClient(string nome, string novoNome);

        bool DeleteClient(long index);

        bool CheckExistsCpfClient(string cpf);

    }
}
