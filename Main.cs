
using static AnimationManager.Helpers;

namespace AnimationManager;

internal static class Anim
{
    // States for managing what files should be opened after command
    internal enum OpenState : byte
    {
        None,
        Latest,
        Template,
        Both
    }

    static OpenState SelectedOpenState = OpenState.None;
    static bool bOpenFolder = true;

    internal static void NewFile(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("'new' requires at least 2 arguments. Use '" + CONFIG.COMMAND_NAME + " help' for usage.");
            return;
        }
        string rigType = args[1];
        string newFolderName = args[2];
        string? newFolderPath;

        if (rigType != "a" && rigType != "r6" && rigType != "r15")
        {
            Console.WriteLine("Invalid rigType.");
            return;
        }

        string[]? folders = GetFolders(newFolderName, true);

        if (folders != null)
        {
            newFolderPath = FoundFolders(folders);
            if (newFolderPath == null)
                return;
            StartAllProcesses(newFolderPath);
            return;
        }

        newFolderPath = CreateAnimationFolder(newFolderName);
        CopyRigs(rigType == "a" || rigType == "r6", rigType == "a" || rigType == "r15", newFolderPath);

        StartAllProcesses(newFolderPath);
    }

    private static string? FoundFolders(string[] folders)
    {
        ListDirs(folders);
        Console.WriteLine("Folder(s) already found. Open? Y/N");

        ConsoleKeyInfo? input = Console.ReadKey();
        if (input != null && input.Value.Key == ConsoleKey.Y)
        {
            Console.WriteLine();
            return ChooseFolder(folders);
        }
        return null;
    }

    internal static void OpenFile(string[] args)
    {
        if (args.Length < 2)
        {
            Launch(CONFIG.ANIMATION_FOLDER);
            return;
        }

        string folderName = args[1];
        string[]? result = GetFolders(folderName);
        string path;

        if (result == null)
            return;

        if (result.Length == 0)
        {
            Console.WriteLine("No such directory found: " + folderName);
            return;
        }
        path = result[0];

        if (result.Length > 1)
        {
            path = ChooseFolder(result);
        }

        StartAllProcesses(path);
    }

    internal static void ClearFolder(string[] args)
    {

    }

    internal static void SetOptions(OpenState state, bool folder)
    {
        SelectedOpenState = state;
        bOpenFolder = folder;
    }

    private static void StartAllProcesses(string folderPath)
    {
        if (bOpenFolder)
        {
            Launch(folderPath);
        }

        if (SelectedOpenState == OpenState.None)
            return;
        else if (SelectedOpenState == OpenState.Both)
        {
            LaunchLatest(folderPath);
            LaunchTemplate(folderPath);
        }
        else if (SelectedOpenState == OpenState.Latest)
        {
            LaunchLatest(folderPath);
        }
        else if (SelectedOpenState == OpenState.Template)
        {
            LaunchTemplate(folderPath);
        }
    }
}