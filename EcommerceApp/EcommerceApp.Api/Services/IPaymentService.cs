using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services;

/// <summary>
/// Servicio para procesar pagos con tarjeta
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Intenta cobrar el monto total a la tarjeta proporcionada
    /// </summary>
    /// <param name="total">Monto total a cobrar</param>
    /// <param name="card">Datos de la tarjeta</param>
    /// <returns>True si el pago fue exitoso</returns>
    bool Charge(double total, ICard card);
}