using System;

namespace Support.API.Services.KoboFormData
{
    /// <summary>
    /// This entity maps to KoboForms' db table: auth_user
    /// </summary>
    public class KoboUser
    {
        // id (int)
        public int Id { get; set; }
        // last_login (datetime)
        public DateTime? LastLogin { get; set; }
        // is_superuser (bool)
        public bool IsSuperUser { get; set; }
        // username	(char)
        public string UserName { get; set; }
        // first_name (char)
        public string FirstName { get; set; }
        // last_name (char)
        public string LastName { get; set; }
        // email (char)
        public string Email { get; set; }
        // is_staff	(bool)
        public bool IsStaff { get; set; }
        // is_active (bool)
        public bool IsActive { get; set; }
        // date_joined (datetime)
        public DateTime DateJoined { get; set; }
    }
}
