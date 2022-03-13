using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    internal class Product_FeaturesMetaData
    {
        public int PF_ID { get; set; }
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductID { get; set; }

        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int FeatureID { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Value { get; set; }
    }
}