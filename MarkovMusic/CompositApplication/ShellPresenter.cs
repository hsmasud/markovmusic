using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositApplication
{
    public class ShellPresenter:APresenter, IShellPresenter
    {
        public ShellPresenter(IShellView view) : base(view)
        {
        }
    }
}