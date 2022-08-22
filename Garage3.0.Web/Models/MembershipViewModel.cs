using Garage3._0.Core;
using System.ComponentModel;

namespace Garage3._0.Web.Models
{
    public class VehicleViewViewModel
    {
        [DisplayName("Name")]
        public string? Name { get; set; }
        [DisplayName("Last name")]
        public string? LastName { get; set; }
      
        public int Id { get; set; }

    }
}
