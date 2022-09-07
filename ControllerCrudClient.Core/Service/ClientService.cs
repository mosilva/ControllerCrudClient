using ControllerCrudClient.Core.Interface;

namespace ControllerCrudClient.Core.Service
{
    public class ClientService : IClientService
    {

        public IClientRepository _clienteRespository;
        public ClientService(IClientRepository clienteRespository)
        {
            _clienteRespository = clienteRespository;
        }

        public List<Client> GetClients()
        {
            return _clienteRespository.GetClients();
        }

        public bool CreateClient(Client client)
        {
            return _clienteRespository.CreateClient(client);
        }

        public bool UpdateClient(string nome, string novoNome)
        {
            return _clienteRespository.UpdateClient(nome, novoNome);
        }

        public bool DeleteClient(long index)
        {
            return _clienteRespository.DeleteClient(index);
        }
        public bool CheckExistsCpfClient(string cpf)
        {
            if(_clienteRespository.GetClientByCpf(cpf)== null)
            {
                return false;
            }

            return true;
        }

        public bool CheckExistsNomeClient(string nome)
        {
            if (_clienteRespository.GetClientByNome(nome) == null)
            {
                return false;
            }

            return true;


        }

    }
}