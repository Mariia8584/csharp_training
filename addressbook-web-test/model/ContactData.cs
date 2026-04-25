using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string text;
        public string allPhones;

        //public string firstname;
        //public string lastname = "";

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData(string text)
        {
            this.text = text;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string FirstEmail { get; set; }

        public string SecondEmail { get; set; }

        public string ThirdEmail { get; set; }

        public string AllPhones 
        { 
            get 
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (Cleanup(HomePhone) + Cleanup(MobilePhone) + Cleanup(WorkPhone)).Trim(); 
                }
            }
            set 
            {
                AllPhones = value;
            } 
        }

        private string Cleanup(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
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
            return Firstname == other.Firstname 
                && Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            //return (Firstname?.GetHashCode() ?? 0) ^ (Lastname?.GetHashCode() ?? 0);
            return (Firstname + " " + Lastname).GetHashCode();
        }

        public override string ToString()
        {
            //return "firstname=" + Firstname;
            return Firstname + " " + Lastname;
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
    }
}
