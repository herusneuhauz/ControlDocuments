using System.ComponentModel.DataAnnotations;

namespace ControlDocuments.Models
{
    public class LojaModel
    {
        [Key]
        public int ID_Loja { get; set; }

        [Required(ErrorMessage = "O nome da loja é obrigatório.")]
        public required string Nome_Loja { get; set; }

        public virtual ICollection<DocumentoModel> Documentos { get; set; }
    }
}
