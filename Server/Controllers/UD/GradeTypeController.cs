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
    public class GradeTypeController: BaseController
    {

        public GradeTypeController(DOOROracleContext _DBcontext,
    OraTransMsgs _OraTransMsgs)
    : base(_DBcontext, _OraTransMsgs)

        {
        }

        [HttpGet]
        [Route("GetGradeType")]
        public async Task<IActionResult> GetGradeType()
        {
            List<GradeTypeDTO> lst = await _context.GradeTypes
                .Select(sp => new GradeTypeDTO
                {
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                Description = sp.Description,
                GradeTypeCode = sp.GradeTypeCode,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
                SchoolId = sp.SchoolId
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetGradeType/{_SchoolId}/{_GradeTypeCode}")]
        public async Task<IActionResult> GetGradeType(int _SchoolID , string _GradeTypeCode)
        {
            GradeTypeDTO? lst = await _context.GradeTypes

                .Where(x => x.SchoolId == _SchoolID)
                .Where(x => x.GradeTypeCode == _GradeTypeCode)
                .Select(sp => new GradeTypeDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Description = sp.Description,
                    GradeTypeCode = sp.GradeTypeCode,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }
        [HttpPost]
        [Route("PostGradeType")]
        public async Task<IActionResult> PostGradeType([FromBody] CourseDTO _CourseDTO)
        {
            try
            {
                Course c = await _context.Courses.Where(x => x.CourseNo == _CourseDTO.CourseNo).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Course
                    {
                        Cost = _CourseDTO.Cost,
                        Description = _CourseDTO.Description,
                        Prerequisite = _CourseDTO.Prerequisite
                    };
                    _context.Courses.Add(c);
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
