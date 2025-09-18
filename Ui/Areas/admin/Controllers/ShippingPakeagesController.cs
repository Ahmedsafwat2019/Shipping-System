using BL.Contract;
using BL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]
    public class ShippingPackagesController : Controller
    {
        private readonly IPackgingTypes _IShippingPackages;

        public ShippingPackagesController(IPackgingTypes shipingTypes)
        {
            _IShippingPackages = shipingTypes;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _IShippingPackages.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Edit(Guid? Id)
        {
            TempData["MessageType"] = null;
            var data = new ShipingPackgingDto();
            if (Id != null)
            {
                data = await _IShippingPackages.GetById((Guid)Id);
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(ShipingPackgingDto data)
        {
            TempData["MessageType"] = null;

            if (!ModelState.IsValid)
                return View("Edit", data);

            try
            {
                if (data.Id == Guid.Empty)
                    await _IShippingPackages.Add(data);
                else
                    await _IShippingPackages.Update(data);

                TempData["MessageType"] = MessageTypes.SaveSucess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageTypes.SaveFailed;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            TempData["MessageType"] = null;

            try
            {
                await _IShippingPackages.ChangeStatus(Id, 0);
                TempData["MessageType"] = MessageTypes.DeleteSuccess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageTypes.DeleteFailed;
            }

            return RedirectToAction("Index");
        }
    }
}
