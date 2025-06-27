    using System.ComponentModel.DataAnnotations;
using EntityFrameWrokCodefirstApp.Common;

namespace EntityFrameWrokCodefirstApp.Models
{
    public class Users : BaseEntity
    {  
        [Key] //auto incremented column in backend
        public int Id { get; set; }
        public string Name { get; set; }

        public string ContactNo { get; set; }

        public string Password {  get; set; }   

        //Navigation Property  , One to Many relationship , one user -> many order
        public List<Order> Orders { get; set; }

    }
}
