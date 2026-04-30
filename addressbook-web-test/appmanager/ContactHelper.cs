using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public ContactHelper CreateContact(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            SelectContact(v);
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper RemoveContact(int v)

        {
            manager.Navigator.GoToContactsPage();
            SelectContact(v);
            SubmitContactRemove();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactRemove()
        {
            driver.FindElement(By.Name("delete")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[19]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click(); 
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();

            return this;
        }

        public ContactHelper InitContactViewDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();

            return this;

        }

        public bool IsContactPresent()
        {
            manager.Navigator.GoToContactsPage();
            return IsElementPresent(By.Name("selected[]"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToContactsPage();
                ICollection<IWebElement> rows = driver.FindElements(By.CssSelector("tr[name='entry']"));

                foreach (IWebElement row in rows)
                {
                    ContactData contact = new ContactData(row.FindElement(By.XPath(".//td[3]")).Text);
                    contact.Lastname = row.FindElement(By.XPath(".//td[2]")).Text;
                    contactCache.Add(contact);
                }
            }
            return new List<ContactData>(contactCache);
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToContactsPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            string allEmails = cells[4].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(0);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                FirstEmail = firstEmail,
                SecondEmail = secondEmail,
                ThirdEmail = thirdEmail
            };
        }

        public string GetActualContactText(int index)
        {
                manager.Navigator.GoToContactsPage();
                InitContactViewDetails(index);

                string text = driver.FindElement(By.Id("content")).Text;
                var nonEmptyLines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                return string.Join("\r\n", nonEmptyLines);
        }

        public string GetExpectedContactText(int index)
        {
            ContactData contact = GetContactInformationFromEditForm(index);
            return GetExpectedContactText(contact);
        }

        public string GetExpectedContactText(ContactData contact)
        {
            var parts = new List<string>();

            parts.Add($"{contact.Firstname} {contact.Lastname}");

            if (!string.IsNullOrEmpty(contact.Address))
                parts.Add(contact.Address);

            var phones = new List<string>();
            if (!string.IsNullOrEmpty(contact.HomePhone)) phones.Add($"H: {contact.HomePhone}");
            if (!string.IsNullOrEmpty(contact.MobilePhone)) phones.Add($"M: {contact.MobilePhone}");
            if (!string.IsNullOrEmpty(contact.WorkPhone)) phones.Add($"W: {contact.WorkPhone}");
            if (phones.Any())
                parts.AddRange(phones);

            var emails = new List<string>();
            if (!string.IsNullOrEmpty(contact.FirstEmail)) emails.Add(contact.FirstEmail);
            if (!string.IsNullOrEmpty(contact.SecondEmail)) emails.Add(contact.SecondEmail);
            if (!string.IsNullOrEmpty(contact.ThirdEmail)) emails.Add(contact.ThirdEmail);
            if (emails.Any())
                parts.AddRange(emails);

            return string.Join("\r\n", parts);
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToContactsPage();
            string text = driver.FindElement(By.Id("search_count")).Text;
            return Int32.Parse(text);
        }

    }
}
