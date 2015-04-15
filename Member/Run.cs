using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.WinForm;

namespace Member
{
    public class Run
    {
        public bool Show(BaseMainForm frm)
        {
            AddMember add = new AddMember();
            add.m_frm = frm;
            return frm.LoadFormToPanel(add);
        }

        public bool SearchShow(BaseMainForm frm)
        {
            MemberSearch search = new MemberSearch();
            search.m_frm = frm;
            return frm.LoadFormToPanel(search);
        }
    }
}
