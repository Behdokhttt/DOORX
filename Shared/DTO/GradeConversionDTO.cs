﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOOR.Shared.DTO
{
    public class GradeConversionDTO
    {

        [Key]
        [Precision(8)]
        public int SchoolId { get; set; }
        [Key]
        [StringLength(2)]
        [Unicode(false)]
        public string LetterGrade { get; set; } = null!;
        public decimal GradePoint { get; set; }
        [Precision(3)]
        public byte MaxGrade { get; set; }
        [Precision(3)]
        public byte MinGrade { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
