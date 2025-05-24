using System.Collections.Generic;
using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services;

/// <summary>
/// Servicio para manejar operaciones del carrito de compras
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Calcula el total de la compra
    /// </summary>
    /// <returns>Suma total de los items en el carrito</returns>
    double Total();
    
    /// <summary>
    /// Obtiene todos los items del carrito
    /// </summary>
    /// <returns>Enumerable con los productos en el carrito</returns>
    IEnumerable<ICartItem> Items();
}