namespace FIT5032_Ass.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<Images> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Images>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<Images>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
