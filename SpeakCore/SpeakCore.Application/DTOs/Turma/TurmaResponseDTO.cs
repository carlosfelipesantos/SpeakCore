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

        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }
      

    }
}
