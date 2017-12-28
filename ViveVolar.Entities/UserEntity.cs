using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ViveVolar.Entities
{
    public class UserEntity : TableEntity
    {
        public string Name { get; set; } 

        public string Rol { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public UserEntity()
        {
            this.PartitionKey = "User";          
        }

        public UserEntity(string Email):base("User",Email){}
    }
}
