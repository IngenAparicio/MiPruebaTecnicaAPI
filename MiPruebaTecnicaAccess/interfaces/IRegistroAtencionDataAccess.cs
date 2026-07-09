using MiPruebaTecnicaAccess.DTOs;
using MiPruebaTecnicaAccess.responses;

namespace MiPruebaTecnicaAccess.interfaces
{
    public interface IRegistroAtencionDataAccess
    {
        public Task<ResponseEntity<RegistroAtencionDto>> CreateEntity(RegistroAtencionDto request);

        public Task<ResponseEntity<RegistroAtencionDto>> GetEntityDetail(long id);

        public Task<ResponseEntity<List<RegistroAtencionDto>>> GetEntityList(int page, int pageSize);

        public Task<ResponseEntity<RegistroAtencionDto>> UpdateEntity(RegistroAtencionDto request);

        public Task<ResponseEntity<bool>> DeleteEntity(long id);

    }
}
