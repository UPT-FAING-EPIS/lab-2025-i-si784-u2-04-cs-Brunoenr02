using EcommerceApp.Api.Models;

namespace EcommerceApp.Api.Services;
public interface IDiscountService
{
    double ApplyDiscount(IEnumerable<ICartItem> items, ICard card);
}