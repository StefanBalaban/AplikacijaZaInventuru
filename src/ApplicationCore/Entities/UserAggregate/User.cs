using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        [Dto] [Get] [Post] [Put] [Required] public string FirstName { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public string LastName { get; set; }

        [Dto] [Get] [Post] [Put] public List<UserContactInfo> UserContactInfos { get; set; }
    }
}