﻿using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

#nullable disable
namespace GestorTareas.Dominio
{
    public class Etiqueta
    {
        public Etiqueta() => Tareas = new HashSet<Tarea>();
        
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string Title { get; set; }

        public ICollection<Tarea> Tareas { get; set; }
    }
}
