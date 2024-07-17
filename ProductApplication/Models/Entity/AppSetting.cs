using System.ComponentModel.DataAnnotations;

namespace ProductApplication.Models.Entity
{
    public class AppSetting
    {
        [Key]
        public int Id { get; set; }
        public bool UseApi { get; set; }
    }
}
