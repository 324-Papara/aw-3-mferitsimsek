using System.Linq.Expressions;

namespace Para.Data.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Save();
    Task<TEntity?> GetById(long Id);
    Task Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Delete(long Id);
    Task<List<TEntity>> GetAll();

    // Dinamik where sorgularý için kullanýlacak method.
    Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);

    // Sorgulanabilir bir IQueryable döndürür.
    Task<IQueryable<TEntity>> GetQueryable();

    // Ýliþkili tablolarý dahil ederek tüm varlýklarý getirir.
    Task<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

    // Belirli bir koþula göre filtreleyerek ve iliþkili tablolarý dahil ederek varlýk getirir.
    Task<TEntity> GetWithWhereAndInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    // Belirli bir koþula göre filtreleyerek ve iliþkili tablolarý dahil ederek varlýklarý getirir.
    Task<List<TEntity>> GetAllWithWhereAndInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

    // dbContext.Customers.Where(x=> x.Name == "Ali") gibi sorguyu dinamik almak için
    // propertyName = sorgulanacak özelliðin adý "Name"
    // comparison = Karþýlaþtýrma operatörü QueryHelper daki (eq, neq, gt, gte, lt, lte, contains)
    // value = Karþýlaþtýrýlacak deðer "Ali" gibi
    // includeProperties = Include edilecek tablolar.
    Task<List<TEntity>> GetWithDynamicQuery(string propertyName, string comparison, string value, params Expression<Func<TEntity, object>>[] includeProperties);
}