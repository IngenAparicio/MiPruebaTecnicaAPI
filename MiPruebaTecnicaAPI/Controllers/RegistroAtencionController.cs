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

        /// <summary>
        /// Servicio para crear un registro de Atenciones.
        /// </summary>
        /// <param request="request">Importante ingresar la entidad completa a crear</param>
        /// <returns>Retorna la entidad creada con su correspondiente Id de la base de datos.</returns>
        /// <response code="200">La atención fue creada correctamente.</response>
        /// <response code="500">Hubo un error al crear el registro.</response>
        [HttpPost]
        [Route("api/atenciones/Crear")]
        public async Task<IActionResult> CreateEntity(RegistroAtencionDto request)
        {
            
            var result = await services.CreateEntity(request);

            return StatusCode(result.StatusCode, result);

        }


        /// <summary>
        /// Servicio para buscar un registro específico de Atenciones.
        /// </summary>
        /// <param id="IdAtencion">Id de la atención a consultar</param>
        /// <returns>Retorna la entidad creada con su correspondiente Id de la base de datos.</returns>
        /// <response code="200">La atención fue encontrada.</response>
        /// <response code="404">No existe una atención con el identificador indicado.</response>
        /// <response code="500">Hubo un error al Buscar el registro.</response>
        [HttpGet]
        [Route("/api/atenciones/Detalle/{id}")]
        public async Task<IActionResult> GetEntityDetail([FromRoute] long id)
        {
            
            var result = await services.GetEntityDetail(id);

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Servicio para realizar auditoría a los registros de Atenciones, se filtran por codigoDiagnostico diferente de null y vacío y se realiza un update a resgistros con fecha mayor a 30 días
        /// </summary>        
        /// <returns>Retorna las entidades filtradas.</returns>
        /// <response code="200">La auditoría fue realizada correctamente.</response>
        /// <response code="404">No existen registros.</response>
        /// <response code="500">Hubo un error al buscar el registro.</response>
        [HttpGet]
        [Route("/api/atenciones/auditoria")]
        public async Task<IActionResult> GetEntityList()
        {

            var result = await services.GetEntityList();

            return StatusCode(result.StatusCode, result);

        }

        /// <summary>
        /// Servicio para actualizar un registro de Atenciones.
        /// </summary>
        /// <param request="request">Importante ingresar la entidad completa a actualizar</param>
        /// <returns>Retorna la entidad actualizada con su correspondiente Id de la base de datos.</returns>
        /// <response code="200">La atención fue actualizada correctamente.</response>
        /// <response code="404">No existe la entidad a actualizar.</response>
        /// <response code="500">Hubo un error al actualizar el registro o no se especificó el Id del registro a actualizar.</response>
        [HttpPut]
        [Route("/api/atenciones/Actualizar")]
        public async Task<IActionResult> UpdateEntity(RegistroAtencionDto request)
        {
            
            var result = await services.UpdateEntity(request);

            return StatusCode(result.StatusCode, result);
        }


        /// <summary>
        /// Servicio para eliminar un registro específico de Atenciones.
        /// </summary>
        /// <param id="IdAtencion">Id de la atención a eliminar</param>
        /// <returns>Retorna un boolean para confirmar o no la eliminación.</returns>
        /// <response code="200">La eliminación fue realizada correctamente.</response>
        /// <response code="404">No existe una atención con el identificador indicado para eliminar.</response>
        /// <response code="500">Hubo un error al eliminar el registro o no se especificó el Id del registro a eliminar.</response>
        [HttpDelete]
        [Route("/api/atenciones/Eliminar")]
        public async Task<IActionResult> DeleteEntity([FromQuery] long id)
        {
            
            var result = await services.DeleteEntity(id);

            return StatusCode(result.StatusCode, result);

        }
    }
}
