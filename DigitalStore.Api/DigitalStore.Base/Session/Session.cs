﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Base
{
    public class Session
    {
        public string UserName { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
