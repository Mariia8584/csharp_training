using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]

        public void TestRemovingContactFromGroup()
        {
            if (GroupData.GetAll().Count == 0)
            {
                GroupData newGroup = new GroupData("GroupForTest");
                app.Groups.Create(newGroup);
            }

            if (ContactData.GetAll().Count == 0)
            {
                ContactData newContact = new ContactData("ContactForTest", "LastName");
                app.Contacts.CreateContact(newContact);
            }

            GroupData group = null;
            ContactData contact = null;

            foreach (GroupData g in GroupData.GetAll())
            {
                var contactsInGroup = g.GetContacts();

                if (contactsInGroup.Count > 0)
                {
                    group = g;
                    contact = contactsInGroup.First();
                    break;
                }
            }

            if (group == null)
            {
                group = GroupData.GetAll().First();
                contact = ContactData.GetAll().First();
                app.Contacts.AddContactToGroup(contact, group);
            }

            List<ContactData> oldList = group.GetContacts();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList.Count, newList.Count);
        }
    }
}
