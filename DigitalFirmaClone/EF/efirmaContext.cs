using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class efirmaContext : DbContext
    {
        public efirmaContext()
        {
        }

        public efirmaContext(DbContextOptions<efirmaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<sig_signature> sig_signature { get; set; }
        public virtual DbSet<sig_signature_details> sig_signature_details { get; set; }
        public virtual DbSet<sig_spectators_details> sig_spectators_details { get; set; }
        public virtual DbSet<ut_form_action> ut_form_action { get; set; }
        public virtual DbSet<ut_user> ut_user { get; set; }
        public virtual DbSet<ut_user_role> ut_user_role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=116.202.175.92;user=sameer;database=efirma;password=19972015AH;port=3306;SSL Mode=None");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sig_signature>(entity =>
            {
                entity.HasKey(e => e.signature_id)
                    .HasName("PRIMARY");

                entity.Property(e => e.signature_id).HasColumnType("int(11)");

                entity.Property(e => e.approved_by).HasColumnType("int(255)");

                entity.Property(e => e.created_by).HasColumnType("int(11)");

                entity.Property(e => e.document_name).HasMaxLength(255);

                entity.Property(e => e.is_active).HasColumnType("int(11)");

                entity.Property(e => e.is_approved).HasColumnType("int(11)");

                entity.Property(e => e.is_deleted).HasColumnType("int(11)");

                entity.Property(e => e.is_locked).HasColumnType("int(11)");

                entity.Property(e => e.mifiel_id)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.modified_by).HasColumnType("int(11)");

                entity.Property(e => e.remarks).HasMaxLength(255);

                entity.Property(e => e.user_id).HasColumnType("int(11)");
            });

            modelBuilder.Entity<sig_signature_details>(entity =>
            {
                entity.HasKey(e => e.signature_details_id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.signature_id)
                    .HasName("sig_signature_details_ibfk_1");

                entity.Property(e => e.signature_details_id).HasColumnType("int(11)");

                entity.Property(e => e.approved_by).HasColumnType("int(11)");

                entity.Property(e => e.created_by).HasColumnType("int(11)");

                entity.Property(e => e.email).HasMaxLength(255);

                entity.Property(e => e.is_active).HasColumnType("int(11)");

                entity.Property(e => e.is_approved).HasColumnType("int(11)");

                entity.Property(e => e.is_deleted).HasColumnType("int(11)");

                entity.Property(e => e.is_locked).HasColumnType("int(11)");

                entity.Property(e => e.modified_by).HasColumnType("int(11)");

                entity.Property(e => e.name).HasMaxLength(255);

                entity.Property(e => e.remarks).HasMaxLength(255);

                entity.Property(e => e.rfc).HasMaxLength(255);

                entity.Property(e => e.signature_id).HasColumnType("int(11)");

                entity.Property(e => e.widget_id).HasMaxLength(255);

                entity.HasOne(d => d.signature_)
                    .WithMany(p => p.sig_signature_details)
                    .HasForeignKey(d => d.signature_id)
                    .HasConstraintName("sig_signature_details_ibfk_1");
            });

            modelBuilder.Entity<sig_spectators_details>(entity =>
            {
                entity.HasKey(e => e.spectators_id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.signature_id)
                    .HasName("sig_spectators_details_ibfk_1");

                entity.Property(e => e.spectators_id).HasColumnType("int(11)");

                entity.Property(e => e.approved_by).HasColumnType("int(11)");

                entity.Property(e => e.created_by).HasColumnType("int(11)");

                entity.Property(e => e.email).HasMaxLength(255);

                entity.Property(e => e.is_active).HasColumnType("int(11)");

                entity.Property(e => e.is_approved).HasColumnType("int(11)");

                entity.Property(e => e.is_deleted).HasColumnType("int(11)");

                entity.Property(e => e.is_locked).HasColumnType("int(11)");

                entity.Property(e => e.modified_by).HasColumnType("int(11)");

                entity.Property(e => e.name).HasMaxLength(255);

                entity.Property(e => e.remarks).HasMaxLength(255);

                entity.Property(e => e.signature_id).HasColumnType("int(11)");

                entity.HasOne(d => d.signature_)
                    .WithMany(p => p.sig_spectators_details)
                    .HasForeignKey(d => d.signature_id)
                    .HasConstraintName("sig_spectators_details_ibfk_1");
            });

            modelBuilder.Entity<ut_form_action>(entity =>
            {
                entity.HasKey(e => e.action_id)
                    .HasName("PRIMARY");

                entity.Property(e => e.action_id).HasColumnType("int(11)");

                entity.Property(e => e.action_name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.display_name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.form_code_id).HasColumnType("int(11)");

                entity.Property(e => e.import_action_id)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.is_active).HasColumnType("int(11)");

                entity.Property(e => e.is_approved).HasColumnType("int(11)");

                entity.Property(e => e.is_locked).HasColumnType("int(11)");

                entity.Property(e => e.remarks)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ut_user>(entity =>
            {
                entity.HasKey(e => e.user_id)
                    .HasName("PRIMARY");

                entity.Property(e => e.user_id).HasColumnType("int(11)");

                entity.Property(e => e.created_by).HasColumnType("int(11)");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.import_user_id)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.is_active).HasColumnType("int(11)");

                entity.Property(e => e.is_approved).HasColumnType("int(11)");

                entity.Property(e => e.is_deleted).HasColumnType("int(11)");

                entity.Property(e => e.is_locked).HasColumnType("int(11)");

                entity.Property(e => e.mobile)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.modified_by).HasColumnType("int(11)");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.remarks)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.show_password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.user_name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ut_user_role>(entity =>
            {
                entity.HasKey(e => e.user_role_id)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.action_id)
                    .HasName("ut_user_role_ibfk_1");

                entity.HasIndex(e => e.user_id)
                    .HasName("ut_user_role_ibfk_2");

                entity.Property(e => e.user_role_id).HasColumnType("int(11)");

                entity.Property(e => e.action_id).HasColumnType("int(11)");

                entity.Property(e => e.created_by).HasColumnType("int(11)");

                entity.Property(e => e.import_user_role_id)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.is_active).HasColumnType("int(11)");

                entity.Property(e => e.is_approved).HasColumnType("int(11)");

                entity.Property(e => e.is_deleted).HasColumnType("int(11)");

                entity.Property(e => e.is_locked).HasColumnType("int(11)");

                entity.Property(e => e.modified_by).HasColumnType("int(11)");

                entity.Property(e => e.remarks)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.user_id).HasColumnType("int(11)");

                entity.HasOne(d => d.action_)
                    .WithMany(p => p.ut_user_role)
                    .HasForeignKey(d => d.action_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ut_user_role_ibfk_1");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.ut_user_role)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ut_user_role_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
