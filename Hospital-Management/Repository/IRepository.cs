using Hospital_Management.Models;

namespace Hospital_Management.Repository
{
    public interface IRepository<Entity, IdType>
    {
        public List<Entity> GetAll();
        public List<Entity> GetPage(int page);
        public int GetTotalPages(int pageSize);
        public List<Entity> Search(string searchString, string searchProperty);
        public Entity GetById(IdType id);
        public void Insert(Entity entity);
        public void Update(Entity entity);
        public void Delete(IdType id);
        public void Save();

    }
}
