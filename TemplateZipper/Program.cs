using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace TemplateZipper
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectLocation = new DirectoryInfo(args[0]);

            foreach (var template in Templates)
            {
                var zipFile = GetZip(template);
                if (zipFile.Exists)
                    zipFile.Delete();

                var command = string.Format("a -tzip -r \"{0}\" *", zipFile.FullName);

                new Process
                {
                    StartInfo = new ProcessStartInfo(ZipFile, command)
                    {
                        WorkingDirectory = template.FullName
                    }
                }.Start();
            }
        }

        public static string ZipFile
        {
            get
            {
                var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\7-Zip");
                return (string)key.GetValue("Path") + "7z.exe";
            }
        }

        public static DirectoryInfo ProjectLocation { get; set; }

        public static string SourceLocation
        {
            get { return Path.Combine(ProjectLocation.Parent.FullName, ProjectLocation.Name + ".Source"); }
        }

        public static DirectoryInfo[] Templates
        {
            get
            {
                var templateTypes = new[] { "ItemTemplates", "ProjectTemplates" };

                return templateTypes.Select    (x => new DirectoryInfo(Path.Combine(SourceLocation, x)))
                                    .SelectMany(y => y.GetDirectories("*", SearchOption.AllDirectories))
                                    .Where     (z => z.GetFiles("*.vstemplate").Any())
                                    .ToArray();
            }
        }

        public static FileInfo GetZip(DirectoryInfo template)
        {
            var file = template.FullName.Replace(SourceLocation, ProjectLocation.FullName);
            return new FileInfo(file + ".zip");
        }
    }
}
