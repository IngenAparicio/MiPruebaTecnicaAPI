using Microsoft.AspNetCore.Mvc;
using MiPruebaTecnicaAccess.DTOs;
using MiPruebaTecnicaAccess.interfaces;


namespace MiPruebaTecnicaAPI.Controllers
{
    
    [ApiController]
    public class RegistroAtencionController : Controller
    {
        readonly IRegistroAtencionServices services;

        public RegistroAtencionController(IRegistroAtencionServices _services)
        {
            services = _services;
        }

        [HttpPost]
        [Route("api/atenciones/Crear")]
        public async Task<IActionResult> CreateEntity(RegistroAtencionDto request)
        {
            
            var result = await services.CreateEntity(request);

            return StatusCode(result.StatusCode, result);

        }

        [HttpGet]
        [Route("api/atenciones/Detalle/{id}")]
        public async Task<IActionResult> GetEntityDetail([FromRoute] long id)
        {
            
            var result = await services.GetEntityDetail(id);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Route("api/atenciones/auditoria")]
        public async Task<IActionResult> GetEntityList(int page = 1, int pageSize = 10)
        {

            var result = await services.GetEntityList(page, pageSize);

            return StatusCode(result.StatusCode, result);

        }

        [HttpPut]
        [Route("api/atenciones/Actualizar")]
        public async Task<IActionResult> UpdateEntity(RegistroAtencionDto request)
        {
            
            var result = await services.UpdateEntity(request);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [Route("api/atenciones/Eliminar/{id}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] long id)
        {
            
            var result = await services.DeleteEntity(id);

            return StatusCode(result.StatusCode, result);

        }
    }
}
