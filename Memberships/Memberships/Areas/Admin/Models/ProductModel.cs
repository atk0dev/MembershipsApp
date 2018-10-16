// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductModel.cs" company="Andrii Tkach">
//   2018
// </copyright>
// <summary>
//   The product model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Memberships.Areas.Admin.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Memberships.Entities;

    /// <summary>
    /// The product model.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [MaxLength(2048)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image url.
        /// </summary>
        [DisplayName("Image Url")]
        [MaxLength(1024)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the product link text id.
        /// </summary>
        public int ProductLinkTextId { get; set; }

        /// <summary>
        /// Gets or sets the product type id.
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets the product link texts.
        /// </summary>
        [DisplayName("Product Link Texts")]
        public ICollection<ProductLinkText> ProductLinkTexts { get; set; }

        /// <summary>
        /// Gets or sets the product types.
        /// </summary>
        [DisplayName("Product Types")]
        public ICollection<ProductType> ProductTypes { get; set; }

        /// <summary>
        /// Gets the product link text.
        /// </summary>
        public string ProductLinkText
        {
            get
            {
                return this.ProductLinkTexts == null || this.ProductLinkTexts.Count().Equals(0)
                           ? string.Empty
                           : this.ProductLinkTexts.First(p => p.Id.Equals(this.ProductLinkTextId)).Title;
            }
        }

        /// <summary>
        /// Gets the product type.
        /// </summary>
        public string ProductType
        {
            get
            {
                return this.ProductTypes == null || this.ProductTypes.Count().Equals(0)
                           ? string.Empty
                           : this.ProductTypes.First(p => p.Id.Equals(this.ProductTypeId)).Title;
            }
        }
    }
}