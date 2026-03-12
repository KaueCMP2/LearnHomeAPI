using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Repositories
{
    public class AreaExpecializacaoRepository : IAreaExpecializacaoRepository
    {
        private readonly LearnHomeDbContext ctx;
        public AreaExpecializacaoRepository(LearnHomeDbContext context)
        {
            ctx = context;
        }

        public List<AreaExpecializacao> Listar()
        {
            return ctx.AreaExpecializacao.ToList();
        }

        public AreaExpecializacao ObterPorId(int id)
        {
            return ctx.AreaExpecializacao.FirstOrDefault(a => a.Id == id);
        }

        public AreaExpecializacao ObterPorNome(string nome)
        {
            return ctx.AreaExpecializacao.FirstOrDefault(a => a.Area == nome);
        }
    }
}
