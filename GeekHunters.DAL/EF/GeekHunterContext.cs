using GeekHunters.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekHunters.DAL.EF
{
    public class GeekHunterContext : DbContext
    {
        public GeekHunterContext()
        {
        }

        public GeekHunterContext(DbContextOptions<GeekHunterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<CandidateSkill> CandidateSkills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CandidateSkill>()
                .HasKey(t => new { t.CandidateId, t.SkillId });

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(pc => pc.Candidate)
                .WithMany(p => p.CandidateSkills)
                .HasForeignKey(pc => pc.CandidateId);

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(pc => pc.Skill)
                .WithMany(c => c.CandidateSkills)
                .HasForeignKey(pc => pc.SkillId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
