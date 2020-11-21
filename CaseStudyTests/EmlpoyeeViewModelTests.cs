using HelpdeskViewModels;
using Xunit;
using System.Collections.Generic;

namespace ExerciseTests
{
    public class ViewModelTests
    {
        [Fact]
        public void Employee_GetByEmail()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "bs@abc.com" };
            vm.GetByEmail();
            Assert.NotNull(vm.FirstName);
        }

        [Fact]
        public void Employee_GetById()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "bs@abc.com" };
            vm.GetByEmail();
            vm.GetById();
            Assert.NotNull(vm.FirstName);
        }

        [Fact]
        public void Employee_GetAllTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            List<EmployeeViewModel> allEmployees = vm.GetAll();
            Assert.True(allEmployees.Count > 0);
        }

        [Fact]
        public void Employeet_AddTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel
            {
                FirstName = "Jean-Luc",
                LastName = "Desjardins",
                PhoneNo = "(555)555-5551",
                Title = "Mr.",
                DepartmentId = 100,
                Email = "jld@abc.com"
            };
            vm.Add();
            Assert.True(vm.id > 0);
        }

        [Fact]
        public void Employee_UpdateTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "jld@abc.com" };
            vm.GetByEmail(); // student just added
            vm.PhoneNo = vm.PhoneNo == "(555)555-5551" ? "(555)555-5552" : "(555)555-5551";
            int employeeUpdated = vm.Update();
            Assert.True(employeeUpdated > 0);
        }

        [Fact]
        public void Employee_DeleteTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "jld@abc.com" };
            vm.GetByEmail();
            int employeeDeleted = vm.Delete();
            Assert.True(employeeDeleted == 1);
        }
    }
}
