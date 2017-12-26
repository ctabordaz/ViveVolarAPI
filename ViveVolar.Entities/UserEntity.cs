using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ViveVolar.Entities
{
    public class UserEntity : TableEntity
    {
        public string Name { get; set; } 

        public string Rol { get; set; }

        //[IgnoreProperty]
        //public string Email
        //{
        //    get { return this.RowKey; }
        //    set { this.RowKey = value; }
        //}

        public UserEntity()
        {
            this.PartitionKey = "User";          
        }

        public UserEntity(string Email):base("User",Email){}
    }
}
