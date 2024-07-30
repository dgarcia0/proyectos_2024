using System.ComponentModel.DataAnnotations;

namespace PreguntasYRespuestas.Data.Models
{
    public class QuestionPostRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is empty")]
        public string Content { get; set; }
    }
}
