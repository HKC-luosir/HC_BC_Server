
namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_portgradegroup
    {
        public int id { get; set; }
        public string eqpid { get; set; }
        public string portgradegroup { get; set; }
        public string portgrade { get; set; }
        public int priority { get; set; } = 0;
        public int enabled { get; set; } = 0;
    }
}
