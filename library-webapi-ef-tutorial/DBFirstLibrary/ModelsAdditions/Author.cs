﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstLibrary  // must be the same namespace as for the generated models!
{
    public partial class Author  // keyword partial extends an existing class Author generated by scaffolding
    {
        public override string ToString()
        {
            return Name;
        }
    }
}