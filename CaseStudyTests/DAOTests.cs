using HelpdeskDAL;
using Xunit;
using System.Collections.Generic;

namespace CaseStudyTests
{
    public class DAOTests
    {
        [Fact]
        public void Employee_GetByEmail()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees selectedEmployee = dao.GetByEmail("bs@abc.com");
            Assert.NotNull(selectedEmployee);
        }
        [Fact]
        public void Employee_GetById()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees selectedEmployee = dao.GetById(2);
            Assert.NotNull(selectedEmployee);
        }
        [Fact]
        public void Employee_GetAll()
        {
            EmployeeDAO dao = new EmployeeDAO();
            List<Employees> allEmployees = dao.GetAll();
            Assert.NotNull(allEmployees);
        }
        [Fact]
        public void Employee_AddTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees newEmployee = new Employees
            {
                FirstName = "Jason",
                LastName = "Smith",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DepartmentId = 100,
                Email = "js@abc.ca"
            };
            dao.Add(newEmployee);
            Assert.NotNull(newEmployee);
        }
        [Fact]
        public void Employee_UpdateTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employees employeeForUpdate = dao.GetByLastName("Smith");

            if (employeeForUpdate != null)
            {
                string oldPhoneNo = employeeForUpdate.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-555" : "519-555-1234";
                employeeForUpdate.PhoneNo = newPhoneNo;
            }

            Assert.True(dao.Update(employeeForUpdate) == UpdateStatus.Ok);
        }

        [Fact]
        public void Employee_DeleteTest()
        {
            EmployeeDAO dao = new EmployeeDAO();
            int EmployeesDeleted = dao.Delete(dao.GetByEmail("jld@abc.ca").Id);
            Assert.True(EmployeesDeleted != -1);
        }
        [Fact]
        public void Employee_ConcurrencyTest()
        {
            EmployeeDAO dao1 = new EmployeeDAO();
            EmployeeDAO dao2 = new EmployeeDAO();
            Employees employeeForUpdate1 = dao1.GetByLastName("Smith");
            Employees employeeForUpdate2 = dao2.GetByLastName("Smith");

            if (employeeForUpdate1 != null)
            {
                string oldPhoneNo = employeeForUpdate1.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-555" : "519-555-1234";
                employeeForUpdate1.PhoneNo = newPhoneNo;
                if (dao1.Update(employeeForUpdate1) == UpdateStatus.Ok)
                {
                    employeeForUpdate2.PhoneNo = "666-666-6666";
                    Assert.True(dao2.Update(employeeForUpdate2) == UpdateStatus.Stale);
                }
                else
                {
                    Assert.True(false);
                }
            }
        }
    }
}
