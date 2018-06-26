using System.Collections.Generic;
using JWTWebApiAuth.Models;

namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Interface describes operations performed by database provider service
    /// </summary>
    public interface IDatabaseProvideService
    {
        int Create(ContactsModel entity);

        int Update(ContactsModel entity);

        int Delete(int? id);

        List<ContactsModel> Get();

        ContactsModel Get(int? id);
    }
}