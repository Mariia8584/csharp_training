using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]

        public void TestAddingContactToGroup()
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
                var availableContacts = ContactData.GetAll().Except(contactsInGroup).ToList();

                if (availableContacts.Count > 0)
                {
                    group = g;
                    contact = availableContacts.First();
                    break;
                }
            }
            if (group == null)
            {
                ContactData newContact = new ContactData("TempContact", DateTime.Now.Ticks.ToString());
                app.Contacts.CreateContact(newContact);
                group = GroupData.GetAll().First();
                contact = ContactData.GetAll().Except(group.GetContacts()).First();
            }

            List<ContactData> oldList = group.GetContacts();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
