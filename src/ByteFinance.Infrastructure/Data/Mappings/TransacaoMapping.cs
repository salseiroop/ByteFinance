using ByteFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteFinance.Infrastructure.Data.Mappings;

public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Valor)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.Data)
            .IsRequired();

        builder.Property(t => t.Tipo)
            .IsRequired();

        builder.HasOne(t => t.Categoria)
            .WithMany()
            .HasForeignKey(t => t.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Transacoes");
    }
}