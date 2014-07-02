using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaulaPires.Models
{
    public class ViewModelBase
    {
        public int ErrorNumber { get; set; }
        public bool ErrorAcesso { get; set; }

        public ViewModelBase()
        {
            Clear();
        }

        private void Clear()
        {
            ErrorNumber = 0;
            ErrorAcesso = false;
        }
    }
}