﻿
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
    public class EnrollmentController : BaseController
    {

        public EnrollmentController(DOOROracleContext _DBcontext,
    OraTransMsgs _OraTransMsgs)
    : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetEnrollment")]
        public async Task<IActionResult> GetEnrollment()
        {
            List<EnrollmentDTO> lst = await _context.Enrollments
                .Select(sp => new EnrollmentDTO
                {
                  CreatedBy = sp.CreatedBy,
                  CreatedDate = sp.CreatedDate,
                  EnrollDate = sp.EnrollDate,
                  FinalGrade = sp.FinalGrade,
                  ModifiedBy = sp.ModifiedBy,
                  ModifiedDate = sp.ModifiedDate,
                  SchoolId = sp.SchoolId,
                  SectionId = sp.SectionId,
                  StudentId = sp.StudentId
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetEnrollment/{_StudentID}/{_SectionID}/{_SchoolID}")]
        public async Task<IActionResult> GetEnrollment( int _StudentID , int _SectionID , int _SchoolID)
        {
            EnrollmentDTO? lst = await _context.Enrollments
                .Where(x => x.StudentId == _StudentID)
                .Where(x => x.SectionId == _SectionID)
                .Where(x => x.SchoolId == _SchoolID)
                .Select(sp => new EnrollmentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    StudentId = sp.StudentId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }

        [HttpPost]
        [Route("PostEnrollment")]
        public async Task<IActionResult> PostEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                Enrollment c = await _context.Enrollments
                .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Enrollment
                    {
                        StudentId = _EnrollmentDTO.StudentId,
                        SectionId = _EnrollmentDTO.SectionId,
                        SchoolId = _EnrollmentDTO.SchoolId,
                        

                        EnrollDate = _EnrollmentDTO.EnrollDate,
                        FinalGrade = _EnrollmentDTO.FinalGrade,

                      
                    };
                    _context.Enrollments.Add(c);
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
        [Route("PutEnrollment")]
        public async Task<IActionResult> PutEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                Enrollment c = await _context.Enrollments
                .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                .FirstOrDefaultAsync();

                if (c != null)
                {

                    c.StudentId = _EnrollmentDTO.StudentId;
                    c.SectionId = _EnrollmentDTO.SectionId;
                    c.SchoolId = _EnrollmentDTO.SchoolId;
                    c.EnrollDate = _EnrollmentDTO.EnrollDate;
                    c.FinalGrade = _EnrollmentDTO.FinalGrade;
                   

                    _context.Enrollments.Update(c);
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
        [Route("DeleteEnrollment/{_StudentID}/{_SectionID}/{_SchoolID}")]
        public async Task<IActionResult> DeleteEnrollment(int _StudentID, int _SectionID, int _SchoolID)
        {
            try
            {
                Enrollment c = await _context.Enrollments
                 .Where(x => x.StudentId == _StudentID)
                .Where(x => x.SectionId == _SectionID)
                .Where(x => x.SchoolId == _SchoolID)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Enrollments.Remove(c);
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
