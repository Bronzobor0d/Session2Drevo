using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class Subdivision
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? LeaderId { get; set; }

    public int? SubdivisionId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EventCalendar> EventCalendars { get; set; } = new List<EventCalendar>();

    public virtual ICollection<Subdivision> InverseSubdivisionNavigation { get; set; } = new List<Subdivision>();

    public virtual Employee? Leader { get; set; }

    public virtual Subdivision? SubdivisionNavigation { get; set; }
}
