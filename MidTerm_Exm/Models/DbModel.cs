using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MidTerm_Exm.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
    }
    public class Country:EntityBase
    {
        
        [Required, StringLength(40)]
        public string CountryName { get; set; }
        [Required, StringLength(30)]
        public string Capital { get; set; }
        [Required, StringLength(50)]
        public string Currency { get; set; }
        [Required, StringLength(50)]
        public string Symbol { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }
        public bool IsDeveloped { get; set; }
        public virtual ICollection<University> Universities { get; set; } = new List<University>();
    }
    public class University:EntityBase
    {
        
        [Required, StringLength(30)]
        public string UniversityName { get; set; }
        [Required, StringLength(40)]
        public string AdmissionRequirment { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime EstublishDate { get; set; }

        [Required, Column(TypeName = "money")]
        public int PayingCost { get; set; }
        [Required, StringLength(30)]
        public string Ranking { get; set; }
        [Required, ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

    }
    public class CountryDbContext : DbContext
    {
        public CountryDbContext()
        {
          Configuration.LazyLoadingEnabled= false;   
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<University> Universities { get; set; }
    }


}