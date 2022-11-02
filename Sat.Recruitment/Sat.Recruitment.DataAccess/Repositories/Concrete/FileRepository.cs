using Sat.Recruitment.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.DataAccess.Repositories.Concrete
{
    public class FileRepository : IFileRepository
    {
        public FileRepository()
        {

        }

        public string GetFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Files/Users.txt");
        }
        public StreamReader ReadUsersFromFile()
        {
            var path = GetFilePath();

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public async void AppendLines(IEnumerable<string> lines)
        {
            await File.AppendAllLinesAsync(GetFilePath(), lines);
        }
    }
}
