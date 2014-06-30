using System.ComponentModel.DataAnnotations;

namespace MySportsStore.Model
{
    public class ShippingDetail
    {
        [Required(ErrorMessage = "必填")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "地址")]
        public string Line { get; set; }
    }
}