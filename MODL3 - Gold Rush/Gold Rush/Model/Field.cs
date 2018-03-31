using System;

namespace Gold_Rush.Model
{
    public class Field
    {
        public string FieldId { get; set; }

        public Field Next { get; set; }

        public Dock Dock { get; set; }
    }
}