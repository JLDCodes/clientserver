using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HelpdeskDAL;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskDAL
{
    public class EmployeeDAO 
    {
        readonly IRepository<Employees> repository;

        public EmployeeDAO()
        {
            repository = new SomeWorkRepository<Employees>();
        }

        public Employees GetByLastName(string name)
        {


            try
            {//gets by last name if it doesn't work then it throws an exception
                return repository.GetByExpression(emp => emp.LastName == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }
        //search by email
        public Employees GetByEmail(string email)
        {
         
            try
            {
                return repository.GetByExpression(emp => emp.Email == email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

        }
        //search by id
        public Employees GetById(int id)
        {
            try
            {
                return repository.GetByExpression(stu => stu.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        //creates a list of all employees
        public List<Employees> GetAll()
        {
            try
            {
                return repository.GetAll();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        //adds an employee to the repository 
        public int Add(Employees newEmployee)
        {
            try
            {
                newEmployee = repository.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newEmployee.Id;
        }
        // remove employee from repository 
        public int Delete(int id)
        {
            int EmployeesDeleted = -1;
            try
            {
                EmployeesDeleted = repository.Delete(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return EmployeesDeleted;
        }
        //update employee object
        public UpdateStatus Update(Employees updatedStudent)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;

            try
            {
                HelpdeskContext _db = new HelpdeskContext();
                Employees currentStudent = _db.Employees.FirstOrDefault(stu => stu.Id == updatedStudent.Id);
                _db.Entry(currentStudent).OriginalValues["Timer"] = updatedStudent.Timer;
                _db.Entry(currentStudent).CurrentValues.SetValues(updatedStudent);
                if (_db.SaveChanges() == 1)
                {
                    operationStatus = UpdateStatus.Ok;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                operationStatus = UpdateStatus.Stale;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return operationStatus;
        }

    }
}
