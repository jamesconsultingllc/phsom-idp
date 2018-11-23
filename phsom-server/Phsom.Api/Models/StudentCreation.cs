namespace Phsom.Api.Models
{
    using Newtonsoft.Json;

    using Phsom.Api.Data;

    public class StudentCreation : Person
    {
        public int StudentId { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty("Address")]
        public AddressModel AddressModel { get; set; }
    }
}