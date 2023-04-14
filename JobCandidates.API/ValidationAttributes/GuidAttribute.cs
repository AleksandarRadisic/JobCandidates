using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace JobCandidates.API.ValidationAttributes
{
    public class GuidAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' does not contain a valid guid";

        public GuidAttribute() : base(DefaultErrorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            Guid guid;
            if (!Guid.TryParse(input, out guid) || guid.Equals(Guid.Empty))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
