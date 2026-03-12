using LearnHomeAPI.Domains;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;
using LearnHomeAPI.Repositories;
using Microsoft.Identity.Client;

namespace LearnHomeAPI.Applications.Service
{
    public class InstrutorService
    {
        private readonly IInstrutorRepository _repository;
        public InstrutorService(IInstrutorRepository instrutorRepository)
        {
            _repository = instrutorRepository;
        }

        public List<Instrutor> Listar()
        {
            List<Instrutor> instrutores = _repository.Listar();
            if (instrutores == null || instrutores.Count == 0)
            {
                throw new DomainException("Nenhum instrutor para ser listado!");
            }

            return instrutores;
        }

        public Instrutor ObterPorId(int id)
        {
            Instrutor instrutor = _repository.ObterPorId(id);
            if (instrutor == null)
            {
                throw new DomainException("Nenhum instrutor para ser listado!");
            }

            return instrutor;
        }

        public void Criar(Instrutor instrutor)
        {
            Instrutor instrutorBanco = _repository.ObterPorNome(instrutor.Nome);
            if (instrutorBanco == null)
                throw new DomainException("Já existe um instrutor com esse nome!");

            _repository.Criar(instrutor);
        }

        public void Atualizar(int id, Instrutor instrutor)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id);
            if (instrutorBanco == null)
                throw new DomainException("Nenhum instrutor encontrado para ser atualizado!");

            _repository.Atualizar(id, instrutor);
        }

        public void Remover(int id)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id);
            if (instrutorBanco == null)
                throw new DomainException("Nenhum instrutor encontrado para ser removido!");

            _repository.Remover(id);
        }
    }
}
