using AspWebApiSecuriter.Data.Models;
using AspWebApiSecuriter.DTO;
using AspWebApiSecuriter.Services;


using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspWebApiSecuriter.Services
{
    public class EfCorePersonneservice : IPersonService
    {


        private readonly ApiDbContext context;

        public EfCorePersonneservice(
             ApiDbContext context)
        {
            this.context = context;
        }

        //private PersonneOutPutModel ToOutPutModel(Personne dbperson)
        //    => new PersonneOutPutModel(
        //        dbperson.DisplayId,
        //        $"{dbperson.Name} {dbperson.LastName}",
        //        dbperson.Birthday == DateTime.MinValue ? null : dbperson.Birthday,
        //        dbperson.Address
        //        );


        //public async Task<PersonneOutPutModel> Add(PersonneInPutModel person)
        //{
        //    var dbPerson = new Personne
        //    {

        //        Address = person.Address,
        //        Birthday = person.Birthday,
        //        LastName = person.LastName,
        //        Name = person.Name,
        //        DisplayId = DisplayIdGenerator.GenerateDisplayId("per")

        //    };
        //    context.People.Add(dbPerson);
        //    await context.SaveChangesAsync();
        //    return ToOutPutModel(dbPerson);
        //}
        private PersonneOutPutModel ToOutPutModel(Personne dbPerson)

               => new PersonneOutPutModel(
                    dbPerson.DisplayId,
                    $"{dbPerson.LastName} {dbPerson.Name}",
                    dbPerson.Birthday == DateTime.MinValue ? null : dbPerson.Birthday,
                    dbPerson.Address); 

        public async Task<PersonneOutPutModel> Add(PersonneInPutModel person)
        {
            var id = DisplayIdGenerator.GenerateDisplayId("per");
            var dbPerson = new Personne
            {
                Id = id,
                Birthday = person.Birthday.GetValueOrDefault(),
                LastName = person.LastName,
                Name = person.Name,
                Address = person.Address 
            };
            dbPerson.DisplayId = dbPerson.Id;

            context.People.Add(dbPerson);
            await context.SaveChangesAsync();
            return ToOutPutModel(dbPerson);
        }
        public async Task<bool> Delete(string id)
        {
            return await context.People.Where(p => p.DisplayId == id).ExecuteDeleteAsync() > 0;
        }
        public async Task<List<PersonneOutPutModel>> GetAll()
        {
            return (await context.People.ToListAsync()).ConvertAll(ToOutPutModel);
        }
        public async Task<PersonneOutPutModel?> GetById(string id)
        {
            var dbPerson = await context.People.Where(p => p.DisplayId == id).FirstOrDefaultAsync();
            if (dbPerson is not null) return ToOutPutModel(dbPerson);
            return null;
        }
        public async Task<bool> Update(string id, PersonneInPutModel person)
        {
            return await context.People
                .Where(p => p.DisplayId == id)
                .ExecuteUpdateAsync(
                per =>
                per.SetProperty(pe => pe.LastName, person.LastName)
                   .SetProperty(pe => pe.Birthday, person.Birthday)
                   .SetProperty(pe => pe.Name, person.Name)) > 0;
        }

    }
}
