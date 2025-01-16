using BIBData.Models;
using BIBServices;
using BIBWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BIBWeb.Controllers;

public class UitleenobjectController : Controller {

    private readonly UitleenobjectService uitleenobjectService;
    private readonly LenerService lenerService;
    private readonly UitleningService uitleningService;
    private readonly ReserveringService reserveringService;

    public UitleenobjectController(
            UitleenobjectService uitleenobjectService,
            LenerService lenerService,
            UitleningService uitleningService,
            ReserveringService reserveringService
        ) {
        this.lenerService = lenerService;
        this.uitleenobjectService = uitleenobjectService;
        this.uitleningService = uitleningService;
        this.reserveringService = reserveringService;
    }

    public IActionResult Index() {
        var objecten = uitleenobjectService.GetAllUitleenobjecten();
        var objectenLijst = objecten.Select( item => 
            new UitleenobjectDetailViewModel {
                Id = item.Id,
                Naam = item.Naam,
                Jaar = item.Jaar,
                Kostprijs = item.Kostprijs,
                Status = item.Status,

                Details = uitleenobjectService.GetDetails(item.Id),
                Type = uitleenobjectService.GetUitleenobjectType(item.Id)
            }
        );

        return View(objectenLijst);
    }

    public IActionResult Detail(int id) {
        UitleenobjectDetailViewModel model = null!;
        Uitleenobject? item = uitleenobjectService.GetUitleenobject(id);
        if (item != null)
            model = new UitleenobjectDetailViewModel {
                Id = id,
                Naam = item.Naam,
                Jaar = item.Jaar,
                Kostprijs = item.Kostprijs,
                Status = item.Status,
                ImageUrl = item.ImageUrl,
                Details = uitleenobjectService.GetDetails(id),
                Type = uitleenobjectService.GetUitleenobjectType(id),
                HuidigeUitlener = uitleningService.GetHuidigeUitlener(id),
                EersteInWachtlijst = reserveringService.GetEersteLenerOpReserveringslijst(id)
            };
        else
            ViewBag.Errormessage = $"Geen info gevonden voor object met id {id}";
        return View(model);
    }

    public IActionResult Uitlenen(int id) {
        var item = uitleenobjectService.GetUitleenobject(id);

        if (item != null) {

            var lenerSelectList = new List<SelectListItem>();

            foreach (var lener in lenerService.GetAllLeners()) {
                lenerSelectList.Add(
                    new SelectListItem {
                        Text = lener.Voornaam + " " + lener.Familienaam,
                        Value = lener.Id.ToString()
                    }
                );
            }

            var model = new UitleenViewModel {
                ItemId = id,
                ImageUrl = item.ImageUrl!,
                Naam = item.Naam,
                Leners = lenerSelectList
            };
            return View(model);
        }
        else {
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public IActionResult UitleningRegistreren(int itemId, int GekozenLenerId) {
        uitleningService.UitleningRegistreren(itemId, GekozenLenerId);
        return RedirectToAction(nameof(Detail), new { id = itemId });
    }

    [HttpPost]
    public IActionResult Terugbrengen(int id) {
        uitleningService.ItemTerugbrengen(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Reserveren(int id) {
        var item = uitleenobjectService.GetUitleenobject(id);
        if (item != null) {
            var reserveringenLijst = reserveringService.GetReserveringenVoorUitleenobject(id);
            var lenerSelectList = new List<SelectListItem>();

            foreach (var lener in lenerService.GetAllLeners())
                lenerSelectList.Add(
                    new SelectListItem {
                        Text = lener.Voornaam + " " + lener.Familienaam,
                        Value = lener.Id.ToString()
                    }
                );
            var model = new ReserveerViewModel {
                ItemId = id,
                ImageUrl = item.ImageUrl,
                Naam = item.Naam,
                Reserveringen = reserveringenLijst,
                Leners = lenerSelectList
            };

            return View(model);
        }
        else {
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public IActionResult ItemReserveren(int itemId, int GekozenLenerId) {
        reserveringService.ItemReserveren(itemId, GekozenLenerId);
        return RedirectToAction("Detail", new { id = itemId });
    }

    [HttpPost]
    public IActionResult ReserveringOphalen(int itemId, int lenerId) {
        //oudste reservering voor dit item verwijderen
        reserveringService.ReserveringVerwijderen(itemId, lenerId);
        //item uitlenen aan eerste in wachtlijst
        uitleningService.UitleningRegistreren(itemId, lenerId);
        //terug naar detailpage
        return RedirectToAction("Detail", new { id = itemId });
    }
}
