classDiagram

class IAddressInfo {
    <<interface>>
    +string Street
    +string Address
    +string City
    +string PostalCode
    +string PhoneNumber
}

class CartController {
    -ICartService _cartService
    -IPaymentService _paymentService
    -IShipmentService _shipmentService
    -IDiscountService _discountService
    +string CheckOut(ICard card, IAddressInfo addressInfo)
}

CartController --> ICartService
CartController --> IPaymentService
CartController --> IShipmentService
CartController --> IDiscountService