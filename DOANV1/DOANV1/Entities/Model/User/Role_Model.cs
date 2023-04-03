using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace DOANV1.Entities.Model.User
{
    
    [CollectionName("Roles")]
    public class Role_Model : MongoIdentityRole<Guid>
    {
        public string Role { get; set; }

    }
}
