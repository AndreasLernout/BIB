namespace BIBData.Models;

public class Reservering {
    public int Id { get; set; }
    public Uitleenobject Uitleenobject { get; set; }
    public Lener Lener { get; set; }
    public DateTime GereserveerdOp { get; set; }
}