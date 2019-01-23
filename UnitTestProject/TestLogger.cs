using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class TestLogger
    {

        public int Id { get; }
        public string  Name { get; set; }


        public TestLogger(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public TestLogger()
        {
        }


        protected bool Equals(TestLogger other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestLogger) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }


        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }
}
