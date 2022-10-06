using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Dominio
{
    public class TareaEtiqueta
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid TareaId { get; set; }
        public Tarea Tarea { get; set; }
        
        public Guid EtiquetaId { get; set; }
        public Etiqueta Etiqueta { get; set; }
    }
}
