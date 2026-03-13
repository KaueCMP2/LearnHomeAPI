using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Repositories
{
    public class AreaEspecializacaoRepository : IAreaEspecializacaoRepository
    {
        private readonly LearnHomeDbContext ctx;
        public AreaEspecializacaoRepository(LearnHomeDbContext context)
        {
            ctx = context;
        }

        public List<AreaEspecializacao> Listar()
        {
            return ctx.AreaEspecializacao.ToList();
        }

        public AreaEspecializacao ObterPorId(int id)
        {
            return ctx.AreaEspecializacao.FirstOrDefault(a => a.Id == id);
        }

        public AreaEspecializacao ObterPorNome(string nome)
        {
            return ctx.AreaEspecializacao.FirstOrDefault(a => a.Area == nome);
        }
    }
}
