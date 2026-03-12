using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IAreaExpecializacaoRepository
    {
        List<AreaExpecializacao> Listar();
        AreaExpecializacao ObterPorId(int id);
        AreaExpecializacao ObterPorNome(string nome);
    }
}
