using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class EventCalendar
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TypeEventId { get; set; }

    public int StatusEventId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public string? Description { get; set; }

    public int? SubdivisionId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual StatusEvent StatusEvent { get; set; } = null!;

    public virtual Subdivision? Subdivision { get; set; }

    public virtual TypeEvent TypeEvent { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
