using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace HelpdeskViewModels
{ 
    // our employee class full of attributes. getters and setters
    public class EmployeeViewModel
    {
        readonly private EmployeeDAO _dao;

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int id { get; set; }
        public string StaffPicture64 { get; set; }
        public bool isTech { get; private set; }

        public EmployeeViewModel()
        {
            _dao = new EmployeeDAO();
        }
        // search employee by email
        public void GetByEmail()
        {
            try
            {
                Employees emp = _dao.GetByEmail(Email);
                Title = emp.Title;
                FirstName = emp.FirstName;
                LastName = emp.LastName;
                PhoneNo = emp.PhoneNo;
                Email = emp.Email;
                id = emp.Id;
                DepartmentId = emp.DepartmentId;
                
                if(emp.IsTech != null)
                {
                    isTech = Convert.ToBoolean(emp.IsTech);
                }

                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                Email = "not found";
            }
            catch (Exception ex)
            {
                Email = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        //search employee by id
        public void GetById()
        {
            try
            {
                Employees emp = _dao.GetById(id);
                Title = emp.Title;
                FirstName = emp.FirstName;
                LastName = emp.LastName;
                PhoneNo = emp.PhoneNo;
                Email = emp.Email;
                id = emp.Id;
                DepartmentId = emp.DepartmentId;

                if (emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                }
                //if (StaffPicture64 != null)
                //{
                //    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                //}
                Timer = Convert.ToBase64String(emp.Timer);
            }
            catch (NullReferenceException nex)
            {
                Debug.WriteLine(nex.Message);
                LastName = "not found";
            }
            catch (Exception ex)
            {
                LastName = "not found";
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        //create a list of employees 
        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employees> allEmployees = _dao.GetAll();
                foreach (Employees emp in allEmployees)
                {
                    EmployeeViewModel empVm = new EmployeeViewModel();
                    empVm.Title = emp.Title;
                    empVm.FirstName = emp.FirstName;
                    empVm.LastName = emp.LastName;
                    empVm.PhoneNo = emp.PhoneNo;
                    empVm.Email = emp.Email;
                    empVm.id = emp.Id;
                    empVm.DepartmentId = emp.DepartmentId;
                    //empVm.DepartmentName = emp.Department.DepartmentName;
                    if (emp.StaffPicture != null)
                    {
                        empVm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                    }
                    empVm.Timer = Convert.ToBase64String(emp.Timer);
                    allVms.Add(empVm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allVms;
        }
        //add employee
        public void Add()
        {
            id = -1;
            try
            {
                Employees emp = new Employees
                {
                    Title = Title,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNo = PhoneNo,
                    Email = Email,
                    DepartmentId = DepartmentId
                };
                id = _dao.Add(emp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }
        //update employeee
        public int Update()
        {
            int employeesUpdated = -1;
            try
            {
                Employees emp = new Employees
                {
                    Title = Title,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNo = PhoneNo,
                    Email = Email,
                    Id = id,
                    DepartmentId = DepartmentId
                };

                if (StaffPicture64 != null)
                {
                    emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }

                emp.Timer = Convert.FromBase64String(Timer);
                employeesUpdated = (int)_dao.Update(emp);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                   MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return employeesUpdated;
        }
        //remove employee
        public int Delete()
        {
            int empdentsDeleted = -1;

            try
            {
                empdentsDeleted = _dao.Delete(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                  MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return empdentsDeleted;
        }

    }
}
