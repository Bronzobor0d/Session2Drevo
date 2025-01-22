using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class TypeMaterial
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
