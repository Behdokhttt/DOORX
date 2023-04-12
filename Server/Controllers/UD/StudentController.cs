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
using static Duende.IdentityServer.Models.IdentityResources;

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
        [Route("GetStudent/{_StudentID}/{_SchoolID}")]
        public async Task<IActionResult> GetStudent(int _StudentID , int _SchoolID)
        {
            StudentDTO? lst = await _context.Students

                .Where(x=>x.StudentId == _StudentID)
                .Where(x => x.SchoolId == _SchoolID)
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

        [HttpPost]
        [Route("PostStudent")]
        public async Task<IActionResult> PostStudent([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                Student c = await _context.Students
                .Where(x => x.StudentId == _StudentDTO.StudentId)
                .Where(x => x.SchoolId == _StudentDTO.SchoolId)

                  .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Student
                    {
                        Salutation = _StudentDTO.Salutation,
                        FirstName= _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        StreetAddress = _StudentDTO.StreetAddress,
                        Zip = _StudentDTO.Zip,
                        Phone = _StudentDTO.Phone,
                        Employer = _StudentDTO.Employer,
                        RegistrationDate = _StudentDTO.RegistrationDate


                        
                    };
                    _context.Students.Add(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }

        [HttpPut]
        [Route("PutStudent")]
        public async Task<IActionResult> PutCourse([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                Student c = await _context.Students

                .Where(x => x.StudentId == _StudentDTO.StudentId)
                .Where(x => x.SchoolId == _StudentDTO.SchoolId)

                .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.Salutation = _StudentDTO.Salutation;
                    c.FirstName = _StudentDTO.FirstName;
                    c.LastName = _StudentDTO.LastName;
                    c.StreetAddress = _StudentDTO.StreetAddress;
                    c.Zip = _StudentDTO.Zip;
                    c.Phone = _StudentDTO.Phone;
                    c.Employer = _StudentDTO.Employer;
                    c.RegistrationDate = _StudentDTO.RegistrationDate;



                    _context.Students.Update(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteStudent/{_StudentID}/{_SchoolID}")]
        public async Task<IActionResult> DeleteStudent(int _StudentID, int _SchoolID)
        {
            try
            {
                Student c = await _context.Students

                .Where(x => x.StudentId == _StudentID)
                .Where(x => x.SchoolId == _SchoolID)


                .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Students.Remove(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }


    }
}
