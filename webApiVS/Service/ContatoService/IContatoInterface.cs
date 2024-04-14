using webApiVS.Models;

namespace webApiVS.Service.ContatoService
{
    public interface IContatoInterface
    {
        Task<ServiceResponse<List<ContatoModel>>> GetContatos();
        Task<ServiceResponse<List<ContatoModel>>> CreateContato(ContatoModel novoContato);
        Task<ServiceResponse<ContatoModel>> GetContatoById(int id);
        Task<ServiceResponse<List<ContatoModel>>> UpdateContato(ContatoModel editadoContato);
        Task<ServiceResponse<List<ContatoModel>>> DeleteContato(int id);
    }
}
 