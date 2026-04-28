using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactDetailInformation()
        {
            ContactData fromViewPage = app.Contacts.GetContactInformationFromViewPage(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            string Clean(string text)
            {
                if (text == null) return "";
                return new string(text.Where(c => char.IsLetterOrDigit(c) || c == '+').ToArray());
            }

            Assert.AreEqual(fromForm.Firstname, fromViewPage.Firstname);
            Assert.AreEqual(fromForm.Lastname, fromViewPage.Lastname);
            Assert.AreEqual(Clean(fromForm.Address), Clean(fromViewPage.Address));
            Assert.AreEqual(Clean(fromForm.AllPhones), Clean(fromViewPage.AllPhones));
            Assert.AreEqual(Clean(fromForm.AllEmails), Clean(fromViewPage.AllEmails));
        }
    }
}
