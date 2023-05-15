using System;
using FluentValidation;
using Rusada.Web.Models.APIViewModels;

namespace Rusada.Web.Models.Validators
{
	public class PlaneSightingEditVMValidator : AbstractValidator<PlaneSightingEditVM>
    {
		public PlaneSightingEditVMValidator()
		{
            //Make is required and max 128 characters
            RuleFor(ps => ps.Make)
                .NotEmpty()
                .MaximumLength(128);

            //Model is required and max 128 characters
            RuleFor(ps => ps.Model)
                .NotEmpty()
                .MaximumLength(128);

            //Location is required and max 128 characters
            RuleFor(ps => ps.Location)
                .NotEmpty()
                .MaximumLength(255);

            /*
             * Registration 
             * Two parts separated by a hyphen. 
             * Prefix should be either one or two characters. 
             * Suffix should be between one and five characters
            */
            RuleFor(ps => ps.Registration)
                .NotEmpty()
                .MaximumLength(128)
                .Matches("[A-Za-z]{1,2}-[A-Za-z]{1,5}");//TODO check this

            //Must be a valid datetime in the past
            RuleFor(ps => ps.SeenDateTime)
                .NotEmpty()
                .LessThan(DateTime.UtcNow);
        }
	}
}

