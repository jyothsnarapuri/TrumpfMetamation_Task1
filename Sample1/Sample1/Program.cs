using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

class Program
{
    static void Main()
    {
        string folderName = "TrumpfMetamation";
        string fileName = "Welcome.txt";
        string content = "Welcome to Trumpf Metamation!";  // Content to write in the file
        string basePath = @"C:\";  // Base directory path
        string folderPath = Path.Combine(basePath, folderName);
        string filePath = Path.Combine(folderPath, fileName);

        try
        {
            // Step 1: Open File Explorer to C:\
            Process.Start("explorer.exe", basePath);
            Thread.Sleep(2000); // Allow File Explorer to open

            // Step 2: Create a new folder using Ctrl+Shift+N (keyboard shortcut)
            var inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.ModifiedKeyStroke(
                new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT },
                VirtualKeyCode.VK_N);
            Thread.Sleep(1000); // Wait for new folder

            // Step 3: Rename the folder to "TrumpfMetamation"
            inputSimulator.Keyboard.TextEntry(folderName);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(1000); // Allow renaming time

            // Verify folder was created programmatically
            if (!Directory.Exists(folderPath))
            {
                throw new Exception($"Failed to create folder: {folderPath}");
            }

            Console.WriteLine($"Folder created successfully at: {folderPath}");

            // Step 4: Open Notepad to write content
            Process.Start("notepad.exe", filePath);
            Thread.Sleep(1000); // Allow Notepad to open

            // Step 5: Clear any existing text (Ctrl+A and Delete)
            inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A); // Select all text
            Thread.Sleep(500); // Allow selection to complete
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.DELETE); // Delete selected text
            Thread.Sleep(500); // Allow delete to complete

            // Step 6: Use InputSimulator to write content slowly to ensure full text
            foreach (char c in content)
            {
                inputSimulator.Keyboard.TextEntry(c.ToString());
                Thread.Sleep(100); // Slow down typing to ensure each character is typed
            }
            Thread.Sleep(1000); // Wait to ensure the content is fully typed
            Console.WriteLine("Content written successfully.");

            // Step 7: Save the file using keyboard shortcuts (Ctrl+S)
            inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_S); // Ctrl+S to save
            Thread.Sleep(500);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN); // Press Enter to confirm the save
            Thread.Sleep(1000); // Allow saving to finish

            // Step 8: Wait for 20 seconds before closing Notepad
            Console.WriteLine("Waiting for 20 seconds before closing Notepad...");
            Thread.Sleep(20000); // Wait for 20 seconds

            // Step 9: Close Notepad automatically (Alt+F4)
            inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.F4); // Alt+F4 to close
            Thread.Sleep(1000); // Allow time for Notepad to close

            // Step 10: Open the file again to read its content
            Process.Start("notepad.exe", filePath); // Open Notepad with the file
            Thread.Sleep(2000); // Allow time for the file to open

            // Step 11: Verify the content by reading it programmatically
            string readContent = File.ReadAllText(filePath);
            Console.WriteLine("Reading file content automatically...");
            Console.WriteLine($"File Content: {readContent}");

            // Step 12: Verify the content
            if (readContent == content)
            {
                Console.WriteLine("File content verified successfully.");
            }
            else
            {
                Console.WriteLine("File content verification failed.");
            }

            // Step 13: Close Notepad after reading
            Process[] processes = Process.GetProcessesByName("notepad");
            foreach (var process in processes)
            {
                if (process.MainWindowTitle.Contains(fileName))
                {
                    process.Kill(); // Kill Notepad process
                    Console.WriteLine("Notepad closed automatically after reading the content.");
                    break;
                }
            }

            // Step 14: Cleanup (delete file and folder)
            File.Delete(filePath);
            Directory.Delete(folderPath);
            Console.WriteLine("File and folder deleted successfully.");

            // Step 15: Exit the program automatically
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Environment.Exit(1); // Exit with error code if an exception occurs
        }
    }
}
