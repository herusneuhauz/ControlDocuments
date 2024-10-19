namespace ControlDocuments.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "A senha e a confirmação não correspondem.")]
        [Display(Name = "Confirme a Senha")]
        [DataType(DataType.Password)]
        public string ConfirmaSenha { get; set; }
    }
}
