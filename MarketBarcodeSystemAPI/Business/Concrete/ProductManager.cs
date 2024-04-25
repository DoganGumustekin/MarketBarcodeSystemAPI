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
        public async Task<IResult> Add(Product product)
        {
            IResult result = BusinessRules.Run(DidThisProductAlreadyExist(product.BarcodeId));
            if (result != null)
            {
                return result;
            }
            else
            {
                using (MarketManagementContext context = new MarketManagementContext())
                {
                    byte[] imageDataBytes = Convert.FromBase64String(product.ImageData);
                    string addImage = await SaveImageAsync(imageDataBytes);

                    Product addProduct = new Product();
                    addProduct.BarcodeId = product.BarcodeId;
                    addProduct.AccountKey = product.AccountKey;
                    addProduct.ImageData = addImage;
                    addProduct.ImageName = addImage;
                    addProduct.ProductName = product.ProductName;
                    addProduct.ProductPrice = product.ProductPrice;
                    addProduct.Description = product.Description;
                    addProduct.StockQuantity = product.StockQuantity;

                    _productDal.Add(addProduct);
                }
                return new SuccessResult(Messages.ProductAdded);
            }
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
            var product = _productDal.Get(p => p.BarcodeId == barcodeId);
            if (product != null)
            {
                product.ImageData = GetImageAsBase64String(product.ImageData);
                return new SuccessDataResult<Product>(product);
            }
            return new ErrorDataResult<Product>("Ürün bulunamadı");
        }

        public IResult AddToCart(long barcodeId, int userId, int numberOfProducts)
        {
            using (MarketManagementContext context = new MarketManagementContext())
            {
                var cartForMultipleProducttInCart = context.Cart.FirstOrDefault(c => c.BarcodeId == barcodeId && c.UserId == userId);
                if (cartForMultipleProducttInCart != null)
                {
                    cartForMultipleProducttInCart.NumberOfProduct += numberOfProducts;
                    _cartDal.Update(cartForMultipleProducttInCart);
                }
                else
                {
                    var cart = new Cart();
                    cart.UserId = userId;
                    cart.BarcodeId = barcodeId;
                    cart.NumberOfProduct = numberOfProducts;
                    _cartDal.Add(cart);
                }


                var product = context.Products.FirstOrDefault(p => p.BarcodeId == barcodeId);
                if (product != null)
                {
                    product.StockQuantity = product.StockQuantity - numberOfProducts;
                    context.SaveChanges();
                }
            }
            return new SuccessResult(Messages.CartAdded);
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

        private async Task<string> SaveImageAsync(byte[] imageData)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
            string filePath = Path.Combine("C:\\Users\\Doğan\\Desktop\\Images", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(imageData, 0, imageData.Length);
            }

            return uniqueFileName;
        }

        private string GetImageAsBase64String(string imageName)
        {
            try
            {
                string imagePath = Path.Combine("C:\\Users\\Doğan\\Desktop\\Images", imageName);
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda logla ve boş bir string döndür.
                Console.WriteLine("Resim okuma hatası: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
