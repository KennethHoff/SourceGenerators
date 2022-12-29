using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
    [Table("Partners")]
    public class Partner
    {

        private const int MaxNameLength = 500;
        private const int MaxStreetAddressLength = 500;
        private const int MaxCityLength = MaxStreetAddressLength;
        private const int MaxPostCodeLength = 10;

        private const int OrganizationIdLength = 9;
        


        #region Parent Company
        
        public required Guid? ParentCompanyId { get; set; }
        
        [ForeignKey(nameof(ParentCompanyId))]
        public virtual required Partner ParentCompany { get; set;  }

        #endregion

        public required bool IsActive { get; set; }

        #region Contact Information

        /// <summary>
        /// Name (max length 500)
        /// </summary>
        [StringLength(MaxNameLength)]
        public required string Name { get; set; }
        
        /// <summary>
        /// DisplayName (Max length 500)
        /// </summary>
        [StringLength(MaxNameLength)]
        public required string DisplayName { get; set; }

        /// <summary>
        /// Email address (max length 300)
        /// </summary>
        [StringLength(300)]
        public required string Email { get; set; }

        /// <summary>
        /// Phone number. (max length 20). only numeric values saved to database
        /// </summary>
        [StringLength(20)]
        public required string Phone { get; set; }
        
        /// <summary>
        /// Organization number (max length 9)
        /// </summary>
        [StringLength(OrganizationIdLength)]
        public required string OrganizationId { get; set; }
        
        #endregion
        
        #region Address

        /// <summary>
        /// Invoice Address -- StreetAddress (max length 300)
        /// </summary>
        [StringLength(MaxStreetAddressLength)]
        public required string InvoiceStreetAddress { get; set; }

        /// <summary>
        /// Invoice Address -- Post code (max length 10)
        /// </summary>
		[StringLength(MaxPostCodeLength)]
        public required string InvoicePostCode { get; set; }

        /// <summary>
        /// Invoice Address -- City (max length 20)
        /// </summary>
		[StringLength(MaxCityLength)]
        public required string InvoiceCity { get; set; }
        
        /// <summary>
        /// Visiting Address -- City (max length 300)
        /// </summary>
        [StringLength(MaxCityLength)]
        public required string VisitingCity { get; set; }
        
        /// <summary>
        /// Visiting Address -- Post code (max length 10)
        /// </summary>
        [StringLength(MaxPostCodeLength)]
        public required string VisitingPostCode { get; set; }
        
        
        /// <summary>
        /// Visiting Address -- StreetAddress (max length 300)
        /// </summary>
        [StringLength(MaxStreetAddressLength)]
        public required string VisitingStreetAddress { get; set; }

        #endregion

        /// <summary>
        /// Comment (max length 2000)
        /// </summary>
        [StringLength(2000)]
        public required string Comment { get; set; }
        public required int TripletexId { get; set; }
        
        public required bool DoesNotAcceptInvoices { get; set; }
        
        public required DateTime? ServiceBusLastSentDate { get; set; }
        public required DateTime? ServiceBusLastReceivedDate { get; set; }
        public required string ServiceBusLastReceivedOrigin { get; set; }

        public virtual required ICollection<County> Counties { get; set; }
        public virtual required ICollection<PartnerContact> PartnerContacts { get; set; }
        public virtual required ICollection<PartnerType> PartnerTypes { get; set; }
        public virtual required ICollection<CeremonyOrder> CeremonyOrders { get; set; }
    }
