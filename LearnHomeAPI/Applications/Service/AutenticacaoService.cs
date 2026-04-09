using Assets_menagement_system.Application.Autenticacao;
using Assets_menagement_system.DTOs.AutenticacaoDTO;
using LearnHomeAPI.Application.Autenticacao;
using LearnHomeAPI.Domains;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LearnHomeAPI.Application.Services
{
    public class AutenticacaoService
    {
        private readonly GeradorTokenJwt _geradorTokenJwt;
        private readonly IInstrutorRepository _repository;
        public AutenticacaoService(GeradorTokenJwt geradorTokenJwt, IInstrutorRepository instrutorRepository)
        {
            _geradorTokenJwt = geradorTokenJwt;
            _repository = instrutorRepository;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitada = CriptografarUsuario.CriptografarSenha(senhaDigitada);

            return hashDigitada.SequenceEqual(senhaHashBanco);
        }
        public TokenDTO Login(LoginDTO loginDTO)
        {

            Instrutor instrutor = _repository.ObterPorId(loginDTO.Id);
            if (instrutor == null)
                throw new DomainException("ID ou senha inválidos ");

            if (!VerificarSenha(loginDTO.Senha, instrutor.Senha))
                throw new DomainException("NIF ou senha inválidos");

            string token = _geradorTokenJwt.GerarToken(instrutor);

            return new TokenDTO
            {
                Token = token,
                TipoUsuario = ("Instrutor")
            };
        }
    }
}
