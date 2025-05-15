namespace EcommerceApp.Api.Models;

/// <summary>
/// Representa la información de dirección para envíos
/// </summary>
public interface IAddressInfo
{
    /// <summary>
    /// Calle o avenida principal
    /// </summary>
    string Street { get; set; }
    
    /// <summary>
    /// Dirección específica (número, departamento, etc.)
    /// </summary>
    string Address { get; set; }
    
    /// <summary>
    /// Ciudad de destino
    /// </summary>
    string City { get; set; }
    
    /// <summary>
    /// Código postal
    /// </summary>
    string PostalCode { get; set; }
    
    /// <summary>
    /// Número de contacto para el envío
    /// </summary>
    string PhoneNumber { get; set; }
}