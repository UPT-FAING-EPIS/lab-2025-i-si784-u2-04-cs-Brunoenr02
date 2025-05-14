using EcommerceApp.Api.Controllers;
using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Moq;

namespace EcommerceApp.Tests;
public class ControllerTests
{
    private CartController controller;
    private Mock<IPaymentService> paymentServiceMock;
    private Mock<ICartService> cartServiceMock;
    private Mock<IShipmentService> shipmentServiceMock;
    private Mock<IDiscountService> discountServiceMock;
    private Mock<ICard> cardMock;
    private Mock<IAddressInfo> addressInfoMock;
    private List<ICartItem> items;

    [SetUp]
    public void Setup()
    {
        cartServiceMock = new Mock<ICartService>();
        paymentServiceMock = new Mock<IPaymentService>();
        shipmentServiceMock = new Mock<IShipmentService>();
        discountServiceMock = new Mock<IDiscountService>();

        cardMock = new Mock<ICard>();
        addressInfoMock = new Mock<IAddressInfo>();

        var cartItemMock = new Mock<ICartItem>();
        cartItemMock.Setup(item => item.Price).Returns(10);

        items = new List<ICartItem>()
        {
            cartItemMock.Object
        };

        cartServiceMock.Setup(c => c.Items()).Returns(items.AsEnumerable());
        cartServiceMock.Setup(c => c.Total()).Returns(10);
        discountServiceMock.Setup(d => d.ApplyDiscount(It.IsAny<IEnumerable<ICartItem>>(), It.IsAny<ICard>())).Returns(2);

        controller = new CartController(
            cartServiceMock.Object, 
            paymentServiceMock.Object, 
            shipmentServiceMock.Object,
            discountServiceMock.Object);
    }

    [Test]
    public void ShouldReturnCharged()
    {
        string expected = "charged with $2 discount";
        paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(true);

        // act
        var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

        // assert
        shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once());
        discountServiceMock.Verify(d => d.ApplyDiscount(items.AsEnumerable(), cardMock.Object), Times.Once());
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void ShouldReturnNotCharged() 
    {
        string expected = "not charged";
        paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(false);

        // act
        var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

        // assert
        shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Never());
        discountServiceMock.Verify(d => d.ApplyDiscount(items.AsEnumerable(), cardMock.Object), Times.Once());
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    [TestCase(10, 2, 8, true, "charged with $2 discount")] // Caso 1: Descuento aplicado, pago exitoso
    [TestCase(15, 0, 15, true, "charged with $0 discount")] // Caso 2: Sin descuento, pago exitoso
    [TestCase(20, 5, 15, false, "not charged")] // Caso 3: Descuento aplicado, pago fallido
    public void ShouldHandleDifferentScenarios(
        double total, 
        double discount, 
        double finalAmount, 
        bool chargeResult, 
        string expectedResult)
    {
        // Arrange
        cartServiceMock.Setup(c => c.Total()).Returns(total);
        discountServiceMock.Setup(d => d.ApplyDiscount(It.IsAny<IEnumerable<ICartItem>>(), It.IsAny<ICard>())).Returns(discount);
        paymentServiceMock.Setup(p => p.Charge(finalAmount, cardMock.Object)).Returns(chargeResult);

        // Act
        var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
        discountServiceMock.Verify(d => d.ApplyDiscount(items.AsEnumerable(), cardMock.Object), Times.Once());
        
        if (chargeResult)
        {
            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once());
        }
        else
        {
            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Never());
        }
    }
}