namespace ApiServico.Models.Dtos;
using System.ComponentModel.DataAnnotations;


public class ChamadoDto
    {
         [Required(ErrorMessage ="O titulo é obrigatório")]
        public required string Titulo { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    public required string Descricao { get; set; }
    }

