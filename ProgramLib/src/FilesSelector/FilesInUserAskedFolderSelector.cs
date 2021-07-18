using System.Linq;
public class FilesInUserAskedFolderSelector : FilesSelector
{
    public FilesInUserAskedFolderSelector(string? MaybeSpecifiedDirectory = null) =>
        this.MaybeSpecifiedDirectory = MaybeSpecifiedDirectory;

    public FilesToCompare SelectFilesForFindingSimilarProblems()
    {
        string SearchPath = MaybeSpecifiedDirectory ?? AskUserForTheDirectory();
        var files = System.IO.Directory.EnumerateFiles(SearchPath)
                .Where((string fileName) => fileName.EndsWith(".tex"))
                .ToList();
        return new FilesToCompare(files);
    }

    private static string AskUserForTheDirectory()
    {
        string curDir = System.IO.Directory.GetCurrentDirectory();
        System.Console.WriteLine("Please enter the directory with files; otherwise " +
                                $"the current directory will be used ({curDir})");
        string? input = System.Console.ReadLine();
        if (input == "") return curDir;
        else return input ?? curDir;
    }

    private readonly string? MaybeSpecifiedDirectory;
}