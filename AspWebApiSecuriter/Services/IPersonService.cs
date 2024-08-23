using AspWebApiSecuriter.DTO;
using AspWebApiSecuriter.Services;

namespace AspWebApiSecuriter.Services
{
    public interface IPersonService
    {
        Task<List<PersonneOutPutModel>> GetAll();
        Task<PersonneOutPutModel?> GetById(string id);
        Task<PersonneOutPutModel> Add(PersonneInPutModel person);
        Task<bool> Update(string id, PersonneInPutModel person);
        Task<bool> Delete(string id);

         

    }
}
