using Business.Abstact;
using Entities.Concrete;
using DataAccess.Abstact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Core.Utilities.Results;
using Business.Constants;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingconcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Business.DependecyResolvers.CCS;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]// validation (min,max karakter doğrulama)
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        { // businnes codes (iş kuralları)
            IResult result = BusinessRules.Run(CheckIfProductCountCategoryCorrect(product.CategoryId),//result : kurala uymayan logic
                CheckIfProductNameExists(product.ProductName),CheckCategoryCount()); //Hatalı bir kural dönerse  

            if (result != null) // uymayan var ise onu dönder
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);




        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 19)
            //{
            //    return new ErrrorDataResult<List<Product>>(Messages.MainteneanceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.CategoryByListed);

        }

        [CacheAspect]
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id), Messages.ProductByListed);

        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MainteneanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            //düzeltemeyi unutma

            if (CheckIfProductCountCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult(Messages.ProductAdded);
            }

            return new ErrorResult();
        }



        private IResult CheckIfProductCountCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result <= 10)
            {
                return new ErrorResult("bir kategorideki ürün sayısı 10u geçemz");
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (result == true)
            {
                return new ErrorResult("aynı isim mevccut");
            }
            return new SuccessResult();
        }

        private IResult CheckCategoryCount()
        {
            var result = _categoryService.GetAll( );

            if (result.Data.Count>15)
            {
                return new ErrorResult("categori adeti 15 i geçemez");
            }
            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
