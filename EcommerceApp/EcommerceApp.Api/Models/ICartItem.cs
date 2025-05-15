namespace EcommerceApp.Api.Models;

/// <summary>
/// Representa un item en el carrito de compras
/// </summary>
public interface ICartItem
{
    /// <summary>
    /// Identificador único del producto
    /// </summary>
    /// <example>PRD-001</example>
    string ProductId { get; set; }

    /// <summary>
    /// Cantidad seleccionada del producto
    /// </summary>
    /// <example>2</example>
    int Quantity { get; set; }

    /// <summary>
    /// Precio unitario del producto
    /// </summary>
    /// <example>29.99</example>
    double Price { get; set; }
}