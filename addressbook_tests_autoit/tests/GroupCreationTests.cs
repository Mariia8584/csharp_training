using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        /*[Test]
        public void TestGroupCreation()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            GroupData newGroup = new GroupData()
            {
                Name = "test"
            };

            app.Groups.Add(newGroup);
            
            List<GroupData> newGroups = app.Groups.GetGroupList();


            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }*/

        [Test]
        public void TestGroupCreation()
        {
            int oldCount = app.Groups.GetGroupCount();

            GroupData newGroup = new GroupData() { Name = "test" };
            app.Groups.Add(newGroup);

            int newCount = app.Groups.GetGroupCount();

            Assert.AreEqual(oldCount + 1, newCount,
                "Количество групп не увеличилось после создания новой группы");
        }

    }
}
