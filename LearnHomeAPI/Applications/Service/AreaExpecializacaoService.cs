using LearnHomeAPI.Domains;
using LearnHomeAPI.DTOs.AlunoDto;
using LearnHomeAPI.Exceptions;
using LearnHomeAPI.Interfaces;

namespace LearnHomeAPI.Applications.Service
{
    public class AreaEspecializacaoService
    {
        private readonly IAreaEspecializacaoRepository _repository;
        public AreaEspecializacaoService(IAreaEspecializacaoRepository repository)
        {
            _repository = repository;
        }

        public LerAreaEspecializacaoDto ConverterAreaEspecializacaoParaDto(AreaEspecializacao areaEspecializacao)
        {
            var area = new LerAreaEspecializacaoDto
            {
                Id = areaEspecializacao.Id,
                Area = areaEspecializacao.Area,
                Instrutor = areaEspecializacao.Instrutor
            };

            return area;
        }
        public List<LerAreaEspecializacaoDto> Listar()
        {
            List<AreaEspecializacao> areas = _repository.Listar();

            if (areas.Any() == false)
                throw new DomainException("Nenhuma area encontrada!");

            List<LerAreaEspecializacaoDto> areasDto = areas.Select(areasSelecionadas => ConverterAreaEspecializacaoParaDto(areasSelecionadas))
                .ToList();

            return areasDto;
        }

        public LerAreaEspecializacaoDto ObterPorId(int id)
        {
            AreaEspecializacao area = _repository.ObterPorId(id);
            LerAreaEspecializacaoDto areaDto = ConverterAreaEspecializacaoParaDto(area);

            if (areaDto == null)
                throw new DomainException("Nenhuma area encontrada!");

            return areaDto;
        }

        public LerAreaEspecializacaoDto ObterPorNome(string nome)
        {
            AreaEspecializacao area = _repository.ObterPorNome(nome);
            LerAreaEspecializacaoDto areaDto = ConverterAreaEspecializacaoParaDto(area);

            if (areaDto == null)
                throw new DomainException("Nenhuma area encontrada!");

            return areaDto;
        }
    }
}
