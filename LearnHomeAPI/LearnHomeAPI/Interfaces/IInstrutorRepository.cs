using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();
        Instrutor ObterPorId(int id);
        Instrutor ObterPorNome(string nome);
        void Criar(Instrutor instrutor);
        void Atualizar(int id, Instrutor instrutor);
        void Remover(int id);
    }
}
