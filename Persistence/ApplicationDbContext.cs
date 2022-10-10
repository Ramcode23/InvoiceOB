using Domain.Entities;
using Domain.Indentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    )
        : base(options)
    {

    }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Database schema
        builder.HasDefaultSchema("Invoicement");

        // Model Contraints
        ModelConfig(builder);
    }
    private void ModelConfig(ModelBuilder modelBuilder)
    {
        new ApplicationUserConfiguration(modelBuilder.Entity<ApplicationUser>());
        new ApplicationRoleConfiguration(modelBuilder.Entity<ApplicationRole>());
    }
}