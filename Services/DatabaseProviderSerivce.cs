using System.Collections.Generic;
using System.Linq;
using JWTWebApiAuth.Models;
using JWTWebApiAuth.Infrastructure;


namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Database provider service. Used for CRUD operations.
    /// </summary>
    public class DatabaseProviderService : IDatabaseProvideService
    {
        private readonly ContactsListDbContext _databaseContext;

        public DatabaseProviderService(ContactsListDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int Create(ContactsModel entity)
        {
            if(entity == null)
                return 0;

            _databaseContext.Add(entity);
            return _databaseContext.SaveChanges();
        }

        public int Delete(int? id)
        {
            if(id == null)
                return 0;

            var entityToDelete = _databaseContext.ContactList.SingleOrDefault(ent => ent.Id == id);

            if(entityToDelete == null)
                return 0;

            _databaseContext.Remove(entityToDelete);

            return _databaseContext.SaveChanges();
        }

        public List<ContactsModel> Get()
        {
            return _databaseContext.ContactList.ToList();
        }

        public ContactsModel Get(int? id)
        {
            if(id == null)
                return null;

            return _databaseContext.ContactList.SingleOrDefault(ent => ent.Id == id);
        }

        public int Update(ContactsModel entity)
        {
            if(entity == null)
                return 0;

            _databaseContext.Update(entity);

            return _databaseContext.SaveChanges();
        }
    }
}