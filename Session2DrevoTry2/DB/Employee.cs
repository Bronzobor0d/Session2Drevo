using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class Employee
{
    public int Id { get; set; }

    public string Fio { get; set; } = null!;

    public string? MobilePhone { get; set; }

    public DateOnly? Birthdate { get; set; }

    public int SubdivisionId { get; set; }

    public int RoleId { get; set; }

    public int? LeaderId { get; set; }

    public int? HelperId { get; set; }

    public string WorkPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CabinetId { get; set; }

    public string? AnotherInformation { get; set; }

    public virtual Cabinet Cabinet { get; set; } = null!;

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();

    public virtual ICollection<EventCalendar> EventCalendars { get; set; } = new List<EventCalendar>();

    public virtual Employee? Helper { get; set; }

    public virtual ICollection<Employee> InverseHelper { get; set; } = new List<Employee>();

    public virtual ICollection<Employee> InverseLeader { get; set; } = new List<Employee>();

    public virtual Employee? Leader { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Subdivision Subdivision { get; set; } = null!;

    public virtual ICollection<Subdivision> Subdivisions { get; set; } = new List<Subdivision>();

    public virtual ICollection<EventCalendar> EventCalendarsNavigation { get; set; } = new List<EventCalendar>();
}
