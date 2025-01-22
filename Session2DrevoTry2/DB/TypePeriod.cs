using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class TypePeriod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();
}
