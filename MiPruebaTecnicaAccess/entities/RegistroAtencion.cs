using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiPruebaTecnicaAccess.entities;

public partial class RegistroAtencion
{
    [Key]
    public int IdAtencion { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string DocumentoPaciente { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? CodigoDiagnostico { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaAtencion { get; set; }

    public bool RequiereAuditoria { get; set; }
}
