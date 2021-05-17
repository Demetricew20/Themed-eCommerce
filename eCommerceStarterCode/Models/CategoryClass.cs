using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Models
{
    public class CategoryTable : IdentityUser
    {

        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
    }
}