using BIBData.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BIBWeb.Models; 

public class ReserveerViewModel {
    public int GekozenLenerId { get; set; }
    public int ItemId { get; set; }
    public string Naam { get; set; }
    public string ImageUrl { get; set; }
    public IEnumerable<Reservering> Reserveringen { get; set; }
    public IEnumerable<SelectListItem> Leners { get; set; }
}
