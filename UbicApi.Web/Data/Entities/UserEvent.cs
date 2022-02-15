using System;

namespace UbicApi.Web.Data.Entities
{
    public class UserEvent
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime EventDate { get; set; }
        public int IdEvent { get; set; }
        public double Kb { get; set; }
        public Boolean Fact { get; set; }
        public DateTime? FactDate { get; set; }
        public string FactUserId { get; set; }

    }
}