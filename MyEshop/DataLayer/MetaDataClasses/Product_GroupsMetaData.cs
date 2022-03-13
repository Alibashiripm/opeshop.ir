using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_GroupsMetaData
    {
        [Key]
        public int GroupID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
        public Nullable<int> ParentID { get; set; }
    }
}