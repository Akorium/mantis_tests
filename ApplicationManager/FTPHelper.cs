using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.FtpClient;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class FTPHelper : HelperBase
    {
        private readonly FtpClient client;
        public FTPHelper(ApplicationManager manager) : base(manager) 
        {
            client = new FtpClient
            {
                Credentials = new System.Net.NetworkCredential("mantis", "mantis"),
                Host = "localhost"
            };
            client.Connect();
        }
        public void BackupFile(String path)
        {
            String backupPath = path + ".bak";
            if (client.FileExists(backupPath))
            {
                return;
            }
            client.Rename(path, backupPath);
        }
        public void RestoreBackupFile(string path)
        {
            String backupPath = path + ".bak";
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            client.Rename(backupPath, path);

        }
        public void Upload(string path, Stream localFile) 
        {
            if (client.FileExists(path))
            {
                client.DeleteFile(path);
            }
            using (Stream ftpStream = client.OpenWrite(path)) 
            {
                byte[] buffer = new byte[8 * 1024];
                int count = localFile.Read(buffer, 0, buffer.Length);
                while (count > 0) 
                {
                    ftpStream.Write(buffer, 0, count);
                    count = localFile.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
