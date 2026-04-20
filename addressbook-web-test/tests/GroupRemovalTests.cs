using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]        
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsGroupPresent())
            {
                GroupData defaultGroup = new GroupData("defaultGroup");
                app.Groups.Create(defaultGroup);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Remove(0);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
