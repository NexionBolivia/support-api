using Support.API.Services.KoboData;
using Support.API.Services.Models;
using Support.API.Services.Models.Request;
using System.Collections.Generic;

namespace Support.API.Services.Services
{
    public interface IOrganizationService
    {
        bool CreateUpdateOrganization(OrganizationRequest org);
        IEnumerable<OrganizationResponse> GetAll();
        bool DeleteOrganization(string organizationId);
    }
}