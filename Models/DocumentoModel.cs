using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlDocuments.Models
{
    public class DocumentoModel
    {
        [Key]
        public int ID_Documento { get; set; }

        public int ID_Loja { get; set; }

        [Required(ErrorMessage = "O número do documento é obrigatório.")]
        public required string Numero_Documento { get; set; }

        [Required(ErrorMessage ="A descrição é obrigatória.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]

        public DateTime Data_Vencimento { get; set; }

        public DateTime Data_Lancamento { get; private set; } = DateTime.Now;

        [ForeignKey("ID_Loja")]
        public LojaModel Lojas { get; set; }
    }
}
