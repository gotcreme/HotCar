using System;
using HotCar.Entities.Enums;

namespace HotCar.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRoles Role { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string AboutMe { get; set; }
        public bool Inactive { get; set; }
        public UserPhoto Photo { get; set; }

        public string PhotoFormatted
        {
            get {
                var userPhoto = this.Photo;
                var base64 = Convert.ToBase64String(userPhoto.Photo);
                return string.Format("data:{0};base64,{1}", userPhoto.FileExtension, base64);
            }
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.Birthday.Value.Year;
                if (this.Birthday > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }

        public User()
        {
            Photo = new UserPhoto();
        }
    }
}
