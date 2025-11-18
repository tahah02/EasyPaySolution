using EasyPay.Data.Dtos;
using FluentValidation;

namespace EasyPay.WebAPI.Validators
{
    public class TransferValidator : AbstractValidator<TransferRequestDto>
    {
        public TransferValidator()
        {
            
            RuleFor(x => x.FromUser)
                .NotEmpty().WithMessage("Enter The Receiver's Name");

            
            RuleFor(x => x.ToUser)
                .NotEmpty().WithMessage("Enter the Sender's Name");

            
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("You can not send 0 Rs")
                .LessThan(100000).WithMessage("You can not send more than 10 lacs"); 

            
            RuleFor(x => x)
                .Must(x => x.FromUser != x.ToUser)
                .WithMessage("You can not send money to yourself");
        }
    }
}
