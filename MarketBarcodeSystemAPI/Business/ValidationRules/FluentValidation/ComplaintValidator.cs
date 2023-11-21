using FluentValidation;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.Business.ValidationRules.FluentValidation
{
    public class ComplaintValidator : AbstractValidator<Complaint>
    {
        public ComplaintValidator()
        {
            RuleFor(p => p.ComplaintDescription).NotEmpty().WithMessage("Lütfen Şikayet Açıklaması Kısmını Boş Bırakmayınız.");
            RuleFor(p => p.ComplaintDescription).Must(ComplaintCorrect).WithMessage("Argo kelime kullanılamaz!");
        }

        private bool ComplaintCorrect(string complaintDescription)
        {
            string[] x = { "salak", "mal" };
            foreach (string item in x)
            {
                if (complaintDescription.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
