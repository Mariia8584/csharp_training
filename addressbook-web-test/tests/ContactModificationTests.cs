using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        
        public void ContactModificationTest()
        {
            if (!app.Contacts.IsContactPresent())
            {
                ContactData defaultContact = new ContactData("defaultContact");
                app.Contacts.CreateContact(defaultContact);
            }

            ContactData newData = new ContactData("WW");
            newData.Lastname = "xxx";

            app.Contacts.Modify(0, newData);
        }
    }
}
