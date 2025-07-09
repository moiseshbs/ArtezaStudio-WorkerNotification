using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtezaStudio.WorkerNotification.Models
{
    public class CurtidaEvent
    {
        public Guid UsuarioId { get; set; }

        public Guid PublicacaoId { get; set; }
        
        public DateTime DataCurtida { get; set; }
    }
}
