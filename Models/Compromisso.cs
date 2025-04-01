using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Crud_Agenda_WinForm.Models
{
    [Table("Compromissos")]
    public class Compromisso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [Required]
        public DateTime DataHora { get; set; }
    }
}
