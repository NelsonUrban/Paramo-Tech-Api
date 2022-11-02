﻿using System;
using System.IO;

namespace Sat.Recruitment.Api.Controllers
{
    public  class UsersController2
    {
        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
