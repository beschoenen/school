using System;

namespace Gold_Rush.Model
{
    public class Track : Field
    {
        public new virtual Track Next { get; set; }

        public Cart Cart { get; set; }
    }
}