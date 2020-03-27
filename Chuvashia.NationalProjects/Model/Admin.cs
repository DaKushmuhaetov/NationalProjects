using System;

namespace Chuvashia.NationalProjects.Model
{
    public sealed class Admin
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AdminRole Role { get; set; }
    }
}
