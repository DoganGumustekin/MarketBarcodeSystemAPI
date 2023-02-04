using FluentValidation;
using MarketBarcodeSystemAPI.Entities.Concrete;

namespace MarketBarcodeSystemAPI.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.BarcodeId).NotEmpty().WithMessage("Lütfen barkod numarasını giriniz");
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Lütfen ürün adı giriniz");
            RuleFor(p => p.ProductPrice).NotEmpty().WithMessage("Lütfen fiyat bilgisi giriniz");
            RuleFor(p => p.BarcodeId).Must(ProductLenght).WithMessage("Barkod uzunluğu 13 haneli olmalıdır!");
        }


        private bool ProductLenght(long arg)
        {
            if (arg.ToString().Length != 13)
            {
                return false;
            }
            return true;
        }
    }
}
