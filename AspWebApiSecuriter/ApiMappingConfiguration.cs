using AspWebApiSecuriter.Data.Models;
using AspWebApiSecuriter.DTO;
using AutoMapper;

namespace AspWebApiSecuriter
{
    public class ApiMappingConfiguration : Profile
    {
        public ApiMappingConfiguration()
        {

            CreateMap<Personne, PersonneOutPutModel>()
                .ConstructUsing(p => new
                PersonneOutPutModel(p.Id,
                $"{p.LastName}{p.Name}",
                p.Birthday == DateTime.MinValue ? null : p.Birthday,
                p.Address));

            CreateMap<PersonneInPutModel, Personne>();

        }
    }
}
