using FluentValidation;
using MyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.Validators.Products
{
    public class CreateAnnouncementValidator : AbstractValidator<Announcement>
    {
        public CreateAnnouncementValidator()
        {
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Currency).NotNull();
        }
    }
}
