using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Memberships.Entities;

    public class ProductModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [DisplayName("Image Url")]
        [MaxLength(1024)]
        public string ImageUrl { get; set; }

        public int ProductLinkTextId { get; set; }

        public int ProductTypeId { get; set; }

        [DisplayName("Product Link Texts")]
        public ICollection<ProductLinkText> ProductLinkTexts { get; set; }

        [DisplayName("Product Types")]
        public ICollection<ProductType> ProductTypes { get; set; }

        public string ProductType {
            get
            {
                return this.ProductTypes == null || this.ProductTypes.Count().Equals(0)
                    ? string.Empty
                    : this.ProductTypes.First(p => p.Id.Equals(this.ProductTypeId)).Title;
            }
        }

        public string ProductLinkText
        {
            get
            {
                return this.ProductLinkTexts == null || this.ProductLinkTexts.Count().Equals(0)
                    ? string.Empty
                    : this.ProductLinkTexts.First(p => p.Id.Equals(this.ProductLinkTextId)).Title;
            }
        }
    }
}