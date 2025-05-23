using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Api.Controllers;

/// <summary>
/// Controlador para manejar el proceso de checkout del carrito
/// </summary>
[ApiController]
[Route("[controller]")]
public class CartController
{
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;
    private readonly IShipmentService _shipmentService;
    private readonly IDiscountService _discountService;
    
    /// <summary>
    /// Constructor del controlador
    /// </summary>
    public CartController(
      ICartService cartService,
      IPaymentService paymentService,
      IShipmentService shipmentService,
      IDiscountService discountService) 
    {
      _cartService = cartService;
      _paymentService = paymentService;
      _shipmentService = shipmentService;
      _discountService = discountService;
    }

    /// <summary>
    /// Procesa el checkout del carrito
    /// </summary>
    /// <param name="card">Datos de la tarjeta de crédito</param>
    /// <param name="addressInfo">Información de envío</param>
    /// <returns>Resultado del proceso ("charged" o "not charged")</returns>
    [HttpPost]
    public string CheckOut(ICard card, IAddressInfo addressInfo) 
    {
        var total = _cartService.Total();
        var discount = _discountService.ApplyDiscount(_cartService.Items(), card);
        var finalAmount = total - discount;
        
        var result = _paymentService.Charge(finalAmount, card);
        if (result)
        {
            _shipmentService.Ship(addressInfo, _cartService.Items());
            return $"charged with ${discount} discount";
        }
        else {
            return "not charged";
        }
    }
}