using LearnHomeAPI.Domains;

namespace LearnHomeAPI.Interfaces
{
    public interface IAreaEspecializacaoRepository
    {
        List<AreaEspecializacao> Listar();
        AreaEspecializacao ObterPorId(int id);
        List<AreaEspecializacao> ObterPorNome(string nome);
    }
}
