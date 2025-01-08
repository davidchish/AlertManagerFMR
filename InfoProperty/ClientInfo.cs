using System.ComponentModel.DataAnnotations;

namespace InfoProperty
{
    public class ClientInfo
    {
        [Required]
        public int ID { get; set; }

        public string ClientName { get; set; }

        public List<RuleInfo> RuleList { get; set; }


       
    }


    public class RuleInfo
    {
        [Required]
        public int ID { get; set; }
        public int Price { get; set; }
        public FlightInfo FlightParameters { get; set; }

    }

}
