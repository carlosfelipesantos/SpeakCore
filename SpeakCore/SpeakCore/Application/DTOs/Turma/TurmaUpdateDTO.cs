using SpeakCore.Domain.Enums;

namespace SpeakCore.Application.DTOs.Turma
{
    public class TurmaUpdateDTO
    {
        public int Numero { get; set; }
        public int AnoLetivo { get; set; }
        public Nivel Nivel { get; set; }
        public DateTime? DataFim { get; set; }

       public int ProfessorId { get; set; }


    }
}
