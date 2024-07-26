
using System.Diagnostics;

namespace AnimationManager;

// Standard helpers
internal static class Helpers
{
    internal static string ChooseFolder(string[] options)
    {
        Console.WriteLine(CONFIG.FOLDER_CHOICE_DIALOG);
        ListDirs(options);

        ushort input = ushort.MaxValue;

        while (input > options.Length)
            input = Convert.ToUInt16(Console.ReadLine());

        return options[input - 1];
    }

    internal static void ListDirs(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            string path = options[i];
            Console.WriteLine($"   {i + 1}: " + GetName(path));
        }
    }

    internal static string[]? GetFolders(string name, bool fullMatch = false)
    {
        string[] dirs = Directory.GetDirectories(CONFIG.ANIMATION_FOLDER);
        List<string>? matchingDirsList = new();

        foreach (string dir in dirs)
        {
            var dirName = dir.Substring(CONFIG.ANIMATION_FOLDER.Length + 1);

            if (
                fullMatch && dirName.ToLower() == name.ToLower()
                ||           dirName.ToLower().Contains(name.ToLower())
                )
            {
                matchingDirsList.Add(dir);
            }
        }

        string[] matchingDirs = matchingDirsList.ToArray();
        matchingDirsList.Clear();

        return (matchingDirs.Length == 0) ? null : matchingDirs;
    }

    internal static void Launch(string filePath)
    {
        FileAttributes attr = File.GetAttributes(filePath);

        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", filePath);
        else
        {
            using (Process p = new Process())
            {
                p.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = filePath
                };

                p.Start();
            }
        }
        //Process.Start(filePath);
    }

    internal static string CreateAnimationFolder(string name)
    {
        DirectoryInfo folder = Directory.CreateDirectory(Path.Combine(CONFIG.ANIMATION_FOLDER, name));
        return folder.FullName;
    }

    internal static void CopyRigs(bool r6, bool r15, string targetPath)
    {
        var filesToCopy = new List<string>();

        if (r6)
            filesToCopy.AddRange(CONFIG.RIG_FILES_R6);
        if (r15)
            filesToCopy.AddRange(CONFIG.RIG_FILES_R15);

        filesToCopy.Add(CONFIG.TEMPLATE_FILE);

        foreach (string file in filesToCopy)
        {
            string name = file.Substring(CONFIG.ANIMATION_FOLDER.Length + 1);
            File.Copy(file, Path.Combine(targetPath, name), false);
        }
    }

    internal static string GetName(string path)
    {
        return path.Substring(CONFIG.ANIMATION_FOLDER.Length + 1);
    }

    internal static void LaunchLatest(string folderPath)
    {
        var path = new DirectoryInfo(folderPath);
        string newestFile = path.GetFiles("*.blend").OrderByDescending(f => f.LastWriteTime).First().FullName;
        Launch(newestFile);
    }

    internal static void LaunchTemplate(string folderPath)
    {
        var path = new DirectoryInfo(folderPath);
        string newestFile = path.GetFiles("*.rbxl").OrderByDescending(f => f.LastWriteTime).First().FullName;
        Launch(newestFile);
    }
}
