using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    using System.ComponentModel;

    [Table("Subscription")]
    public class Subscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [DisplayName("Registration Code")]
        [MaxLength(20)]
        public string RegistrationCode { get; set; }
    }
}