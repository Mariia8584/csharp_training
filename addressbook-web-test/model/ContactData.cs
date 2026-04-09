using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        public string firstname;
        public string lastname;

        public ContactData(string firstname)
        {
            this.firstname = firstname;
        }
        
        public string Firstname
        { 
            get 
            { 
                return firstname; 
            }
            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }
    }
}
