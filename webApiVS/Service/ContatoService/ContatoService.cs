using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using webApiVS.DataContest;
using webApiVS.Models;

namespace webApiVS.Service.ContatoService
{
    public class ContatoService : IContatoInterface
    {
        private readonly AplicationDbContext _context;

        public ContatoService(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<ContatoModel>>> CreateContato(ContatoModel novoContato)
        {
            ServiceResponse<List<ContatoModel>> serviceResponse = new ServiceResponse<List<ContatoModel>>();

            try
            {
                System.Diagnostics.Trace.WriteLine("CreateContato - Início");

                if (novoContato == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                if (string.IsNullOrWhiteSpace(novoContato.Nome))
                {
                    serviceResponse.Mensagem = "O campo 'Nome' é obrigatório.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                Regex regexEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!regexEmail.IsMatch(novoContato.Email))
                {
                    serviceResponse.Mensagem = "O formato do e-mail é inválido.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }


                Regex regexTelefone = new Regex(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$");
                if (!regexTelefone.IsMatch(novoContato.Telefone))
                {
                    serviceResponse.Mensagem = "O formato do telefone é inválido.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                novoContato.Data = DateTime.Now.ToLocalTime();


                _context.Add(novoContato);

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
            {
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

        public async Task<ServiceResponse<List<ContatoModel>>> UpdateContato(ContatoModel editadoContato)
        {
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
