using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;

public class StripeOptions
{
    public string option { get; set; }
}

namespace server.Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
              .UseUrls("http://0.0.0.0:4242")
              .UseWebRoot("public")
              .UseStartup<Startup>()
              .Build()
              .Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // This test secret API key is a placeholder. Don't include personal details in requests with this key.
            // To see your test secret API key embedded in code samples, sign in to your Stripe account.
            // You can also find your test secret API key at https://dashboard.stripe.com/test/apikeys.

            StripeConfiguration.ApiKey = "sk_test_51PiEUvRpUc8TzDYkK5G8l1eL0Dob5qZjYZA0ymzJnazn5GjZ99GVKkeN3veSLc8XHATHqzTG5mKGY1FHlqD7wyJ600C2EuH3vU";

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }

    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        [HttpPost]
        public ActionResult Create()
        {
            var domain = "http://localhost:3000";
            var options = new SessionCreateOptions
            {
                UiMode = "embedded",
                //PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1Pw2fjRpUc8TzDYkrQ5vN1e2",
                    Quantity = 1,
                  },
                },
                Mode = "subscription", //"payment",
                ReturnUrl = domain + "/return?session_id={CHECKOUT_SESSION_ID}",
                Customer = "cus_QfjiPhoEjqVkfK", //Org 138-2
                ClientReferenceId = "cus_QfjiPhoEjqVkfK",
               // Currency = "usd"
               // PaymentIntentData
               //PaymentMethodConfiguration
               
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new {clientSecret = session.RawJObject["client_secret"]});
        }
    }

    [Route("session-status")]
    [ApiController]
    public class SessionStatusController : Controller
    {
        [HttpGet]
        public ActionResult SessionStatus([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            return Json(new {status = session.RawJObject["status"],  customer_email = session.RawJObject["customer_details"]["email"]});
        }
    }
}