using AspnetCoreMvcStarter.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspnetCoreMvcStarter.Areas.User.DataAccessLayer
{
  public class SocialGroupsRepository<T> where T : class
  {
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public SocialGroupsRepository(ApplicationDbContext context)
    {
      _context = context;
      _dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
      return _dbSet.ToList();
    }
    public T GetById(int id)
    {
      return _dbSet.Find(id);
    }
    public string GetNameByID(int id)
    {
      // Lấy thực thể theo ID
      var entity = _dbSet.Find(id);

      // Kiểm tra nếu thực thể tồn tại và có thuộc tính Name
      if (entity != null)
      {
        var nameProperty = typeof(T).GetProperty("Name");
        if (nameProperty != null)
        {
          return nameProperty.GetValue(entity)?.ToString() ?? string.Empty;
        }
      }

      return string.Empty;
    }
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
      return _dbSet.Where(predicate).ToList();
    }
    public void Add(T entity)
    {
      _dbSet.Add(entity);
      _context.SaveChanges();
    }
    public void Update(T entity)
    {
      _dbSet.Update(entity);
      _context.SaveChanges();
    }
    public async Task<bool> DeleteAsync(int id)
    {
      var entity = await _dbSet.FindAsync(id);
      if (entity == null)
      {
        return false;
      }

      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
      return true;
    }


  }
}
