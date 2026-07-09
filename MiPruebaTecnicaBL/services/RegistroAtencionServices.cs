using MiPruebaTecnicaAccess.DTOs;
using MiPruebaTecnicaAccess.interfaces;
using MiPruebaTecnicaAccess.responses;
using System.Globalization;


namespace MiPruebaTecnicaBL.services
{

    public class RegistroAtencionServices : IRegistroAtencionServices
    {
        private readonly IRegistroAtencionDataAccess dataAccess;        

        public RegistroAtencionServices(IRegistroAtencionDataAccess _dataAccess)
        {
            dataAccess = _dataAccess;            

        }

        public async Task<ResponseEntity<RegistroAtencionDto>> CreateEntity(RegistroAtencionDto request)
        {
            try
            {
                var creationResult = await dataAccess.CreateEntity(request);

                if (creationResult.SuccessProcess && creationResult.SuccessData)
                {                    

                    return ResponseEntity<RegistroAtencionDto>.Ok(creationResult.Data, "CancelationPolitic created correctly");
                    
                }
                else if (!creationResult.SuccessProcess)
                {
                    var dataDummy = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, "Error while creating");

                }

                var dataDummy2 = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.NotFound(dataDummy2, "dataBlank");

            }
            catch (Exception ex)
            {

                var dataDummy = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, ex.Message);

                
            }
        }

        public async Task<ResponseEntity<RegistroAtencionDto>> GetEntityDetail(long id)
        {
            try
            {

                var validation =  await Validation(id);

                if (!validation)
                {
                    var dataDummy = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, "Id must be specified");
                }

                var detailResult = await dataAccess.GetEntityDetail(id);

                
                if (detailResult.SuccessProcess && detailResult.SuccessData)
                {
                    return ResponseEntity<RegistroAtencionDto>.Ok(detailResult.Data, "Success");
                    
                }
                else if (!detailResult.SuccessProcess)
                {                    
                    var dataDummy = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, "Error while searching");
                }

                var dataDummy2 = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.NotFound(dataDummy2, "No data found");
            }
            catch (Exception ex)
            {
                var dataDummy = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, ex.Message);
            }
        }

        public async Task<ResponseEntity<List<RegistroAtencionDto>>> GetEntityList()
        {
            try
            {
                var responseAccess = await dataAccess.GetEntityList();

                if (responseAccess.SuccessProcess && responseAccess.SuccessData)
                {
                    return ResponseEntity<List<RegistroAtencionDto>>.Ok(responseAccess.Data, "Success");
                }
                else if (!responseAccess.SuccessProcess)
                {
                    
                    var dataDummy = new List<RegistroAtencionDto>();
                    return ResponseEntity<List<RegistroAtencionDto>>.Error(dataDummy, "Error while searching");
                }

                var dataDummy2 = new List<RegistroAtencionDto>();
                return ResponseEntity<List<RegistroAtencionDto>>.NotFound(dataDummy2, "No data found");
            }
            catch (Exception ex)
            {                
                var dataDummy = new List<RegistroAtencionDto>();
                return ResponseEntity<List<RegistroAtencionDto>>.Error(dataDummy, ex.Message);
            }
        }

        public async Task<ResponseEntity<RegistroAtencionDto>> UpdateEntity(RegistroAtencionDto request)
        {
            try
            {

                var validation = await Validation(request.IdAtencion);

                if (!validation)
                {
                    var dataDummy = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, "Id must be specified");
                }


                var updateResult = await dataAccess.UpdateEntity(request);

                if (updateResult.SuccessProcess && updateResult.SuccessData)
                {

                    return ResponseEntity<RegistroAtencionDto>.Ok(updateResult.Data, "Successfully updated");
                    
                }
                else if (!updateResult.SuccessProcess)
                {
                    var dataDummy = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, "Error while updating");
                }

                var dataDummy2 = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.NotFound(dataDummy2, "No data to update");

            }
            catch (Exception ex)
            {
                var dataDummy = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, ex.Message);
            }
        }

        public async Task<ResponseEntity<RegistroAtencionDto>> DeleteEntity(long id)
        {
            try
            {

                var validation = await Validation(id);

                if (!validation)
                {
                    var dataDummy2 = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy2, "Id must be specified");
                }

                var deleteResult = await dataAccess.DeleteEntity(id);

                if (deleteResult.SuccessProcess && deleteResult.SuccessData)
                {

                    return ResponseEntity<RegistroAtencionDto>.Ok(deleteResult.Data, "Successfully deleted");

                    
                }
                else if (!deleteResult.SuccessProcess)
                {
                    var dataDummy2 = new RegistroAtencionDto();
                    return ResponseEntity<RegistroAtencionDto>.Error(dataDummy2, "Error while deleting");
                }

                var dataDummy = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.NotFound(dataDummy, "No data to delete");
            }
            catch (Exception ex)
            {
                var dataDummy = new RegistroAtencionDto();
                return ResponseEntity<RegistroAtencionDto>.Error(dataDummy, ex.Message);
            }
        }

        private async static Task<bool> Validation(long id)
        {
            if(id == 0)
            {
                return false;
            }

            return true;
        }

    }
}
