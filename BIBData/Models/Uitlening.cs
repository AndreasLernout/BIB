namespace BIBData.Models;

public class Uitlening {
    public int Id { get; set; }
    public Uitleenobject Uitleenobject { get; set; }
    public Lener Lener { get; set; }
    public DateTime Van { get; set; }
    public DateTime? Tot { get; set; }
}
