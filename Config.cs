
namespace AnimationManager;

internal static class CONFIG
{
    public static string COMMAND_NAME = "anim";

    public static string ANIMATION_FOLDER =
        @"e:\Animation\Commissions";

    public static string[] RIG_FILES_R6 =
    {
        Path.Combine(ANIMATION_FOLDER, @".R6IK.blend")
    };

    public static string[] RIG_FILES_R15 =
    {
        Path.Combine(ANIMATION_FOLDER, @".R15IK.blend")
    };

    public static string TEMPLATE_FILE =
        Path.Combine(ANIMATION_FOLDER, @"template.rbxl");

    public static string OPTION_DESC =
        "Options:\n" +
        "   -s: don't open folder.      \n" +
        "   -l: only open latest file.       \n" +
        "   -t: only open template file.     \n" +
        "   -n: don't open any files inside the folder.";

    public static string NEW_DESC =
        "Create new animation folder.";

    public static string OPEN_DESC =
        "Open animation folder or file.";

    public static string CLEAR_DESC =
        "Clear all blender autosave files in folder.";

    public static string FOLDER_CHOICE_DIALOG =
        "More than 1 folder found matching provided name. Type the number of the desired folder and press Enter.";
}
