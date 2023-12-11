using Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataBase
{   
    public class ContatoRepository
    {
        //globalizar a abertura da conexão atraves do (método - ConexaoAberta)
        private void ConexaoAberta(Action<ContatoContext> acao)
        {
            using (var context = new ContatoContext())
            {
                context.Database.Connection.Open();
                acao(context);
            }
        }
        public IEnumerable<Contato> ObterQuantidadeDeContatos()
        {
            using (var context = new ContatoContext())
            {
                context.Database.Connection.Open();
                var resultado = context.Contatos.ToList();
                return resultado;
            }
        }

        //neste exemplo estou consumindo a (conexão global - método ConexaoAberta) 
        public bool SalvarContato(Contato contato)
        {    
                try
                {
                   ConexaoAberta((context) =>
                   { 
                    context.Contatos.Add(contato);
                    context.SaveChanges();
                    });
                    return true; 
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        //woto-> neste momento estou consumindo (conexão local)
        public bool AtualizarContato(Contato contatoAtualizado)
        {
            using (var context = new ContatoContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    var contatoExistente = context.Contatos.Find(contatoAtualizado.Id);

                    if (contatoExistente == null)
                    {
                        //woto - posso personalizar uma logica
                        return false;
                    }

                    //woto -> atualiza as propriedades e monta objeto
                    contatoExistente.Nome = contatoAtualizado.Nome;
                    contatoExistente.Telefone = contatoAtualizado.Telefone;
                    contatoExistente.Data = DateTime.Now;

                    context.SaveChanges();

                    return true; 
                }
                catch (Exception ex)
                {
                    //woto-> pensamento log
                    return false;
                }
            }
        }
        public Contato ObterContatoPorId(int recebendoId)
        {
            using (var context = new ContatoContext())
            {
                try
                {
                    context.Database.Connection.Open();
                    return context.Contatos.Find(recebendoId);
                }

                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public bool DeletarContato(int? contatoId)
        {
                try
                {
                ConexaoAberta((context) =>
                {
                    var contatoExistente = context.Contatos.Find(contatoId);

                    if (contatoExistente == null)
                    {
                        return ;
                    }
                    context.Contatos.Remove(contatoExistente);
                    context.SaveChanges();
                });
                return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }


    

    


