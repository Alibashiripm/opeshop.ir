using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    internal class FeaturesMetaData
    {
        [Key]
        public int FeatureID { get; set; }

        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FeatureTitle { get; set; }
    }
}