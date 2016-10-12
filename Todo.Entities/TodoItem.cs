using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Entities
{
    [Table("TodoItems")]
    public class TodoItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanına max. {1} karakter girilmeli."),
            DisplayName("Konu"),
            Required(ErrorMessage = "{0} boş geçilemez.")]
        public string Subject { get; set; }

        [StringLength(250, ErrorMessage = "{0} alanına max. {1} karakter girilmeli."), 
            DisplayName("Açıklama")]
        public string Description { get; set; }

        [DisplayName("Tamamlandı")]
        public bool IsCompleted { get; set; }
    }
}
