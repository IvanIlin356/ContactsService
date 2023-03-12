using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace Service.Models
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; } = null!;
        public ContactsContext()
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=postgresPasword");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry entityEntry in ChangeTracker.Entries())
            {
                if (entityEntry.Entity is Contact contact)
                {
                    switch (entityEntry.State)
                    {
                        case EntityState.Added:
                            if (string.IsNullOrEmpty(contact.Displayname))
                                contact.Displayname = string.Format("{0} {1} {2}", contact.Salution, contact.Firstname, contact.Lastname);

                            contact.CreationTimestamp = DateTime.Now;
                            contact.LastChangeTimestamp = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            if (string.IsNullOrEmpty(contact.Displayname))
                                contact.Displayname = string.Format("{0} {1} {2}", contact.Salution, contact.Firstname, contact.Lastname);

                            contact.LastChangeTimestamp = DateTime.Now;

                            Contact old = this.Contacts.First(c => c.Id == contact.Id);
                            contact.CreationTimestamp = old.CreationTimestamp;

                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
