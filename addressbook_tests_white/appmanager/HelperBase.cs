using System.Collections.Generic;
using System.Linq;
using System.Text;
using addressbook_tests_white;

namespace addressbook_tests_white
{
    public class HelperBase
    {
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
        }
    }
}
