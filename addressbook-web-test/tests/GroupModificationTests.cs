using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            if (!app.Groups.IsGroupPresent())
            {
                GroupData defaultGroup = new GroupData("defaultGroup");
                app.Groups.Create(defaultGroup);
            }

            GroupData newData = new GroupData("nnn");
            newData.Header = null;
            newData.Footer = null;

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModify = oldGroups[0];

            GroupData oldData = oldGroups[0];

            app.Groups.Modify(toBeModify, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups) 
            {
                if (group.Id == oldData.Id) 
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
