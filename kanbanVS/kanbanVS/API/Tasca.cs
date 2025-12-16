using System;

namespace kanbanVS.Model
{
    public class Tasca
    {
        public long Codi { get; set; }
        public string Titol { get; set; }
        public string Descripcio { get; set; }
        public DateTime DataInici { get; set; }
        public DateTime DataFinal { get; set; }
        public string Prioritat { get; set; }
        public int IdResp { get; set; }
        public string Estat { get; set; }
    }
}