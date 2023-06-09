﻿using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp_MongoDb.Models
{
    public class UserModel : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("owner")]
        public string Owner { get; set; }

        [MapTo("name")]
        [Required]
        public string Name { get; set; }

        [MapTo("surname")]
        [Required]
        public string Surname { get; set; }

        [MapTo("telefon")]
        [Required]
        public string Telefon { get; set; }

        [MapTo("mail")]
        [Required]
        public string Mail { get; set; }

        [MapTo("_partition")]
        [Required]
        public string Partition { get; set; }
    }
}
