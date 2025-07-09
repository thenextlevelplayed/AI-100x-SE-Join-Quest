namespace ChineseChess.Tests.ReportGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var solutionDir = FindSolutionDirectory(Directory.GetCurrentDirectory());
            if (solutionDir == null)
            {
                Console.WriteLine("Error: Could not find solution directory.");
                return;
            }

            var trxPath = Path.Combine(solutionDir, "test_results.trx");
            var htmlPath = Path.Combine(solutionDir, "TestReport.html");

            if (!File.Exists(trxPath))
            {
                Console.WriteLine($"Error: Could not find test results file at '{trxPath}'");
                Console.WriteLine("Please run 'dotnet test --logger \"trx;LogFileName=../../test_results.trx\"' from the 'ChineseChess.Tests' directory first.");
                return;
            }

            HtmlReportGenerator.GenerateReport(trxPath, htmlPath);

            Console.WriteLine($"Report generated at: {htmlPath}");
        }

        private static string? FindSolutionDirectory(string startPath)
        {
            var dir = new DirectoryInfo(startPath);
            while (dir != null && !dir.GetFiles("*.sln").Any())
            {
                dir = dir.Parent;
            }
            return dir?.FullName;
        }
    }
} 