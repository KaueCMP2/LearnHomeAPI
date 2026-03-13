using LearnHomeAPI.Contexts;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Repositories
{
    public class InstrutorRepository : IInstrutorRepository
    {
        private readonly LearnHomeDbContext ctx;
        public InstrutorRepository(LearnHomeDbContext context)
        {
            ctx = context;
        }

        public List<Instrutor> Listar()
        {
            return ctx.Instrutor.ToList();
        }

        public Instrutor ObterPorId(int id)
        {
            return ctx.Instrutor.FirstOrDefault(i => i.Id == id);
        }

        public Instrutor ObterPorNome(string nome)
        {
            return ctx.Instrutor.FirstOrDefault(i => i.Nome == nome);
        }

        public Instrutor ObterPorEmail(string email)
        {
            return ctx.Instrutor.FirstOrDefault(i => i.Email == email);
        }

        public bool InstrutorExiste(string email)
        {
            return ctx.Instrutor.Any(i => i.Email == email);
        }

        public void Adicionar(Instrutor instrutor)
        {
            ctx.Instrutor.Add(instrutor);
            ctx.SaveChanges();
        }

        public void Atualizar(int id, Instrutor instrutor)
        {
            var instrutorExistente = ctx.Instrutor.FirstOrDefault(i => i.Id == id);
            if (instrutorExistente != null)
            {
                instrutorExistente.Nome = instrutor.Nome;
                instrutorExistente.AreaEspecializacaoId = instrutor.AreaEspecializacaoId;
                ctx.SaveChanges();
            }
        }

        public void Remover(int id)
        {
            var instrutor = ctx.Instrutor.FirstOrDefault(i => i.Id == id);
            if (instrutor != null)
            {
                ctx.Instrutor.Remove(instrutor);
                ctx.SaveChanges();
            }
        }
    }
}
