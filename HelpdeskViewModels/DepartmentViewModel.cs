using ExercisesDAL;
using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExercisesViewModels
{
    public class DepartmentViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        //public byte[] Timer { get; set; }

        readonly private DepartmentsDAO _dao;
        public DepartmentViewModel()
        {
            _dao = new DepartmentsDAO();
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Departments> allDepartments = _dao.GetAll();
                foreach (Departments dep in allDepartments)
                {
                    DepartmentViewModel depVm = new DepartmentViewModel
                    {
                        Id = dep.Id,
                        Name = dep.DepartmentName,
                    };
                    allVms.Add(depVm);
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

    }
}
