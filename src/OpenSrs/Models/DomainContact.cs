using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenSrs.Models
{
    public sealed class DomainContact
    {
        [Required, StringLength(64)]
        public string FirstName { get; set; }

        [Required, StringLength(64)]
        public string LastName { get; set; }

        /// <summary>
        /// format: +CCC.NNNNNNNNNNxEEEE, where C = country code, N = phone number, and E = extension (optional).
        /// </summary>
        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [Required, StringLength(255)]
        // [EmailAddress]
        public string Email { get; set; }

        public string OrganizationName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [Required]
        [StringLength(64)]
        public string City { get; set; }

        [StringLength(32)]
        public string State { get; set; }

        [Required]
        [StringLength(2)]
        public string Country { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        public string Url { get; set; }

        public Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "first_name",     FirstName },
                { "last_name",      LastName },
                { "phone",          Phone },
                { "fax",            Fax },
                { "email",          Email },
                { "org_name",       OrganizationName },
                { "address1",       Address1 },
                { "address2",       Address2 },
                { "address3",       Address3 },
                { "city",           City },
                { "state",          State },
                { "country",        Country },
                { "postal_code",    PostalCode },
                { "url",            Url }
            };
        }
    }

    public enum ContactType
    {
        Owner,
        Admin,
        Tech,
        Billing
    }
}