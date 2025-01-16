using BIBData.Models;
using BIBData.Repositories;

namespace BIBServices;

public class UitleenobjectService {
    private readonly IUitleenobjectRepository uitleenobjectRepository;
    public UitleenobjectService(IUitleenobjectRepository uitleenobjectRepository) {
        this.uitleenobjectRepository = uitleenobjectRepository;
    }

    public IEnumerable<Uitleenobject> GetAllUitleenobjecten() {
        return uitleenobjectRepository.GetAll();
    }

    public string GetUitleenobjectType(int id) {
        return uitleenobjectRepository
                .Get(id)!.GetType().ToString()
                .Contains("Boek") ? "Boek" : "Device";
    }
    public string GetDetails(int id) {
        if (GetUitleenobjectType(id) == "Boek") {
            var boek = uitleenobjectRepository.GetBoek(id);
            return boek != null ? boek.ISBN + " (" + boek.Auteur + ", " + boek.Aantalpaginas + "p.)"
                                : $"Geen info gevonden over een boek met id {id}";
        }
        else {
            var device = uitleenobjectRepository.GetDevice(id);
            return device != null ? device.OperatingSysteem.Naam + " - " + device.Schermgrootte + "\""
                                  : $"Geen info gevonden over een device met id {id}";
        }
    }

    public Uitleenobject? GetUitleenobject(int id) {
        return uitleenobjectRepository.Get(id);
    }

}
