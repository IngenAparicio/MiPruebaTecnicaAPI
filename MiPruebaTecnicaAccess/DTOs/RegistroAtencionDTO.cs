using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiPruebaTecnicaAccess.DTOs;

public partial class RegistroAtencionDto
{
    
    public int IdAtencion { get; set; }

    public string DocumentoPaciente { get; set; }

    public string? CodigoDiagnostico { get; set; }
    
    public DateTime FechaAtencion { get; set; }

    public bool RequiereAuditoria { get; set; }
}
