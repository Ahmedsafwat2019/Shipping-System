using BL.Contract;
using BL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    public class CarriersController : Controller
    {
 
            private readonly  ICarriersServices _carriersServices ;
         
            public CarriersController(ICarriersServices carriersServices)
            {
                _carriersServices = carriersServices ;
            }
            public async Task<IActionResult> Index()
            {
                var data =  await _carriersServices.GetAll();
                return View(data);
            }

            public async Task<IActionResult> Edit(Guid? Id)
            {
                TempData["MessageType"] = null;
                var data = new BL.Dtos.CarrierDto();
                if (Id != null)
                {
                    data = await _carriersServices.GetById((Guid)Id);
                }
                return View(data);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Save(CarrierDto data)
            {
                TempData["MessageType"] = null;
                if (!ModelState.IsValid)
                {
                    return View("Edit", data);
                }

                try
                {
                    if (data.Id == Guid.Empty)
                    await    _carriersServices.Add(data);
                    else
                    await    _carriersServices.Update(data);
                    TempData["MessageType"] = MessageTypes.SaveSucess;
                }
                catch (Exception ex)
                {
                    TempData["MessageType"] = MessageTypes.SaveFailed;
                }

                return RedirectToAction("Index");
            }

            public async Task <IActionResult> Delete(Guid Id)
            {
                TempData["MessageType"] = null;
                try
                {
                   await  _carriersServices.ChangeStatus(Id, 0);
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
