using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    public class BookingData
    {
        [JsonProperty("bookingid")]
        public int BookingId {  get; set; }

        [JsonProperty("booking")]
        public BookingDetails? Booking { get; set; }

    }
}
