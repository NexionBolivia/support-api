using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.Request
{
    public class OrganizationRequest
    {
        public string? OrganizationId { get; set; }
        public string? ParentOrganizationId { get; set; }
        public string? ProfileId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<string> Members { get; set; }
    }
}