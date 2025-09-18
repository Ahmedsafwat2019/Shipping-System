using BL.Contract;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.admin.Controllers
{

    [Area("admin")]
    public class ShipmentController : Controller
    {
        private readonly IShipment _IShipment;
        public ShipmentController(IShipment shipment)
        {
            _IShipment = shipment;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task< IActionResult> Edit(Guid id)
        {
            var shipment = await _IShipment.GetById(id);
            return View(shipment);
        }
        public async Task<IActionResult> List(int page = 1)
        {
            //int? ShipmentStatus = null;
            int? status = null;

            if (User.IsInRole("Reviewer"))
            {
                // الشحنة لسه Created، والمراجع هيغيرها لـ Reviewed
                status = (int)ShipmentStatus.Created;
            }
            else if (User.IsInRole("Operation"))
            {
                // الشحنة Reviewed، والـ Operation هيغيرها لـ Approved
                status = (int)ShipmentStatus.Approved;
            }
            else if (User.IsInRole("OperationManager"))
            {
                // الشحنة Approved، والـ Manager هيغيرها لـ Dispatched
                status = (int)ShipmentStatus.ReadyForShipment;
            }
            else if (User.IsInRole("Admin"))
            {
                // Admin بيتابع كل الحالات، مش بيغير حالة مباشرة
                status = null;
            }
          

            var shipments = await _IShipment.GetShipments(page, 1, false,status);
            return View(shipments);
        }
    }
}
