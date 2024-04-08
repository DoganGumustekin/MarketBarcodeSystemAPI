﻿using MarketBarcodeSystemAPI.Business.Abstract;
using MarketBarcodeSystemAPI.Business.BusinessAspects.Autofac;
using MarketBarcodeSystemAPI.Business.Constans;
using MarketBarcodeSystemAPI.Business.ValidationRules.FluentValidation;
using MarketBarcodeSystemAPI.Core.Aspects.Autofac.Validation;
using MarketBarcodeSystemAPI.Core.Utilities.Business;
using MarketBarcodeSystemAPI.Core.Utilities.Results;
using MarketBarcodeSystemAPI.DataAccess.Abstract;
using MarketBarcodeSystemAPI.DataAccess.Concrete.EntityFramework;
using MarketBarcodeSystemAPI.Entities.Concrete;
using IResult = MarketBarcodeSystemAPI.Core.Utilities.Results.IResult;

namespace MarketBarcodeSystemAPI.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICartDal _cartDal;
        public ProductManager(IProductDal productDal, ICartDal cartDal)
        {
            _productDal = productDal;
            _cartDal = cartDal;
        }


        //[SecuredOperation("admin")]
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

        public IResult AddToCart(long barcodeId, int userId, int numberOfProducts)
        {
            var cart = new Cart();
            cart.UserId = userId;
            cart.BarcodeId = barcodeId;
            cart.NumberOfProduct = numberOfProducts;

            _cartDal.Add(cart);
            using (MarketManagementContext context = new MarketManagementContext())
            {
                var product = context.Products.FirstOrDefault(p => p.BarcodeId == barcodeId);
                if (product != null)
                {
                    product.StockQuantity = product.StockQuantity - numberOfProducts;
                    context.SaveChanges();
                }
            }
            return new SuccessResult(Messages.CartAdded);
        }

        //public IResult DeleteToCart(Product product, int NumberOfProducts) //sepetten çıkar
        //{
        //    var result = product.StockQuantity + NumberOfProducts;
        //    product.StockQuantity = result;
        //    _productDal.Update(product);
        //    return new SuccessResult(Messages.DeleteToCart);
        //}


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

        //private string ConvertStringToByteForImage(string imageString)
        //{
        //    byte[] imageByte = Convert.FromBase64String(imageString);
        //    if (imageByte == null)
        //    {
        //        return imageString;
        //    }
        //    return imageByte.ToString();
        //}
    }
}
