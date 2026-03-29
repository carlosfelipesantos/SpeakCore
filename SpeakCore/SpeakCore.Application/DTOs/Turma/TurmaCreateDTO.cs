using SpeakCore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpeakCore.Application.DTOs.Turma
{
    public class TurmaCreateDTO
    {
        [Required]
        public int Numero { get; set; }
        public int AnoLetivo { get; set; }

        [Range(1,5)]
        public int CapacidadeMax { get; set; }
        public Nivel Nivel { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }

    }
}
