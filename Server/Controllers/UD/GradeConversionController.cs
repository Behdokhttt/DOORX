﻿using DOOR.EF.Data;
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
    public class GradeConversionController: BaseController
    {
        public GradeConversionController(DOOROracleContext _DBcontext,
    OraTransMsgs _OraTransMsgs)
    : base(_DBcontext, _OraTransMsgs)

        {
        }
        [HttpGet]
        [Route("GetGradeConversion")]
        public async Task<IActionResult> GetGradeConversion()
        {
            List<GradeConversionDTO> lst = await _context.GradeConversions
                .Select(sp => new GradeConversionDTO
                {
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                GradePoint = sp.GradePoint,
                LetterGrade = sp.LetterGrade,
                MaxGrade = sp.MaxGrade,
                MinGrade = sp.MinGrade,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
                SchoolId = sp.SchoolId
                }).ToListAsync();
            return Ok(lst);
        }

        [HttpGet]
        [Route("GetGradeConversion/{_GradeConversionID}")]
        public async Task<IActionResult> GetGradeConversion(int _GradeConversionID)
        {
            List<GradeConversionDTO> lst = await _context.GradeConversions
                .Where(x => x.GradeConversionID == _GradeConversionID)
                .Select(sp => new GradeConversionDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    GradePoint = sp.GradePoint,
                    LetterGrade = sp.LetterGrade,
                    MaxGrade = sp.MaxGrade,
                    MinGrade = sp.MinGrade,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId
                }).ToListAsync();
            return Ok(lst);
        }
    }
}
