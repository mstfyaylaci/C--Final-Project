using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //Generic repository fonksiyonları, 
                                             //Generic Constrait
    public interface IEntityRepository<T> where T : class , IEntity,new ()// -- T:class:sadece referans tipler
                                                                          //-- IEntitiy:veri tabanı nesnesi olan class yazılabilir
                                                                          //-- new(): newlenebilir olması yani class olabilmesi
    {                                                                           
        List<T> GetAll(Expression<Func<T,bool>> filter=null);//Experssion Linq ile sorgu , filtreleme yapabilmemizii sağlıyor
                                                               //p=>p.Category.Id gibi
        T Get(Expression<Func<T, bool>> filter);// filtre vermek zorunlu 

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        
    }
}

