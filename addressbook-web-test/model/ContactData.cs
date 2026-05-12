using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string text;
        public string allPhones;
        public string allEmails;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData(string text)
        {
            Firstname = text;
            Lastname = "";
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "address")] 
        public string Address { get; set; }

        [Column(Name = "home")] 
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string FirstEmail { get; set; }

        [Column(Name = "email2")]
        public string SecondEmail { get; set; }

        [Column(Name = "email3")]
        public string ThirdEmail { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public string AllEmails
        { 
            get 
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanupEmail(FirstEmail) + CleanupEmail(SecondEmail) + CleanupEmail(ThirdEmail)).Trim(); 
                }
            }
            set 
            {
                allEmails = value;
            } 
        }

        private string CleanupEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return Regex.Replace(email, " ", "") + "\r\n";
        }

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
                allPhones = value;
            }
        }

        private string Cleanup(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[^0-9+]", "") + "\r\n";
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
            return ("firstname=" + Firstname + "\nlastname=" + Lastname).GetHashCode();
        }

        public override string ToString()
        {
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

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }

    }
}
