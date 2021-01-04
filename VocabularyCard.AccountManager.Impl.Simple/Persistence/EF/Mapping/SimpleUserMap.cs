using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.AccountManager.Impl.Simple.Domain;

namespace VocabularyCard.AccountManager.Impl.Simple.Persistence.EF.Mapping
{
    public class SimpleUserMap : EntityTypeConfiguration<SimpleUser>
    {
        public SimpleUserMap()
        {
            this.ToTable("SIMPLE_USER");
            this.HasKey(t => t.UserId);
            this.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("USER_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(c => c.Flag).IsRequired().HasColumnName("FLAG").IsOptional();
            this.Property(c => c.LoginId).IsRequired().HasColumnName("LOGIN_ID").HasMaxLength(255).IsOptional();
            this.Property(c => c.Password).IsRequired().HasColumnName("PASSWORD").HasMaxLength(255).IsOptional();
            this.Property(c => c.DisplayName).IsRequired().HasColumnName("DISPLAYNAME").HasMaxLength(255).IsOptional();
            this.Property(c => c.Email).IsRequired().HasColumnName("EMAIL").HasMaxLength(255).IsOptional();
        }
    }
}
