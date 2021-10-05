using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DigitalFirmaClone.EF
{
    public partial class ecommerceContext : DbContext
    {
        public ecommerceContext()
        {
        }

        public ecommerceContext(DbContextOptions<ecommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<inv_product> inv_product { get; set; }
        public virtual DbSet<inv_product_brands> inv_product_brands { get; set; }
        public virtual DbSet<inv_product_variant> inv_product_variant { get; set; }
        public virtual DbSet<inv_ut_category> inv_ut_category { get; set; }
        public virtual DbSet<inv_ut_unit_of_measure> inv_ut_unit_of_measure { get; set; }
        public virtual DbSet<sdfghjk> sdfghjk { get; set; }
        public virtual DbSet<sig_signature> sig_signature { get; set; }
        public virtual DbSet<sig_signature_details> sig_signature_details { get; set; }
        public virtual DbSet<sig_spectators_details> sig_spectators_details { get; set; }
        public virtual DbSet<ut_company> ut_company { get; set; }
        public virtual DbSet<ut_form_action> ut_form_action { get; set; }
        public virtual DbSet<ut_user> ut_user { get; set; }
        public virtual DbSet<ut_user_role> ut_user_role { get; set; }
        public virtual DbSet<view_inv_stock_management> view_inv_stock_management { get; set; }
        public virtual DbSet<view_inv_stock_management_transations> view_inv_stock_management_transations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=./;Database=e-commerce;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<inv_product>(entity =>
            {
                entity.HasKey(e => e.product_id)
                    .HasName("PK_product");

                entity.Property(e => e.approved_date).HasColumnType("datetime");

                entity.Property(e => e.category_id).HasDefaultValueSql("((0))");

                entity.Property(e => e.company_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.cost_price)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.has_variants).HasDefaultValueSql("((0))");

                entity.Property(e => e.import_product_id).HasMaxLength(250);

                entity.Property(e => e.is_variant).HasDefaultValueSql("((0))");

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.product_name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.product_variant_id)
                    .HasDefaultValueSql("((1))")
                    .HasComment("if variant, then id of that varaint else nothing");

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.Property(e => e.selling_price)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.category_)
                    .WithMany(p => p.inv_product)
                    .HasForeignKey(d => d.category_id)
                    .HasConstraintName("FK__product__categor__6BE40491");

                entity.HasOne(d => d.company_)
                    .WithMany(p => p.inv_product)
                    .HasForeignKey(d => d.company_id)
                    .HasConstraintName("FK__inv_produ__compa__2CF2ADDF");

                entity.HasOne(d => d.product_brand_)
                    .WithMany(p => p.inv_product)
                    .HasForeignKey(d => d.product_brand_id)
                    .HasConstraintName("FK__inv_produ__produ__756D6ECB");

                entity.HasOne(d => d.product_variant_)
                    .WithMany(p => p.inv_product)
                    .HasForeignKey(d => d.product_variant_id)
                    .HasConstraintName("FK__inv_produ__produ__511AFFBC");

                entity.HasOne(d => d.unit_of_measure_)
                    .WithMany(p => p.inv_product)
                    .HasForeignKey(d => d.unit_of_measure_id)
                    .HasConstraintName("FK__product__unit_of__6AEFE058");
            });

            modelBuilder.Entity<inv_product_brands>(entity =>
            {
                entity.HasKey(e => e.product_brand_id)
                    .HasName("unit_of_measure_id_copy1");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.import_product_brand_id).HasMaxLength(250);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.product_brand_name).HasMaxLength(250);

                entity.Property(e => e.remarks).HasMaxLength(800);
            });

            modelBuilder.Entity<inv_product_variant>(entity =>
            {
                entity.HasKey(e => e.product_variant_id)
                    .HasName("PK_user_type_copy1_copy1_copy2_copy2_copy3_copy1_copy2");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.import_product_variant_id).HasMaxLength(250);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.product_id)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Main product id (non variant product)");

                entity.Property(e => e.product_variant_name).HasMaxLength(255);

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.HasOne(d => d.product_)
                    .WithMany(p => p.inv_product_variant)
                    .HasForeignKey(d => d.product_id)
                    .HasConstraintName("FK__inv_produ__produ__2042BE37");
            });

            modelBuilder.Entity<inv_ut_category>(entity =>
            {
                entity.HasKey(e => e.category_id)
                    .HasName("PK_category");

                entity.Property(e => e.category_name).HasMaxLength(250);

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.import_category_id).HasMaxLength(250);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.remarks).HasMaxLength(800);
            });

            modelBuilder.Entity<inv_ut_unit_of_measure>(entity =>
            {
                entity.HasKey(e => e.unit_of_measure_id)
                    .HasName("unit_of_measure_id");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.import_unit_of_measure_id).HasMaxLength(250);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.Property(e => e.unit_of_measure_name).HasMaxLength(250);
            });

            modelBuilder.Entity<sdfghjk>(entity =>
            {
                entity.HasKey(e => e.form_code_id)
                    .HasName("PK__ut_form___B943269AA9F9ABAE");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.modified_date).HasColumnType("datetime");
            });

            modelBuilder.Entity<sig_signature>(entity =>
            {
                entity.HasKey(e => e.signature_id)
                    .HasName("PK_user_copy1_copy4");

                entity.Property(e => e.approved_by).HasDefaultValueSql("((0))");

                entity.Property(e => e.approved_date).HasColumnType("datetime");

                entity.Property(e => e.auth).HasColumnType("text");

                entity.Property(e => e.company_id).HasMaxLength(255);

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.document_name).HasMaxLength(255);

                entity.Property(e => e.document_string).HasColumnType("text");

                entity.Property(e => e.mode_logo).HasMaxLength(255);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.paypal_id).HasMaxLength(255);

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.Property(e => e.remember_at).HasMaxLength(255);

                entity.Property(e => e.sign_mode).HasMaxLength(255);

                entity.Property(e => e.sign_ordered).HasMaxLength(255);

                entity.Property(e => e.sign_position).HasMaxLength(255);

                entity.Property(e => e.tries).HasMaxLength(255);
            });

            modelBuilder.Entity<sig_signature_details>(entity =>
            {
                entity.HasKey(e => e.signature_details_id)
                    .HasName("PK_user_copy1_copy4_copy1");

                entity.Property(e => e.approved_by).HasDefaultValueSql("((0))");

                entity.Property(e => e.approved_date).HasColumnType("datetime");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.email).HasMaxLength(255);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.name).HasMaxLength(255);

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.Property(e => e.rfc).HasMaxLength(255);

                entity.HasOne(d => d.signature_)
                    .WithMany(p => p.sig_signature_details)
                    .HasForeignKey(d => d.signature_id)
                    .HasConstraintName("FK__sig_signa__signa__16CE6296");
            });

            modelBuilder.Entity<sig_spectators_details>(entity =>
            {
                entity.HasKey(e => e.spectators_id)
                    .HasName("PK_user_copy1_copy4_copy1_copy1");

                entity.Property(e => e.approved_by).HasDefaultValueSql("((0))");

                entity.Property(e => e.approved_date).HasColumnType("datetime");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.email).HasMaxLength(255);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.name).HasMaxLength(255);

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.HasOne(d => d.signature_)
                    .WithMany(p => p.sig_spectators_details)
                    .HasForeignKey(d => d.signature_id)
                    .HasConstraintName("FK__sig_signa__signa__16CE6297");
            });

            modelBuilder.Entity<ut_company>(entity =>
            {
                entity.HasKey(e => e.company_id)
                    .HasName("PK_user_copy1_copy1");

                entity.Property(e => e.address).HasMaxLength(100);

                entity.Property(e => e.company_name).HasMaxLength(250);

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.email).HasMaxLength(50);

                entity.Property(e => e.fax).HasMaxLength(50);

                entity.Property(e => e.import_company_id).HasMaxLength(250);

                entity.Property(e => e.mobile).HasMaxLength(50);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.ntn_no).HasMaxLength(50);

                entity.Property(e => e.remarks).HasMaxLength(800);

                entity.Property(e => e.sales_tax_no).HasMaxLength(50);

                entity.Property(e => e.short_name).HasMaxLength(50);

                entity.Property(e => e.website).HasMaxLength(50);
            });

            modelBuilder.Entity<ut_form_action>(entity =>
            {
                entity.HasKey(e => e.action_id)
                    .HasName("PK_action");

                entity.Property(e => e.action_name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.display_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.form_code_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.import_action_id)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.remarks)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.HasOne(d => d.form_code_)
                    .WithMany(p => p.ut_form_action)
                    .HasForeignKey(d => d.form_code_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ut_form_a__form___0B7CAB7B");
            });

            modelBuilder.Entity<ut_user>(entity =>
            {
                entity.HasKey(e => e.user_id)
                    .HasName("PK_user");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.import_user_id)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.mobile)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.remarks)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.show_password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.user_name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ut_user_role>(entity =>
            {
                entity.HasKey(e => e.user_role_id)
                    .HasName("PK_user_role");

                entity.Property(e => e.created_date).HasColumnType("datetime");

                entity.Property(e => e.import_user_role_id)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.modified_date).HasColumnType("datetime");

                entity.Property(e => e.remarks)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.HasOne(d => d.action_)
                    .WithMany(p => p.ut_user_role)
                    .HasForeignKey(d => d.action_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_role__actio__3B75D760");

                entity.HasOne(d => d.user_)
                    .WithMany(p => p.ut_user_role)
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_role__user___3C69FB99");
            });

            modelBuilder.Entity<view_inv_stock_management>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_inv_stock_management");

                entity.Property(e => e.product_name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.quantity).HasColumnType("decimal(38, 6)");
            });

            modelBuilder.Entity<view_inv_stock_management_transations>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_inv_stock_management_transations");

                entity.Property(e => e.amount).HasColumnType("decimal(24, 6)");

                entity.Property(e => e.currency_name).HasMaxLength(100);

                entity.Property(e => e.date).HasColumnType("datetime");

                entity.Property(e => e.exchange_rate).HasColumnType("decimal(24, 6)");

                entity.Property(e => e.is_grn)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.product_name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.quantity).HasColumnType("decimal(24, 6)");

                entity.Property(e => e.unit_of_measure_name).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
