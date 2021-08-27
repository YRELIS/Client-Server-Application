using Newtonsoft.Json;
using System;


namespace ApplicationTZ.Core.Models
{
    public class Base
    {
        public Base()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        [System.ComponentModel.DataAnnotations.Key]
        [JsonProperty]
        public Guid Id { get; set; }
        [JsonProperty]
        public DateTime CreatedOn { get; set; }

        [JsonProperty]
        public DateTime UpdatedOn { get; set; }
    }
}