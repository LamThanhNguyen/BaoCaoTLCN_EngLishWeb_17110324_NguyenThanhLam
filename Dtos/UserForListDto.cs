﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Dtos
{

    // Kiểu dữ liệu chứa các User trong một List.
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
