﻿using System;
using System.Collections.Generic;
using System.Text;
using TeacherControl.Core.AuditableModels;

namespace TeacherControl.Core.Models
{
    public class UserInfo : IModificationAudit
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string CodeId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
