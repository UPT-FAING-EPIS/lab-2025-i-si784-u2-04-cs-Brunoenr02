using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services;

/// <summary>
/// Servicio para gestionar envíos de productos
/// </summary>
public interface IShipmentService
{
    /// <summary>
    /// Envía los items del carrito a la dirección proporcionada
    /// </summary>
    /// <param name="info">Información de dirección de envío</param>
    /// <param name="items">Items a enviar</param>
    void Ship(IAddressInfo info, IEnumerable<ICartItem> items);
}