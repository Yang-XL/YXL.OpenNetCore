using System.Threading.Tasks;
using IdentitySite.Common;
using IdentitySite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewModels.IdentitySite.Consent;

namespace XL.Identity.WebSite.Controllers
{
    /// <summary>
    ///     This controller processes the consent UI
    /// </summary>
    [SecurityHeaders]
    public class ConsentController : Controller
    {
        private readonly ConsentService _consent;
        private readonly ILogger _logger;

        public ConsentController(ILoggerFactory loggerFactory, ConsentService consent)
        {
            _consent = consent;
            _logger = loggerFactory.CreateLogger<ConsentController>();
        }

        /// <summary>
        ///     Shows the consent screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await _consent.BuildViewModelAsync(returnUrl);
            if (vm != null)
                return View("Index", vm);

            return View("Error");
        }

        /// <summary>
        ///     Handles the consent screen postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            var result = await _consent.ProcessConsent(model);

            if (result.IsRedirect)
                return Redirect(result.RedirectUri);

            if (result.HasValidationError)
                ModelState.AddModelError("", result.ValidationError);

            if (result.ShowView)
                return View("Index", result.ViewModel);

            return View("Error");
        }
    }
}