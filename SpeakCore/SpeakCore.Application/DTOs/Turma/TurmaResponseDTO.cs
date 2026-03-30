using SpeakCore.Application.DTOs.Disciplina;
using SpeakCore.Application.DTOs.Professor;
using SpeakCore.Domain.Enums;

namespace SpeakCore.Application.DTOs.Turma
{
    public class TurmaResponseDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int AnoLetivo { get; set; }
        public int CapacidadeMax { get; set; }
        public Nivel Nivel { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DisciplinaResponseDTO Disciplina { get; set; }
        public ProfessorResponseDTO Professor { get; set; }



    
      

    }
}
