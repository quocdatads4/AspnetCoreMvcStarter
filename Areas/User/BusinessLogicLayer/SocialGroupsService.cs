using AspnetCoreMvcStarter.Areas.User.DataAccessLayer;
using AspnetCoreMvcStarter.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer
{
  public class SocialGroupsService<T> where T : class
  {
    private readonly SocialGroupsRepository<T> _repository;

    public SocialGroupsService(SocialGroupsRepository<T> repository)
    {
      _repository = repository;
    }

    public IEnumerable<T> GetAll()
    {
      return _repository.GetAll();
    }

    public T GetById(int id)
    {
      return _repository.GetById(id);
    }

    public string GetNameByID(int id)
    {
      // Lấy entity bằng ID
      var entity = _repository.GetById(id);

      // Kiểm tra nếu entity tồn tại và có thuộc tính Name
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
      return _repository.Find(predicate);
    }

    public void Add(T entity)
    {
      // Logic nghiệp vụ có thể thêm tại đây nếu cần
      _repository.Add(entity);
    }

    public void Update(T entity)
    {
      // Logic nghiệp vụ có thể thêm tại đây nếu cần
      _repository.Update(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
      return await _repository.DeleteAsync(id);
    }
  }


}
