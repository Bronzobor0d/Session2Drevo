using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class Cabinet
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
