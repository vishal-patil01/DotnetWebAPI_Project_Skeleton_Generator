using System.Diagnostics;

namespace ProjectCreator.Helpers
{
    public static class CommonHelper
    {
        public static void ExecuteStep(string stepDescription, Action stepAction)
        {
            Console.Write($"{stepDescription}... ");
            var cts = new CancellationTokenSource();
            Console.ForegroundColor = ConsoleColor.Blue;
            var progressTask = Task.Run(() => ShowProgress(stepDescription, cts.Token));

            stepAction.Invoke();
            cts.Cancel();
            progressTask.Wait();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Completed");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ShowProgress(string message, CancellationToken token)
        {
            char[] progressChars = { '|', '/', '-', '\\' };
            int progressIndex = 0;

            while (!token.IsCancellationRequested)
            {
                Console.Write(progressChars[progressIndex]);
                progressIndex = (progressIndex + 1) % progressChars.Length;
                Thread.Sleep(100);
                Console.Write("\b");
            }
        }

        public static void RunCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            var process = Process.Start(processInfo);
            process.WaitForExit();
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static void UpdateCsprojFile(string path, string[] folders)
        {
            string csprojContent = File.ReadAllText(path);
            string folderContent = string.Empty;
            foreach (var folder in folders)
            {
                folderContent += $@"
  <ItemGroup>
    <Folder Include=""{folder}"" />
  </ItemGroup>
";
            }
            folderContent += @"
</Project>
                            ";
            csprojContent = csprojContent.Replace("</Project>", folderContent);
            File.WriteAllText(path, csprojContent);
        }
    }
}
