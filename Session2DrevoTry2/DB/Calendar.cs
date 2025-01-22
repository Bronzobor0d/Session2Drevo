using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class Calendar
{
    public int Id { get; set; }

    public int TypePeriodId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public string? Justification { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual TypePeriod TypePeriod { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
