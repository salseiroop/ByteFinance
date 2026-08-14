using ByteFinance.Application.DTOs;
using FluentValidation;

namespace ByteFinance.Application.Validators;

public class TransacaoValidator : AbstractValidator<TransacaoRequestDTO>
{
    public TransacaoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MaximumLength(100).WithMessage("A descrição deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

        RuleFor(x => x.Data)
            .NotEqual(default(DateTime)).WithMessage("A data informada é inválida.");

        RuleFor(x => x.CategoriaId)
            .NotEqual(Guid.Empty).WithMessage("A categoria informada é inválida.");
    }
}