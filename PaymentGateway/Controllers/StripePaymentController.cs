using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.FinancialConnections;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripePaymentController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration=configuration;

        [Route("Success")]
        [HttpGet]
        public string Success()
        {
            return "Pyament Done Successfully!!";
        }

        [Route("Error")]
        [HttpGet]
        public string Error()
        {
            return "Something Went Wrong!!";
        }

        [Route("MakePayment")]
        [HttpPost]
        public IActionResult MakePayment(string amount)
        {
            var currency = "usd";
            var successUrl = "https://localhost:7147/Success";
            var ErrorUrl = "https://localhost:7147/Error";

            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var option = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(amount)*100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Test Name",
                                Description ="Test Desc"
                            }
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = ErrorUrl
            };

            var service = new SessionService();
            var session = service.Create(option);
            var sessionId = session.Id;


            return Redirect(session.Url);
        }
    }
}
