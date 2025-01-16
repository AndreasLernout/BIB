using Microsoft.AspNetCore.Mvc.Rendering;

namespace BIBWeb.Models;

public class UitleenViewModel {
    public int ItemId { get; set; }
    public string Naam { get; set; }
    public string ImageUrl { get; set; }
    public int GekozenLenerId { get; set; }
    public IEnumerable<SelectListItem> Leners { get; set; }
}
