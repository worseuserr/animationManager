
using static AnimationManager.Anim;

namespace AnimationManager;

internal static class Input
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0] == "help")
        {
            ShowHelp();
            return;
        }

        var options = ParseOptions(args);
        SetOptions(options.Item1, options.Item2);

        if (args[0] == "new")
            NewFile(args);

        else if (args[0] == "open" || args[0] == "openf")
            OpenFile(args);

        else if (args[0] == "clear")
            ClearFolder(args);

        else
            Console.WriteLine("Invalid command: " + args[0] + $"\nUse '{CONFIG.COMMAND_NAME}' or '{CONFIG.COMMAND_NAME} help' for usage.");
    }

    internal static (OpenState, bool, bool) ParseOptions(string[] args)
    {
        bool openFolder = true;
        bool quitAfter = false;
        OpenState openState = OpenState.Both;

        foreach (string arg in args)
        {
            if (string.IsNullOrEmpty(arg) || (arg.Length > 1))
                continue;

            if (arg == "s")
                openFolder = false;

            else if (arg == "l" && openState != OpenState.None)
                openState = OpenState.Latest;

            else if (arg == "t" && openState != OpenState.None)
                openState = OpenState.Template;

            else if (arg == "n")
                openState = OpenState.None;
        }

        return (openState, openFolder, quitAfter);
    }
    private static void ShowHelp()
    {
        Console.WriteLine(
            $"Usage:\n" +
            $"  {CONFIG.COMMAND_NAME} new [r6|r15|a] [folderName]           : {CONFIG.NEW_DESC}\n" +
            $"  {CONFIG.COMMAND_NAME} open [folderName]                     : {CONFIG.OPEN_DESC}\n" +
            $"  {CONFIG.COMMAND_NAME} openf [folderName] [filename(blend)]  : {CONFIG.OPEN_DESC}\n" +
            $"  {CONFIG.COMMAND_NAME} clear [folderName]                    : {CONFIG.CLEAR_DESC}\n\n" +
            $"{CONFIG.OPTION_DESC}"
        );
    }
}
