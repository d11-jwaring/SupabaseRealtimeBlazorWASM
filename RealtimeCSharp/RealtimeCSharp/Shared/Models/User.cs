using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace RealtimeCSharp.Shared.Models
{
    [Table("users")]
    public class User : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
