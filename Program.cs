using System;
using System.IO;

namespace BuildZipGen
{
    class Program
    {
        public static string projectName, path;

        static void Main(string[] args)
        {
            // Console customisation
            Console.ForegroundColor = ConsoleColor.White;

            // Introduction
            Console.WriteLine("Build Zip Gen");
            Console.WriteLine("Created by Mattie");
            Console.WriteLine("------------------");

            // Get project name
            Console.Write("Input project name: ");
            projectName = Console.ReadLine();
            Console.WriteLine("Project name is: " + projectName);

            // Get output path
            Console.WriteLine("------------------");
            Console.Write("Input output path (include trailing slash, leave empty for exe directory): ");
            path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            } else
            {
                if (!Directory.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Directory does not exist, but will be created.");
                    Directory.CreateDirectory(path);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.WriteLine("Press any key to begin generation.");
            Console.ReadKey();
            Console.WriteLine();

            // Generate
            Generate("Windows");
            Generate("Linux");
            Generate("OSX");
            Generate("WebGL");
            Generate("Android");
        }

        public static void Generate(string platform)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Generate for " + platform + "? (press y for yes, anything else for no.)");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.KeyChar == 'y' || keyInfo.KeyChar == 'Y')
            {
                Console.WriteLine("Generating for " + platform);
                Directory.CreateDirectory(path + "/" + platform);
                StreamWriter file = File.CreateText(path + "/" + platform + "/Build.ps1");
                file.WriteLine("del " + projectName + "-" + platform + ".zip");
                file.WriteLine("Compress-Archive -Path ./* -DestinationPath ./" + projectName + "-" + platform + ".zip");
                file.Flush();
                file.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Generated folder and script for platform: " + platform);
                Console.ForegroundColor = ConsoleColor.White;
            } else
            {
                return;
            }
        }
    }
}
