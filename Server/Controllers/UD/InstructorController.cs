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
    public class InstructorController: BaseController
    {
        public InstructorController(DOOROracleContext _DBcontext,
    OraTransMsgs _OraTransMsgs)
    : base(_DBcontext, _OraTransMsgs)

        {
        }
        [HttpGet]
        [Route("GetInstructor")]
        public async Task<IActionResult> GetInstructor()
        {
            List<InstructorDTO> lst = await _context.Instructors
                .Select(sp => new InstructorDTO
                {
                  CreatedBy = sp.CreatedBy,
                  CreatedDate = sp.CreatedDate,
                  FirstName = sp.FirstName,
                  InstructorId = sp.InstructorId,
                  LastName = sp.LastName,
                  ModifiedBy = sp.ModifiedBy,
                  ModifiedDate = sp.ModifiedDate,
                  Phone = sp.Phone,
                  Salutation = sp.Salutation,
                  SchoolId = sp.SchoolId,
                  StreetAddress = sp.StreetAddress,
                  Zip = sp.Zip
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetInstructor/{_InstructorID}")]
        public async Task<IActionResult> GetInstructor(int _InstructorID)
        {
            InstructorDTO? lst = await _context.Instructors
                .Where(x => x.InstructorId == _InstructorID)
                .Select(sp => new InstructorDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    FirstName = sp.FirstName,
                    InstructorId = sp.InstructorId,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Phone = sp.Phone,
                    Salutation = sp.Salutation,
                    SchoolId = sp.SchoolId,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


    }
}
