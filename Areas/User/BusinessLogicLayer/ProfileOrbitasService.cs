using AspnetCoreMvcStarter.Areas.User.DataAccessLayer;
using AspnetCoreMvcStarter.Models;
using System.Linq.Expressions;

namespace AspnetCoreMvcStarter.Areas.User.BusinessLogicLayer
{
  public class ProfileOrbitasService<T> where T : class
  {
    private readonly ProfileOrbitasRepository<T> _repository;
    private readonly SocialGroupsService<T> _social_groups_repository;
    public ProfileOrbitasService(ProfileOrbitasRepository<T> repository, SocialGroupsService<T> social_groups_repository)
    {
      _repository = repository;
      _social_groups_repository = social_groups_repository;
    }

    public IEnumerable<T> GetAll()
    {
      return _repository.GetAll();
    }

    public T GetById(int id)
    {
      return _repository.GetById(id);
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
