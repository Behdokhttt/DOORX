using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DOOR.Server.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Numerics;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;


namespace DOOR.Server.Controllers.UD
{

    [ApiController]
    [Route("api/[controller]")]

    public class StudentController: BaseController
    {

        public StudentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }
        [HttpGet]
        [Route("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            List<StudentDTO> lst = await _context.Students
                .Select(sp => new StudentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Phone = sp.Phone,
                    RegistrationDate = sp.RegistrationDate,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    StudentId = sp.StudentId,
                    Zip = sp.Zip


                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetStudent/{_StudentID}")]
        public async Task<IActionResult> GetStudent(int _StudentID)
        {
            StudentDTO? lst = await _context.Students
                .Where(x=>x.StudentId == _StudentID)
                .Select(sp => new StudentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Phone = sp.Phone,
                    RegistrationDate = sp.RegistrationDate,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    StudentId = sp.StudentId,
                    Zip = sp.Zip


                }).FirstOrDefaultAsync();
            return Ok(lst);
        }
    }
}
