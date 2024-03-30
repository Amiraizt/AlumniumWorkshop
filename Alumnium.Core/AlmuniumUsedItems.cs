using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class AlmuniumUsedItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("AluminumTypeId")]
        public AlumniumType AlumniumType { get; set; }
        public int AluminumTypeId { get; set; }
        public int ItemQuantity { get; set; }
    }
}
