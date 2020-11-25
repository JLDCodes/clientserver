using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Linq;
using System.Reflection; // compiler figures out method name using meta data 
using Microsoft.EntityFrameworkCore;
using HelpdeskDAL;

namespace ExercisesDAL
{
    public class DepartmentsDAO
    {
        readonly IRepository<Departments> repository;

        public DepartmentsDAO()
        {
            repository = new SomeWorkRepository<Departments>();
        }


        public List<Departments> GetAll()
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

    }
}
