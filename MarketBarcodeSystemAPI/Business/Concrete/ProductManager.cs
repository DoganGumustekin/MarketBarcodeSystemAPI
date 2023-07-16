using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Business.ValidationRules.FluentValidation;
using MarketBarcodeSystemAPI.Core.Aspects.Autofac.Validation;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }



        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(DidThisProductAlreadyExist(product.BarcodeId));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Delete(Product product)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(IsThisProductAvailable(product.BarcodeId));
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<Product> GetById(long barcodeId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.BarcodeId == barcodeId));
        }

        public IResult AddToCart(Product product, int NumberOfProducts) //sepete ekle
        {
            if (NumberOfProducts <= product.StockQuantity)
            {
                var result = product.StockQuantity - NumberOfProducts;
                product.StockQuantity = result;
                _productDal.Update(product);
                return new SuccessResult(Messages.AddToCart);
            }
            return new SuccessResult(Messages.NotEnoughProduct);
        }

        public IResult DeleteToCart(Product product, int NumberOfProducts) //sepetten çıkar
        {
            var result = product.StockQuantity + NumberOfProducts;
            product.StockQuantity = result;
            _productDal.Update(product);
            return new SuccessResult(Messages.DeleteToCart);
        }


        private IResult IsThisProductAvailable(long barcodeId)
        {
            var result = _productDal.GetAll(p => p.BarcodeId == barcodeId).Any();
            if (result)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.ProductIsNotAvailable);
        }

        private IResult DidThisProductAlreadyExist(long barcodeId)
        {
            var result = _productDal.GetAll(p => p.BarcodeId == barcodeId).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductIsAvailable);
            }
            return new SuccessResult();
        }

    }
}
