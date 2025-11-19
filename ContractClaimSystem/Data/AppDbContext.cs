using Microsoft.EntityFrameworkCore;
using ContractClaimSystem.Models;

namespace ContractClaimSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Claim entity
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount)
                      .HasPrecision(18, 2); // Use HasPrecision instead of HasColumnType for EF Core 8
                entity.Property(e => e.LecturerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LecturerEmail).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ReviewerNotes).HasMaxLength(1000);

                // Configure enum conversion
                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20);
            });

            // Seed initial data
            modelBuilder.Entity<Claim>().HasData(
                new Claim
                {
                    Id = 1,
                    LecturerName = "Dr. Sarah Johnson",
                    LecturerEmail = "sarah.johnson@university.edu",
                    ClaimMonth = new DateTime(2024, 1, 1),
                    Amount = 2500.00m,
                    Description = "Monthly teaching services for January 2024 - Advanced Computer Science Course",
                    Status = ClaimStatus.Approved,
                    SubmittedDate = new DateTime(2024, 1, 5),
                    ReviewedDate = new DateTime(2024, 1, 8),
                    ApprovedDate = new DateTime(2024, 1, 10),
                    ReviewerNotes = "All documentation verified and approved."
                },
                new Claim
                {
                    Id = 2,
                    LecturerName = "Prof. Michael Chen",
                    LecturerEmail = "michael.chen@university.edu",
                    ClaimMonth = new DateTime(2024, 1, 1),
                    Amount = 2800.00m,
                    Description = "Monthly teaching services for January 2024 - Data Structures and Algorithms",
                    Status = ClaimStatus.UnderReview,
                    SubmittedDate = new DateTime(2024, 1, 6),
                    ReviewedDate = new DateTime(2024, 1, 9),
                    ReviewerNotes = "Awaiting additional documentation for verification."
                },
                new Claim
                {
                    Id = 3,
                    LecturerName = "Dr. Emily Watson",
                    LecturerEmail = "emily.watson@university.edu",
                    ClaimMonth = new DateTime(2024, 2, 1),
                    Amount = 2300.00m,
                    Description = "Monthly teaching services for February 2024 - Software Engineering Principles",
                    Status = ClaimStatus.Pending,
                    SubmittedDate = new DateTime(2024, 2, 3)
                },
                new Claim
                {
                    Id = 4,
                    LecturerName = "Dr. Robert Kim",
                    LecturerEmail = "robert.kim@university.edu",
                    ClaimMonth = new DateTime(2024, 1, 1),
                    Amount = 1950.00m,
                    Description = "Monthly teaching services for January 2024 - Database Management Systems",
                    Status = ClaimStatus.Rejected,
                    SubmittedDate = new DateTime(2024, 1, 7),
                    ReviewedDate = new DateTime(2024, 1, 10),
                    ReviewerNotes = "Incomplete timesheet submission. Please resubmit with proper documentation."
                }
            );
        }
    }
}