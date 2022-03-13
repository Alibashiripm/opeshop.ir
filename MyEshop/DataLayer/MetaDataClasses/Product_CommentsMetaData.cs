using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_CommentsMetaData
    {
        public int CommentID { get; set; }
        public int ProductID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }


        [Display(Name = "وب سایت")]
        public string WebSite { get; set; }


        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
    }
}