using System;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    public partial class Departments: WorkEntity
    {
        public Departments()
        {
            Employees = new HashSet<Employees>();
        }

        public string DepartmentName { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
