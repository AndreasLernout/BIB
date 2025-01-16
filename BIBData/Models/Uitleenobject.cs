using System.ComponentModel.DataAnnotations;

namespace BIBData.Models;

public abstract class Uitleenobject {
    public int Id { get; set; }
    public string Naam { get; set; }
    public int Jaar { get; set; }
    public Status Status { get; set; }
    [DisplayFormat(DataFormatString = "0:€ #,##0.00")]
    public decimal Kostprijs { get; set; }
    public string? ImageUrl { get; set; } //niet verplicht veld
}

public enum Status {
    Beschikbaar,
    Uitgeleend,
    Gereserveerd
}

