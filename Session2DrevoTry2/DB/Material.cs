using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class Material
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateApproval { get; set; }

    public DateTime? DateChange { get; set; }

    public int StatusMaterialId { get; set; }

    public int TypeMaterialId { get; set; }

    public string Area { get; set; } = null!;

    public string Author { get; set; } = null!;

    public virtual StatusMaterial StatusMaterial { get; set; } = null!;

    public virtual TypeMaterial TypeMaterial { get; set; } = null!;

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();
}
