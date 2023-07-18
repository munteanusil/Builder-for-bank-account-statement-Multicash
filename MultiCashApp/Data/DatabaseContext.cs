using Microsoft.EntityFrameworkCore;
using MultiCashApp.Models;


namespace MultiCashApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 
        }

        public DbSet<Banks> Banks { get; set; }
        public DbSet<CompanyAccounts> CompanyAccounts { get; set; }
        public DbSet<Invoice_history> Invoices_History { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AllSupplierList> AllSuppliers { get; set; }

        public DbSet<AllInvoicesList> AllInvoices { get; set; }

        //public DbSet<Invoice> InvoiceHistory { get;set; }

        public DbSet<MultiCashApp.Models.AllSupplierList> SupplierUpload { get; set; } = default!;

        public DbSet<MultiCashApp.Models.SupplierChanges> SupplierChanges { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(r => r.Role).WithMany(u => u.Users).HasForeignKey(fk => fk.RoleId);
            });


            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
            });

        
       
        }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = MKDEV01\\MMR_DEV;Database=MultiCashAppDB;Trusted_Connection=True;TrustServerCertificate=true;");
        }


       
    
    }
}
