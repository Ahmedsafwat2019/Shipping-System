using BL.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShipment _IShipment;
        public ShipmentController(ILogger<HomeController> logger, IShipment iGenericRepository)
        {
            _logger = logger;
            _IShipment = iGenericRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated){

                return RedirectToRoute(new
                {
                    controller = "Account",
                    action = "Login",
                    returnUrl = Url.Action("Create", "Shipment")
                });
            }
            return View();
        }
        

        public async Task<IActionResult> List(int page=1)
        {
            var shipments = await _IShipment.GetShipments(page,10,true,null);
            return View(shipments);
        }

        public IActionResult Show(Guid id)
        {
            return View();
        }
        public IActionResult Edit(Guid id)
        {
            return View();
        }
        public IActionResult Delete(Guid id)
        {
            _IShipment.ChangeStatus(id,0);
            return RedirectToAction("List");
        }
    }
} 