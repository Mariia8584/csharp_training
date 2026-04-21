using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string firstname;
        public string lastname = "";

        public ContactData(string firstname)
        {
            this.firstname = firstname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other)) 
            {
                return true;
            }
            return Firstname == other.firstname && Lastname == other.lastname;
        }

        public override int GetHashCode()
        {
            return (Firstname?.GetHashCode() ?? 0) ^ (Lastname?.GetHashCode() ?? 0);
        }

        public override string ToString()
        {
            return "firstname=" + Firstname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            int result = Lastname.CompareTo(other.Lastname);
            if (result != 0)
            {
                return result;
            }
            return Firstname.CompareTo(other.Firstname);
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}
