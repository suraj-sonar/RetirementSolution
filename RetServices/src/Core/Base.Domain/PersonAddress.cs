using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain
{
    public class PersonAddress:BaseEntity
    {

        //update it as per the values "id	PersonId	Address_Line_1	Address_Line_2	City	State	ZipCode	Country	startDate	endDate	AddressTypeId	DateCreated	DateModified"
        public int PersonId { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AddressTypeId { get; set; }
    }

}
