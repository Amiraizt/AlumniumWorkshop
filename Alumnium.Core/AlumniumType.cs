using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class AlumniumType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public decimal Quantity { get; set; }
        //[ForeignKey("UsedItemId")]
        //public Item Item { get; set; }
        //public int UsedItemId { get; set; }
        public string Status { get; set; }
    }
}
