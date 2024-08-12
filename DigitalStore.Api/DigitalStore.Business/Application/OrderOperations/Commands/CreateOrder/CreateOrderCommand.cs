using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using AutoMapper;
using DigitalStore.Base;
using DigitalStore.Base.Response;
using DigitalStore.Data.Domain;
using DigitalStore.Data.UnitOfWork;
using DigitalStore.Schema;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DigitalStore.Business.Application.OrderOperations.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderRequest Request) : IRequest<ApiResponse>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISessionContext sessionContext;
        private readonly UserManager<User> userManager;


        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionContext sessionContext, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sessionContext = sessionContext;
            this.userManager = userManager;
        }

        public async Task<ApiResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = sessionContext.Session.UserId;
            var shoppingCart = await unitOfWork.ShoppingCartRepository.FirstOrDefault(sc => sc.UserId == userId, "ShoppingCartItem");

            //ÖDEME İŞLEMİ BAŞLIYOR--IYZICO

            //Yapılacak ödeme isteiğinin authorization seçenekleri için nesne yaratılıyor.
            Options options = new Options();
            options.ApiKey = "sandbox-vOA1YEy9CWxc9XssFISNhoDGEmUxnoxq";
            options.SecretKey = "dhi86r8MxktjudoLb3h9Ua6CpHXNjKNb";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            //Yapılacak ödeme isteği için nesne yaratılıyor.
            CreatePaymentRequest createPaymentRequest = new CreatePaymentRequest();
            createPaymentRequest.Locale = Locale.TR.ToString();
            createPaymentRequest.ConversationId = "DigitalStoreApp";
            createPaymentRequest.Price = shoppingCart.TotalAmount.ToString();
            createPaymentRequest.PaidPrice = shoppingCart.TotalAmount.ToString();
            createPaymentRequest.Currency = Currency.TRY.ToString();
            createPaymentRequest.Installment = 1;
            createPaymentRequest.BasketId = shoppingCart.Id.ToString();
            createPaymentRequest.PaymentChannel = PaymentChannel.WEB.ToString();
            createPaymentRequest.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            //Ödemenin yapılacağı kart için nesne yaratılıyor.
            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = request.Request.CardOwnerName;
            paymentCard.CardNumber = request.Request.CardNumber;
            paymentCard.ExpireMonth = request.Request.ExpirationMonth;
            paymentCard.ExpireYear = request.Request.ExpirationYear;
            paymentCard.Cvc = request.Request.Cvc;
            paymentCard.RegisterCard = 0;
            createPaymentRequest.PaymentCard = paymentCard;
            //Alıcı bilgileri için nesne yaratılıyor.
            Buyer buyer = new Buyer();
            buyer.Id = userId;
            buyer.Name = request.Request.FirstName;
            buyer.Surname = request.Request.LastName;
            buyer.GsmNumber = request.Request.PhoneNumber;
            buyer.Email = request.Request.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = request.Request.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = request.Request.City;
            buyer.Country = "Türkiye";
            buyer.ZipCode = "34732";
            createPaymentRequest.Buyer = buyer;
            //Adres bilgileri için nesneler yaratılıyor.
            Address shippingAddress = new Address();
            shippingAddress.ContactName = request.Request.FirstName;
            shippingAddress.City = request.Request.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = request.Request.Address;
            shippingAddress.ZipCode = "34742";
            createPaymentRequest.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = request.Request.FirstName;
            billingAddress.City = request.Request.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = request.Request.Address;
            billingAddress.ZipCode = "34742";
            createPaymentRequest.BillingAddress = billingAddress;
            // Sepet ürünleri için nesneler yaratılıyor.
            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketItem;
            foreach (var item in shoppingCart.ShoppingCartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.ProductId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = item.Product.ProductCategories.ToString();
                basketItem.Category2 = item.Product.Name;
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                float price = 0;
                var coupon = await unitOfWork.CouponRepository.FirstOrDefaultAsync(x => x.IsActive && x.Code == shoppingCart.CouponCode);

                if (coupon != null && DateTime.Now >= coupon.ValidFrom && DateTime.Now <= coupon.ValidUntil)
                {
                    price = coupon.DiscountPercentage / 100 * item.Price;
                }
                var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x=> x.Id == userId);


                basketItem.Price = price.ToString().Replace(",", ".");
                basketItems.Add(basketItem);
            }
            createPaymentRequest.BasketItems = basketItems;
            Payment payment = Payment.Create(createPaymentRequest, options);
            //Paymentın durumuna göre yapılacak işlemler
            if (payment.Status == "success")
            {
                //Eğer ödeme başarılı ise siparişi kendi veritabanımıza kayıt ediyoruz.
                Order order = new Order
                {
                    OrderNumber = payment.PaymentId,
                    UserId = userId,
                    FirstName = request.Request.FirstName,
                    LastName = request.Request.LastName,
                    Address = request.Request.Address,
                    City = request.Request.City,
                    PhoneNumber = request.Request.PhoneNumber,
                    Email = request.Request.Email,
                    Note = request.Request.Note,
                    OrderDate = DateTime.Now,
                    OrderDetails = shoppingCart.ShoppingCartItems.Select(x => new OrderDetail
                    {
                        ProductId = x.ProductId,
                        TotalPrice = x.Price,
                        Quantity = x.Quantity,

                    }).ToList()
                };

               
                await unitOfWork.OrderRepository.Insert(order);
                await unitOfWork.ShoppingCartItemRepository.Delete(shoppingCart.Id);
            }

            return new ApiResponse(payment.Status);
        }
    }
}
