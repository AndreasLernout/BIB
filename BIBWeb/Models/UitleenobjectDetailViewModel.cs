using BIBData.Models;
using System.ComponentModel.DataAnnotations;

namespace BIBWeb.Models;

public class UitleenobjectDetailViewModel {
    public int Id { get; set; }
    public string Naam { get; set; }
    public int Jaar { get; set; }
    public Status Status { get; set; }
    [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
    public decimal Kostprijs { get; set; }
    public string? ImageUrl { get; set; }
    public string Details { get; set; }
    public string Type { get; set; }
    public string? HuidigeUitlener { get; set; }
    public Lener? EersteInWachtlijst { get; set; }
}

