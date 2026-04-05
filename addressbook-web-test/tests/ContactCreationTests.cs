using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitNewContactCreation();
            app.Contacts.FillContactForm(new ContactData("P", "P"));
            app.Contacts.SubmitContactCreation();
            app.Contacts.ReternToContactsPage();
            app.Auth.Logout();
        }
    }
}

