using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;

namespace ApplicationCore.Entities.UserAggregate
{
    public class UserContactInfo : BaseEntity
    {
        [Dto] [Get] [Post] [Put] [Required] public string Contact { get; set; }
    }
}