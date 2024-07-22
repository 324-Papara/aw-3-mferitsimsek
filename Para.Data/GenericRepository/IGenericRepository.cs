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

    // Dinamik where sorgular� i�in kullan�lacak method.
    Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);

    // Sorgulanabilir bir IQueryable d�nd�r�r.
    Task<IQueryable<TEntity>> GetQueryable();

    // �li�kili tablolar� dahil ederek t�m varl�klar� getirir.
    Task<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

    // Belirli bir ko�ula g�re filtreleyerek ve ili�kili tablolar� dahil ederek varl�k getirir.
    Task<TEntity> GetWithWhereAndInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    // Belirli bir ko�ula g�re filtreleyerek ve ili�kili tablolar� dahil ederek varl�klar� getirir.
    Task<List<TEntity>> GetAllWithWhereAndInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

    // dbContext.Customers.Where(x=> x.Name == "Ali") gibi sorguyu dinamik almak i�in
    // propertyName = sorgulanacak �zelli�in ad� "Name"
    // comparison = Kar��la�t�rma operat�r� QueryHelper daki (eq, neq, gt, gte, lt, lte, contains)
    // value = Kar��la�t�r�lacak de�er "Ali" gibi
    // includeProperties = Include edilecek tablolar.
    Task<List<TEntity>> GetWithDynamicQuery(string propertyName, string comparison, string value, params Expression<Func<TEntity, object>>[] includeProperties);
}