using System.ComponentModel.DataAnnotations;

namespace EntityFrameWrokCodefirstApp.Models
{
    public class Users
    {  
        [Key] //auto incremented column in backend
        public int Id { get; set; }
        public string Name { get; set; }

        public string ContactNo { get; set; }

        public string Password {  get; set; }   

    }
}
