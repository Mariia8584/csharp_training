using System;
using System.Collections.Generic;
using System.Linq;
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

            List<GroupData> OldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            OldGroups.RemoveAt(0);
            Assert.AreEqual(OldGroups, newGroups);
        }
    }
}
