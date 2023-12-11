using DataBase;
using Model;
using System.Collections.Generic;


namespace Business
{ 
    public class ContatoBusiness
    {
        private readonly ContatoRepository _contatoRepository;

        //woto-> injeção de dependencia
        public ContatoBusiness()
        {
            _contatoRepository = new ContatoRepository();
        }
        public IEnumerable<Model.Contato> ObterQuantidadeDeContatos()
        {
            return _contatoRepository.ObterQuantidadeDeContatos();
        }
        public bool SalvarContato(Contato contato)
        {
            return _contatoRepository.SalvarContato(contato);
        }
        public bool AtualizarContato(Contato contato)
        {
            return _contatoRepository.AtualizarContato(contato);
        }
        public bool DeletarContato(int? contatoId)
        {
            return _contatoRepository.DeletarContato(contatoId);
        }
        public Contato ObterContatoPorId(int recebendoId)
        {
            return _contatoRepository.ObterContatoPorId(recebendoId);
        }
    }
}
