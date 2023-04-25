using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using WGU_Capstone_C868.Model.Interfaces;

namespace WGU_Capstone_C868.Model
{
    [Table("Address")]
    public class Address : IAddress
    {
        [PrimaryKey, AutoIncrement]
        [Column("AddressId")]
        public int AddressId { get; set; }
        [Column("StreetAddress")]
        public string StreetAddress { get; set; }
        [Column("SuiteNumber")]
        public string? SuiteNumber { get; set; }
        [Column("City")]
        public string? City { get; set; }
        [Column("State")]
        public string? State { get; set; }
        [Column("ZipCode")]
        public string? ZipCode { get; set; }
        [Column("Country")]
        public string Country { get; set; }
    }
}
