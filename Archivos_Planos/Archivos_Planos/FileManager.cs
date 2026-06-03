using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Flat_Files
{
    public static class FileManager
    {
        public static string BasePath = @"C:\Flat_Files";

        public static string UsersFile =>
            Path.Combine(BasePath, "User.txt");

        public static string CustomersFile =>
            Path.Combine(BasePath, "Log.txt");

        public static string ConfigFile =>
            Path.Combine(BasePath, "Config.txt");

        public static void CreateStructure()
        {
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);

            if (!File.Exists(UsersFile))
            {
                File.WriteAllText(
                    UsersFile,
                    "1,admin,12345,true" +
                    "\n2,jzuluaga,P@ssw0rd123!,true" +
                    "\n3,mbedoya,S0yS3gur02025*,false" +
                    Environment.NewLine);
            }

            if (!File.Exists(CustomersFile))
            {
                File.WriteAllText(
                    CustomersFile,
                    "ID,FirstName,LastName,Phone,City,Balance" +
                    Environment.NewLine);
            }

            if (!File.Exists(ConfigFile))
            {
                File.WriteAllText(
                    ConfigFile,
                    "PATH=C:\\Flat_Files" +
                    Environment.NewLine +
                    "MAX_ATTEMPTS=3");
            }
        }
    }
}