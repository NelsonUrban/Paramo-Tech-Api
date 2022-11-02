using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.DataAccess.Repositories.Abstract
{
    public interface IFileRepository
    {
         string GetFilePath();
         StreamReader ReadUsersFromFile();
        void AppendLines(IEnumerable<string> lines);
    }
}
