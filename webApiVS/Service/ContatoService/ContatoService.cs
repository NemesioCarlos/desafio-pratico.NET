using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using webApiVS.DataContest;
using webApiVS.Models;

namespace webApiVS.Service.ContatoService
{
    public class ContatoService : IContatoInterface
    {   //apenas leitura
        private readonly AplicationDbContext _context;
        //injeção de dependencia 
        public ContatoService(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<ContatoModel>>> CreateContato(ContatoModel novoContato)
        {
            ServiceResponse<List<ContatoModel>> serviceResponse = new ServiceResponse<List<ContatoModel>>();

            try
            {
                if (novoContato == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }
                //adiciouna um contato novo 
                _context.Add(novoContato);
                //salvou no banco 
                await _context.SaveChangesAsync();
                //consulta geral no banco 
                serviceResponse.Dados = _context.Contatos.ToList();


            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            //o qure retorna da nova cosulta
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ContatoModel>>> DeleteContato(int id)
        {
            ServiceResponse<List<ContatoModel>> serviceResponse = new ServiceResponse<List<ContatoModel>>();


            try
            {
                ContatoModel contato = _context.Contatos.FirstOrDefault(x => x.Id == id);

                if (contato == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuario não encontrado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Contatos.Remove(contato);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Contatos.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ContatoModel>> GetContatoById(int id)
        {
            ServiceResponse<ContatoModel> serviceResponse = new ServiceResponse<ContatoModel>();

            try
            {   //o x é um contatoModel com todas suas propriedades aqui pega o ID, esta sendo armazenado na variavel connato
                ContatoModel contato = _context.Contatos.FirstOrDefault(x => x.Id == id);

                if (contato == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Contato não encontrado";
                    serviceResponse.Sucesso = false;
                }
                serviceResponse.Dados = contato;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ContatoModel>>> GetContatos()
        {
            ServiceResponse<List<ContatoModel>> serviceResponse = new ServiceResponse<List<ContatoModel>>();
            //colocando dentro do service dados todos os dados da tabela contatos
            try
            {
                serviceResponse.Dados = _context.Contatos.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum contato encontrado";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }
        //dentro do editatoContato tem todos os seus dados
        public async Task<ServiceResponse<List<ContatoModel>>> UpdateContato(ContatoModel editadoContato)
        {               //lista de contatos model
            ServiceResponse<List<ContatoModel>> serviceResponse = new ServiceResponse<List<ContatoModel>>();

            try
            {
                ContatoModel contato = _context.Contatos.AsNoTracking().FirstOrDefault(x => x.Id == editadoContato.Id);

                if (contato == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Contato não encontrado!";
                    serviceResponse.Sucesso = false;
                }


                _context.Contatos.Update(editadoContato);

                //para salvar a alteração feita
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Contatos.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }
        
        


    }

}
