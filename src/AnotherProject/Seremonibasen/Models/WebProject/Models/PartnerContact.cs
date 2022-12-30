using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
    [Table("PartnerContacts")]
    public class PartnerContact
    {
        public virtual required ICollection<CeremonyOrder> CeremonyOrders { get; set; }

        public required bool IsActive { get; set; }

        public required Guid PartnerId { get; set; }
        [ForeignKey(nameof(PartnerId))]
        public virtual required Partner Partner { get; set; }

        /// <summary>
        /// First name (max length 500)
        /// </summary>
        [StringLength(500)]
        public required string FirstName { get; set; }

        /// <summary>
        /// Surname (max length 500)
        /// </summary>
        [StringLength(500)]
        public required string Surname { get; set; }

        /// <summary>
        /// Email address (max length 300)
        /// </summary>
        [StringLength(300)]
        public required string Email { get; set; }

        /// <summary>
        /// Mobile number. (max length 20). only numeric values saved to database
        /// </summary>
        [StringLength(20)]
        public required string Mobile { get; set; }
        
        public required bool IsPrimaryContact { get; set; }

    }
