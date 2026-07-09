using Microsoft.EntityFrameworkCore;
using MiPruebaTecnicaAccess.context;
using MiPruebaTecnicaAccess.DTOs;
using MiPruebaTecnicaAccess.entities;
using MiPruebaTecnicaAccess.interfaces;
using MiPruebaTecnicaAccess.responses;
using MiPruebaTecnicaAccess.utilities;

namespace MiPruebaTecnicaAccess.access
{
    public class RegistroAtencionDataAccess : IRegistroAtencionDataAccess
    {
        protected PruebaTecnicaDbContext context;

        public RegistroAtencionDataAccess(PruebaTecnicaDbContext _context)
        {
            context = _context;
        }

        public async Task<ResponseEntity<RegistroAtencionDto>> CreateEntity(RegistroAtencionDto request)
        {
            try
            {
                var entity = Mapper.Map<RegistroAtencionDto, RegistroAtencion>(request);
                context.RegistroAtencion.Add(entity);
                await context.SaveChangesAsync();
                var result = Mapper.Map<RegistroAtencion, RegistroAtencionDto>(entity);

                var response = new ResponseEntity<RegistroAtencionDto>();
                response.Message = "Success";
                response.SuccessData = true;
                response.Data = result;
                response.SuccessProcess = true;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseEntity<RegistroAtencionDto>();
                response.Message = ex.Message;
                response.SuccessData = false;
                response.Data = new RegistroAtencionDto();
                response.SuccessProcess = false;
                return response;
            }
        }

        public async Task<ResponseEntity<RegistroAtencionDto>> GetEntityDetail(long id)
        {
            try
            {
                var entity = await context.RegistroAtencion.FirstOrDefaultAsync(x => x.IdAtencion == id);
                
                var response = new ResponseEntity<RegistroAtencionDto>();
                
                response.SuccessData = false;
                
                var dto = new RegistroAtencionDto();
                
                if (entity is not null)
                {
                    dto = Mapper.Map<RegistroAtencion, RegistroAtencionDto>(entity);

                    response.SuccessData = true;
                }
                
                response.Message = "Success";                
                response.Data = dto;
                response.SuccessProcess = true;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseEntity<RegistroAtencionDto>();
                response.Message = ex.Message;
                response.SuccessData = false;
                response.Data = new RegistroAtencionDto();
                response.SuccessProcess = false;
                return response;
            }
        }

        public async Task<ResponseEntity<List<RegistroAtencionDto>>> GetEntityList(int page, int pageSize)
        {
            try
            {
                var fechaLimite = DateTime.UtcNow.AddDays(-30);

                await context.RegistroAtencion.Where(x => !string.IsNullOrEmpty(x.CodigoDiagnostico) && x.FechaAtencion < fechaLimite)
                    .ExecuteUpdateAsync(updates => updates.SetProperty(x => x.RequiereAuditoria, true));

                var list = await context.RegistroAtencion.Where(x => !string.IsNullOrEmpty(x.CodigoDiagnostico))                    
                    .OrderByDescending(x => x.FechaAtencion).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();                           

                var response = new ResponseEntity<List<RegistroAtencionDto>>();

                response.SuccessData = false;

                var dtoList = new List<RegistroAtencionDto>();

                if (list.Count > 0)
                {
                    dtoList = Mapper.MapList<List<RegistroAtencion>, List<RegistroAtencionDto>>(list);

                    response.SuccessData = true;
                }                

                
                response.Message = "Success";                
                response.Data = dtoList;
                response.SuccessProcess = true;
                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseEntity<List<RegistroAtencionDto>>();
                response.Message = ex.Message;
                response.SuccessData = false;
                response.Data = new List<RegistroAtencionDto>();
                response.SuccessProcess = false;
                return response;
            }
        }

        public async Task<ResponseEntity<RegistroAtencionDto>> UpdateEntity(RegistroAtencionDto request)
        {
            try
            {
                var entityExist = await context.RegistroAtencion.FirstOrDefaultAsync(x => x.IdAtencion == request.IdAtencion);
                if (entityExist != null)
                {
                    FrameworkTypeUtility.SetProperties(request, entityExist);
                    await context.SaveChangesAsync();

                    var response = new ResponseEntity<RegistroAtencionDto>();
                    response.Message = "Successfully updated";
                    response.SuccessData = true;
                    response.Data = request;
                    response.SuccessProcess = true;
                    return response;
                }
                else
                {
                    var response = new ResponseEntity<RegistroAtencionDto>();
                    response.Message = "No data to update";
                    response.SuccessData = false;
                    response.Data = new RegistroAtencionDto();
                    response.SuccessProcess = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseEntity<RegistroAtencionDto>();
                response.Message = ex.Message;
                response.SuccessData = false;
                response.Data = new RegistroAtencionDto();
                response.SuccessProcess = false;
                return response;
            }
        }

        public async Task<ResponseEntity<bool>> DeleteEntity(long id)
        {
            try
            {
                var entityExist = await context.RegistroAtencion.FirstOrDefaultAsync(x => x.IdAtencion == id);
                if (entityExist != null)
                {
                    context.RegistroAtencion.Remove(entityExist);
                    await context.SaveChangesAsync();

                    var response = new ResponseEntity<bool>();
                    response.Message = "Successfully deleted";
                    response.SuccessData = true;
                    response.Data = true;
                    response.SuccessProcess = true;
                    return response;

                }
                else
                {
                    var response = new ResponseEntity<bool>();
                    response.Message = "No data to remove";
                    response.SuccessData = false;
                    response.Data = false;
                    response.SuccessProcess = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseEntity<bool>();
                response.Message = ex.Message;
                response.SuccessData = false;
                response.Data = false;
                response.SuccessProcess = false;
                return response;
            }
        }
    }
}
