//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Remarks { get; set; }
        public string Province { get; set; }
        public string Hash { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ParkingLot ParkingLot { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
