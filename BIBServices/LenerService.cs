using BIBData.Models;
using BIBData.Repositories;

namespace BIBServices;

public class LenerService {
    private readonly ILenerRepository lenerRepository;
    public LenerService(ILenerRepository lenerRepository) {
        this.lenerRepository = lenerRepository;
    }

    public Lener? GetLener(int id) {
        return lenerRepository.Get(id);
    }

    public IEnumerable<Lener> GetAllLeners() {
        return lenerRepository.GetAll();
    }
}
