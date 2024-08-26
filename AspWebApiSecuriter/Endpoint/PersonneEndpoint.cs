using AspWebApiSecuriter.DTO;
using AspWebApiSecuriter.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using AspWebApiSecuriter.Data.Models;
using System.Threading;

namespace AspWebApiSecuriter.Endpoint
{
    public static class PersonneEndpoint
    {
        public static WebApplication MapPersonEndpoint(this WebApplication App)
        {
            var groupMap = App.MapGroup("/Person").WithTags("PersonManagement");

            groupMap.MapGet("", GetAll);

            groupMap.MapGet("/{id}", GetById)
                .Produces(200)
                .Produces(404)
                .Produces(201);

            groupMap.MapDelete("/{id}", DeleteById)
                .Produces(200)
                .Produces(404)
                 ;

            groupMap.MapPut("/{id}", PutById)
                .Produces(200)
                .Produces(404)
                ;

            groupMap.MapPost("", Post)
                .Produces(200)
                .Produces(404)
                 .Produces<PersonneOutPutModel>(contentType: "application/json")
                .Accepts<PersonneInPutModel>(contentType: "application/json")
                .WithName("GetById");

            return App;
        }

        public static async Task<IResult> GetAll
             ([FromServices] IPersonService service)
        {
                var people = await service.GetAll();
                return Results.Ok(people);
            
        }


        public static async Task<IResult> GetById(
                [FromRoute] string id,
                [FromServices] IPersonService service)
          {
            var people = await service.GetById(id);
            if (people is not null) return Results.NotFound();
            
            return Results.Ok(people);
        }
        public static async Task<IResult> DeleteById(
                [FromRoute] string id,
                [FromServices] IPersonService service)
        {
            var people = await service.Delete(id);
            if (people) return Results.NoContent();
            return Results.NotFound();
        }
        public static async Task<IResult> PutById(
                [FromRoute] string id,
                [FromBody] PersonneInPutModel po,
                [FromServices] IPersonService service)
        {
            var result = await service.Update(id, po);
            if (result) 
            { 
                return Results.NoContent();
            }
            return Results.NotFound();
        }
        public static async Task<IResult> Post(
                [FromBody] PersonneInPutModel p,
                [FromServices] IPersonService service,
                [FromServices] IValidator<PersonneInPutModel> validator,
                [FromServices] LinkGenerator linkGenerator,
                HttpContext httpContext,
                CancellationToken token)
        {
            var result = validator.Validate(p);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(e => new
                {
                    Message = e.ErrorMessage,
                    e.PropertyName
                }));
            }
            var dbResult = await service.Add(p);
            var path = linkGenerator.GetUriByName(httpContext, "GetById", new { id = dbResult.Id });
            //return Results.Created(path, dbResult);
            return Results.Ok(dbResult);
        }
    }
}
