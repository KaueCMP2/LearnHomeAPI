using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();
        Instrutor ObterPorId(int id);
        Instrutor ObterPorNome(string nome);
        Instrutor ObterPorEmail(string email);
        bool InstrutorExiste(string email);
        void Adicionar(Instrutor instrutor);
        void Atualizar(int id, Instrutor instrutor);
        void Remover(int id);
    }
}
