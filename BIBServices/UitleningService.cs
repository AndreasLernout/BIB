using BIBData.Models;
using BIBData.Repositories;

namespace BIBServices;

public class UitleningService {
    private IUitleenobjectRepository uitleenobjectRepository;
    private IUitleningRepository uitleningRepository;
    private ILenerRepository lenerRepository;
    private IReserveringRepository reserveringRepository;

    public UitleningService( 
            IUitleenobjectRepository uitleenobjectRepository, 
            IUitleningRepository uitleningRepository, 
            ILenerRepository lenerRepository,
            IReserveringRepository reserveringRepository
        ) {
        this.uitleenobjectRepository = uitleenobjectRepository;
        this.uitleningRepository = uitleningRepository;
        this.lenerRepository = lenerRepository;
        this.reserveringRepository = reserveringRepository;
    }

    public void ItemTerugbrengen(int uitleenobjectId) {
        //datum "tot" invullen in uitlening voor dit object
        uitleningRepository.SetReturnDate(uitleenobjectId, DateTime.Now);
        //wijzig de status
        if (reserveringRepository.IsGereserveerd(uitleenobjectId))
            uitleenobjectRepository.SetStatus(uitleenobjectId, Status.Gereserveerd);
        else
            uitleenobjectRepository.SetStatus(uitleenobjectId, Status.Beschikbaar);
    }

    public void UitleningRegistreren(int uitleenobjectId, int lenerId) {
        var item = uitleenobjectRepository.Get(uitleenobjectId);
        var lener = lenerRepository.Get(lenerId);
   
        if (item != null && lener != null) { 
   
            item.Status = Status.Uitgeleend;

            uitleningRepository.Add(
                new Uitlening { 
                    Uitleenobject = item,
                    Lener = lener,
                    Van = DateTime.Now,
                    Tot = null
                }
            );
        }
    }

    public string? GetHuidigeUitlener(int uitleenobjectId) {
        var uitlening = uitleningRepository.GetOpenstaandeUitleningVoorUitleenobject(uitleenobjectId);
        return uitlening != null ? $"{uitlening.Lener.Voornaam} {uitlening.Lener.Familienaam}" : null;
    }

    public IEnumerable<Uitlening> GetOpenstaandeUitleningenVanLener(int lenerId) {
        return uitleningRepository.GetOpenstaandeUitleningenVanLener(lenerId);
    }
}
