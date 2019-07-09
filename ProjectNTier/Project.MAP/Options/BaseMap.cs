using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public abstract class BaseMap<T> : EntityTypeConfiguration<T>  where T : BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.Status).HasColumnName("Veri Durumu");
            Property(x => x.CreatedDate).HasColumnName("Oluşturma Tarihi").HasColumnType("datetime2").IsOptional();
            Property(x => x.DeletedDate).HasColumnType("datetime2").HasColumnName("Silme Tarihi").IsOptional();
            Property(x => x.ModifiedDate). HasColumnType("datetime2").HasColumnName("Guncelleme Tarihi").IsOptional();

        }
    }
}
