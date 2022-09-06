using Example01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Example01.Context
{
    
        [MetadataType(typeof(ProductMasterData))]
        public partial class Product_2119110193
        {
            [NotMapped]
            public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
        }
        [MetadataType(typeof(CategoryMasterData))]
        public partial class Category_2119110193
        {
            [NotMapped]
            public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
        }
        [MetadataType(typeof(BrandMasterData))]
        public partial class Brand_2119110193
        {
            [NotMapped]
            public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
        }
        [MetadataType(typeof(UserMasterData))]
        public partial class Users_2119110193
        {
            [NotMapped]
            public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
        }

}