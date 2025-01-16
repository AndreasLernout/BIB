using BIBData.Models;
using BIBData.Repositories;

namespace BIBServices; 

public class ReserveringService {
    private readonly IReserveringRepository reserveringRepository;
    private readonly IUitleenobjectRepository uitleenobjectRepository;
    private readonly ILenerRepository lenerRepository;

    public ReserveringService(
            IReserveringRepository reserveringRepository, 
            IUitleenobjectRepository uitleenobjectRepository, 
            ILenerRepository lenerRepository
        ) {
        this.reserveringRepository = reserveringRepository;
        this.uitleenobjectRepository = uitleenobjectRepository;
        this.lenerRepository = lenerRepository;
    }

    public IEnumerable<Reservering> GetReserveringenVoorUitleenobject(int id) {
        return reserveringRepository.GetReserveringenVoorUitleenobject(id);
    }

    public void ItemReserveren(int itemId, int lenerId) {
        var item = uitleenobjectRepository.Get(itemId);
        var lener = lenerRepository.Get(lenerId); 
        if (item != null && lener != null) {
            reserveringRepository.Add(new Reservering {
                Uitleenobject = item,
                Lener = lener,
                GereserveerdOp = DateTime.Now

            });
        }
    }

    public Lener? GetEersteLenerOpReserveringslijst(int uitleenobjectId) {
        return reserveringRepository.GetReserveringenVoorUitleenobject(uitleenobjectId)
                                        .FirstOrDefault()?
                                        .Lener;
    }

    public void ReserveringVerwijderen(int itemId, int lenerId) {
        reserveringRepository.VerwijderReservering(itemId, lenerId);
    }

    public IEnumerable<Reservering> GetReserveringenVanLener(int lenerId) {
        return reserveringRepository.GetReserveringenVanLener(lenerId);
    }

}
