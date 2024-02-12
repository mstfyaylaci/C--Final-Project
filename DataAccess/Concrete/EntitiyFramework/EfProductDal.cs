using Core.DataAccess.EntityFramework;
using DataAccess.Abstact;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitiyFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal //IproductDal Ürüne ait özel operasyonlar yazmak için var
    {
        //public void Add(Product entity)
        //{
        //    using (NorthwindContext context=new NorthwindContext()) // using kullanılmasının sebebi foknsiyon bitince garbageCollector anında çağırır
        //                                                            // temizler , Context nesenesi pahalı çünkü
        //    {
        //        var addedEntity=context.Entry(entity); // git veri kaynağından bu gönderilen productı eşleştir referansı yakala
        //        addedEntity.State = EntityState.Added;
        //        context.SaveChanges();// kaydet
        //    }
        //}

        //public void Delete(Product entity)
        //{
        //    using(NorthwindContext context = new NorthwindContext()) // using kullanılmasının sebebi foknsiyon bitince garbageCollector anında çağırır
        //                                                            // temizler , Context nesenesi pahalı çünkü
        //    {
        //        var deletedEntity = context.Entry(entity); // git veri kaynağından bu gönderilen productı eşleştir referansı yakalıa
        //        deletedEntity.State = EntityState.Deleted; // o yakalanan referasnı sil 
        //        context.SaveChanges();// kaydet
        //    }
        //}

        //public Product Get(Expression<Func<Product, bool>> filter)
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        return context.Set<Product>().SingleOrDefault(filter);// ürünler tablosuna filtreyi uygula

        //    }
        //}

        //public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        //{
        //    using (NorthwindContext context=new NorthwindContext())
        //    {
        //        return filter==null 
        //            ? context.Set<Product>().ToList() // Eğer filtre null ise ürünleri listele
        //            : context.Set<Product>().Where(filter).ToList(); //Eğer filtre null değilse filterelenmiş ürünleri listele

        //    }
        //}

        //public void Update(Product entity)
        //{
        //    using (NorthwindContext context = new NorthwindContext()) // using kullanılmasının sebebi foknsiyon bitince garbageCollector anında çağırır
        //                                                              // temizler , Context nesenesi pahalı çünkü
        //    {
        //        var updatedEntity = context.Entry(entity); // git veri kaynağından bu gönderilen productı eşleştir referansı yakalıa
        //        updatedEntity.State = EntityState.Modified; // o yakalanan referasnı güncelle 
        //        context.SaveChanges();// kaydet
        //    }
        //}
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             { ProductId = p.ProductId, ProductName = p.ProductName,
                                 CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock 
                             };
                return result.ToList();
            }
        }
    }
}
