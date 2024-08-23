using AspWebApiSecuriter.DTO;
using FluentValidation;

namespace AspWebApiSecuriter.Validation
{
    public class PersonValidation : AbstractValidator<PersonneInPutModel>
    {
        public PersonValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Birthday).LessThanOrEqualTo(DateTime.Now);
        }
    }
}
