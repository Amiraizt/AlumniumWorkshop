using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class SiteUsedAlumimum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("AlmuniumTypeId")]
        public AlumniumType AlumniumType { get; set; }
        public int AlmuniumTypeId { get; set; }
        [ForeignKey("SiteRequestId")]
        public SiteRequest SiteRequest { get; set; }
        public int SiteRequestId { get; set; }
    }
}
