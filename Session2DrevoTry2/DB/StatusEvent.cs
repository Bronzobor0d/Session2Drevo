using System;
using System.Collections.Generic;

namespace Session2DrevoTry2.DB;

public partial class StatusEvent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventCalendar> EventCalendars { get; set; } = new List<EventCalendar>();
}
