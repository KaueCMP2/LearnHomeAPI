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
            return ctx.Instrutor.Find(id);
        }

        public List<Instrutor> ObterPorNome(string nome)
        {
            return ctx.Instrutor.Where(a => a.Nome.Contains(nome)).ToList();

        }

        public List<Instrutor> ObterPorEmail(string email)
        {
            return ctx.Instrutor.Where(i => i.Email.Contains(email)).ToList();
        }

        public bool EmailExiste(string email)
        {
            return ctx.Instrutor.Any(i => i.Email == email);
        }


        public bool InstrutorExiste(string email)
        {
            return ctx.Instrutor.Any(i => i.Email == email);
        }

        public void Adicionar(Instrutor Instrutor)
        {
            ctx.Instrutor.Add(Instrutor);
            ctx.SaveChanges();
        }

        public void Atualizar(int id, Instrutor Instrutor)
        {
            var InstrutorExistente = ctx.Instrutor.FirstOrDefault(i => i.Id == id);
            if (InstrutorExistente != null)
            {
                InstrutorExistente.Nome = Instrutor.Nome;
                InstrutorExistente.AreaEspecializacaoId = Instrutor.AreaEspecializacaoId;
                ctx.SaveChanges();
            }
        }

        public void Remover(int id)
        {
            var Instrutor = ctx.Instrutor.FirstOrDefault(i => i.Id == id);
            if (Instrutor != null)
            {
                ctx.Instrutor.Remove(Instrutor);
                ctx.SaveChanges();
            }
        }
    }
}
