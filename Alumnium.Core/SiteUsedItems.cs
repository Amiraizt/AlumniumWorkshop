using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class SiteUsedItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("SiteId")]
        public SiteRequest SiteRequest { get; set; }
        public int SiteId { get; set; }
        public int UsedQuantity { get; set; }
        public DateTime UsedDate { get; set; }
    }
}
