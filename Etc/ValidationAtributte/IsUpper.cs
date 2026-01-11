using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Done.Etc.ValidationAtributte
{
    public class IsUpper() : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? text = value as string;
            if(!string.IsNullOrEmpty(text))
            {
                if (text.Any(char.IsUpper)) return true;
            }
            return false;
        }
    }
}
