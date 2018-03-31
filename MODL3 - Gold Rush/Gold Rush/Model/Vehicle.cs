using System;

namespace Gold_Rush.Model
{
    public abstract class Vehicle
    {
        public virtual int Load { get; set; }

        public virtual void Move()
        {
            throw new NotImplementedException();
        }
    }
}