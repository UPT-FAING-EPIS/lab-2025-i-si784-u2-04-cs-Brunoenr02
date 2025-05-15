using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services;

/// <summary>
/// Servicio para calcular descuentos
/// </summary>
public interface IDiscountService
{
    /// <summary>
    /// Calcula el descuento aplicable a una compra
    /// </summary>
    /// <param name="items">Items en el carrito</param>
    /// <param name="card">Tarjeta usada (para descuentos especiales)</param>
    /// <returns>Monto del descuento</returns>
    double ApplyDiscount(IEnumerable<ICartItem> items, ICard card);
}