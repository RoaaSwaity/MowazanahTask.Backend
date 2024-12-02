
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Stock
    {

        #region Props
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public required string Time { get; set; }

        [MaxLength(100)]
        public required string SymbolName { get; set; }

        [MaxLength(100)]
        public required decimal MidPrice { get; set; }
        #endregion Props        
    }
}
