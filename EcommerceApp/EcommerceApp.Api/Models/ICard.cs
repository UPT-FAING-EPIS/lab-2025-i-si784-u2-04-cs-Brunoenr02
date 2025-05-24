using System;

namespace EcommerceApp.Api.Models;

/// <summary>
/// Representa la información de una tarjeta de crédito/débito
/// </summary>
public interface ICard
{
    /// <summary>
    /// Número de la tarjeta (16 dígitos)
    /// </summary>
    /// <example>4111111111111111</example>
    string CardNumber { get; set; }

    /// <summary>
    /// Nombre del titular como aparece en la tarjeta
    /// </summary>
    /// <example>John Doe</example>
    string Name { get; set; }

    /// <summary>
    /// Fecha de vencimiento de la tarjeta
    /// </summary>
    /// <example>2025-12-31</example>
    DateTime ValidTo { get; set; }
}