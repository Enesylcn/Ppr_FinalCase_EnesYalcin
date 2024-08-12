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

        public async Task<ApiResponse> Handle(CreateOrderCommand requests, CancellationToken cancellationToken)
        {
            var userId = sessionContext.Session.UserId;
            var userAd = sessionContext.Session.UserName;
            var shoppingCart = await unitOfWork.ShoppingCartRepository.FirstOrDefaultAsync(sc => sc.UserId == userId, "ShoppingCartItems");

            //ÖDEME İŞLEMİ BAŞLIYOR--IYZICO

            ////Yapılacak ödeme isteiğinin authorization seçenekleri için nesne yaratılıyor.
            //Options options = new Options();
            //options.ApiKey = "sandbox-hCO18sVwh1CYAUaDLfxirivJmBEX6U6O";
            //options.SecretKey = "sandbox-hgvnGh09aHYvCAYYQsYhgZZOwlwb4bFL";
            //options.BaseUrl = "https://sandbox-api.iyzipay.com";
            ////Yapılacak ödeme isteği için nesne yaratılıyor.
            //CreatePaymentRequest createPaymentRequest = new CreatePaymentRequest();
            //createPaymentRequest.Locale = Locale.TR.ToString();
            //createPaymentRequest.ConversationId = "FS-2310-13-MiniShopApp";
            //createPaymentRequest.Price = shoppingCart.TotalAmount.ToString();
            //createPaymentRequest.PaidPrice = shoppingCart.TotalAmount.ToString();
            //createPaymentRequest.Currency = Currency.TRY.ToString();
            //createPaymentRequest.Installment = 1;
            //createPaymentRequest.BasketId = shoppingCart.Id.ToString();
            //createPaymentRequest.PaymentChannel = PaymentChannel.WEB.ToString();
            //createPaymentRequest.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            ////Ödemenin yapılacağı kart için nesne yaratılıyor.
            //PaymentCard paymentCard = new PaymentCard();
            //paymentCard.CardHolderName = request.Request.CardOwnerName;
            //paymentCard.CardNumber = request.Request.CardNumber;
            //paymentCard.ExpireMonth = request.Request.ExpirationMonth;
            //paymentCard.ExpireYear = request.Request.ExpirationYear;
            //paymentCard.Cvc = request.Request.Cvc;
            //paymentCard.RegisterCard = 0;
            //createPaymentRequest.PaymentCard = paymentCard;
            ////Alıcı bilgileri için nesne yaratılıyor.
            //Buyer buyer = new Buyer();
            //buyer.Id = userId;
            //buyer.Name = request.Request.FirstName;
            //buyer.Surname = request.Request.LastName;
            //buyer.GsmNumber = request.Request.PhoneNumber;
            //buyer.Email = request.Request.Email;
            //buyer.IdentityNumber = "74300864791";
            //buyer.LastLoginDate = "2015-10-05 12:43:35";
            //buyer.RegistrationDate = "2013-04-21 15:12:09";
            //buyer.RegistrationAddress = request.Request.Address;
            //buyer.Ip = "85.34.78.112";
            //buyer.City = request.Request.City;
            //buyer.Country = "Türkiye";
            //buyer.ZipCode = "34732";
            //createPaymentRequest.Buyer = buyer;
            ////Adres bilgileri için nesneler yaratılıyor.
            //Address shippingAddress = new Address();
            //shippingAddress.ContactName = request.Request.FirstName;
            //shippingAddress.City = request.Request.City;
            //shippingAddress.Country = "Turkey";
            //shippingAddress.Description = request.Request.Address;
            //shippingAddress.ZipCode = "34742";
            //createPaymentRequest.ShippingAddress = shippingAddress;

            //Address billingAddress = new Address();
            //billingAddress.ContactName = request.Request.FirstName;
            //billingAddress.City = request.Request.City;
            //billingAddress.Country = "Turkey";
            //billingAddress.Description = request.Request.Address;
            //billingAddress.ZipCode = "34742";
            //createPaymentRequest.BillingAddress = billingAddress;
            //// Sepet ürünleri için nesneler yaratılıyor.
            //List<BasketItem> basketItems = new List<BasketItem>();
            //BasketItem basketItem;
            //foreach (var item in shoppingCart.ShoppingCartItems)
            //{
            //    basketItem = new BasketItem();
            //    basketItem.Id = item.ProductId.ToString();
            //    basketItem.Name = item.Name;
            //    basketItem.Category1 = "Elektronik";
            //    basketItem.Category2 = "Telefon";
            //    basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            //    float price = 0;
            //    var coupon = await unitOfWork.CouponRepository.FirstOrDefaultAsync(x => x.IsActive && x.Code == shoppingCart.CouponCode);

            //    if (coupon != null && DateTime.Now >= coupon.ValidFrom && DateTime.Now <= coupon.ValidUntil)
            //    {
            //        price = coupon.DiscountPercentage / 100 * item.Price;
            //    }
            //    var user = await userManager.FindByNameAsync(userAd);
            //    //var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x=> x.Id == userId);


            //    basketItem.Price = price.ToString().Replace(",", ".");
            //    basketItems.Add(basketItem);
            //}
            //createPaymentRequest.BasketItems = basketItems;
            //Payment payment = Payment.Create(createPaymentRequest, options);


            Options options = new Options();
            options.ApiKey = "sandbox-hCO18sVwh1CYAUaDLfxirivJmBEX6U6O";
            options.SecretKey = "sandbox-hgvnGh09aHYvCAYYQsYhgZZOwlwb4bFL";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "Enes";
            buyer.Surname = "Yalçın";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);

            //// Sepet ürünleri için nesneler yaratılıyor.
            //List<BasketItem> basketItems = new List<BasketItem>();
            //BasketItem basketItem;
            //foreach (var item in shoppingCart.ShoppingCartItems)
            //{
            //    basketItem = new BasketItem();
            //    basketItem.Id = item.ProductId.ToString();
            //    basketItem.Name = $"{item.Name}";
            //    basketItem.Category1 = "Elektronik";
            //    basketItem.Category2 = "Telefon";
            //    basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            //    float price = 0;
            //    var coupon = await unitOfWork.CouponRepository.FirstOrDefaultAsync(x => x.IsActive && x.Code == shoppingCart.CouponCode);

            //    if (coupon != null && DateTime.Now >= coupon.ValidFrom && DateTime.Now <= coupon.ValidUntil)
            //    {
            //        price = coupon.DiscountPercentage / 100 * item.Price;
            //    }
            //    else 
            //    {
            //        price = item.Price;
            //    }
            //    var user = await userManager.FindByNameAsync(userAd);
            //    //var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x=> x.Id == userId);


            //    basketItem.Price = price.ToString();
            //    basketItems.Add(basketItem);
            //}
            //request.BasketItems = basketItems;
            //Payment payment = Payment.Create(request, options);







            //Paymentın durumuna göre yapılacak işlemler
            if (payment.Status == "success")
            {
                //Eğer ödeme başarılı ise siparişi kendi veritabanımıza kayıt ediyoruz.
                Order order = new Order
                {
                    OrderNumber = payment.PaymentId,
                    UserId = userId,
                    FirstName = requests.Request.FirstName,
                    LastName = requests.Request.LastName,
                    Address = requests.Request.Address,
                    City = requests.Request.City,
                    PhoneNumber = requests.Request.PhoneNumber,
                    Email = requests.Request.Email,
                    Note = requests.Request.Note,
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
