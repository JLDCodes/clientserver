using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using ExercisesViewModels;
using HelpdeskDAL;
using HelpdeskViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExercisesWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                DepartmentViewModel viewModel = new DepartmentViewModel();
                List<DepartmentViewModel> Allemployees = viewModel.GetAll();
                return Ok(Allemployees);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }
    }
}
