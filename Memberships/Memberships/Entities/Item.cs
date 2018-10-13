using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
	using System.ComponentModel;
	using System.Web.Mvc;

	[Table("Item")]
	public class Item
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(255)]
		public string Title { get; set; }

		[MaxLength(2048)]
		public string Description { get; set; }

		[MaxLength(1024)]
		public string Url { get; set; }

		[DisplayName("Image Url")]
		[MaxLength(1024)]
		public string ImageUrl { get; set; }

		[AllowHtml]
		public string Html { get; set; }

		[DisplayName("Wait Days")]
		[DefaultValue(0)]
		public int WaitDays { get; set; }

		public string HtmlShort 
		{
			get 
			{
				return this.Html == null || this.Html.Length < 50 ? this.Html : this.Html.Substring(0, 50);
			}
		}

		public int ProductId { get; set; }

		public int ItemTypeId { get; set; }

		public int SectionId { get; set; }

		public int PartId { get; set; }

		public bool IsFree { get; set; }

		[DisplayName("Item Type")]
		public ICollection<ItemType> ItemTypes { get; set; }

		[DisplayName("Section")]
		public ICollection<Section> Sections { get; set; }

		[DisplayName("Parts")]
		public ICollection<Part> Parts { get; set; }
	}
}