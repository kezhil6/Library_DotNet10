using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Staff.Core.Interfaces
{
    public interface IStaffRepository
    {
        List<Library.Staff.Core.Entities.Staff> GetAllStaffs();
        void AddStaff(Library.Staff.Core.Entities.Staff staff);
    }
}
