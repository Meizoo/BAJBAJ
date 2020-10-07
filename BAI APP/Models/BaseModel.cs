using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI_APP.Models
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}