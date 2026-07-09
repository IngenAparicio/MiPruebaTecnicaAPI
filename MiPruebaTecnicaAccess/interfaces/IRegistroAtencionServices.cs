using MiPruebaTecnicaAccess.DTOs;
using MiPruebaTecnicaAccess.responses;

namespace MiPruebaTecnicaAccess.interfaces
{
    public interface IRegistroAtencionServices
    {
        public Task<ResponseEntity<RegistroAtencionDto>> CreateEntity(RegistroAtencionDto request);

        public Task<ResponseEntity<RegistroAtencionDto>> GetEntityDetail(long id);

        public Task<ResponseEntity<List<RegistroAtencionDto>>> GetEntityList();

        public Task<ResponseEntity<RegistroAtencionDto>> UpdateEntity(RegistroAtencionDto request);

        public Task<ResponseEntity<RegistroAtencionDto>> DeleteEntity(long id);
    }
}
