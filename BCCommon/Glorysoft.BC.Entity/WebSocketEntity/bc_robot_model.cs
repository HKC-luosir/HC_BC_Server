using System;
namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class bc_robot_model
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public int modelposition { get; set; }
        public string uplinkname { get; set; }
        public string downlinkname { get; set; }
        public string portid { get; set; }
        public string sentoutname { get; set; }
        public string unitno { get; set; }
        public int robotmotion { get; set; } = 0;
        public int portgettype { get; set; }
        public bool exchangeenable { get; set; }
        public bool transferenable { get; set; }
        public bool getwaitenable { get; set; }
        public bool putwaitenable { get; set; }
        public string modelname { get; set; }
        public bool dualarm { get; set; }
        public int modelid { get; set; } = 0;
        public bool getenable { get; set; }
        public string groupname { get; set; }
        public int inpriority { get; set; }
        public int outpriority { get; set; }
        public int transferpriority { get; set; }
        public int exchangepriority { get; set; }
        public int putarm { get; set; }
        public int getarm { get; set; }
        public string transinname { get; set; }
    }
}
