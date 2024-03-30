using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class SiteRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteOwnerName { get; set; }
        public string SiteOwnerPhone { get; set; }
        //[ForeignKey("AlmuniumTypeId")]
        //public AlumniumType AlumniumType { get; set; }
        //public int AlmuniumTypeId { get; set; }
        public decimal MetersNumber { get; set; }
        public int DoorsNumber { get; set; }
        public int WindowsNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }

    }
}
