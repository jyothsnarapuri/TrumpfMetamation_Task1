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
        // Define the folder path and file path to be created.
        string folderPath = @"C:\TrumpfMetamation";
        string filePath = Path.Combine(folderPath, "Welcome.txt");
        string content = "Welcome to Trumpf Metamation!"; // Content to write in the file.

        try
        {
            // Step 1: Open File Explorer and navigate to C:\
            // This launches the File Explorer and navigates to the C:\ drive.
            Process.Start("explorer.exe", @"C:\");
            Thread.Sleep(2000); // Wait for 2 seconds to allow File Explorer to open.

            // Step 2: Simulate Ctrl+Shift+N to create a new folder
            // Uses the InputSimulator library to simulate the keyboard shortcut.
            var inputSimulator = new InputSimulator();
            inputSimulator.Keyboard.ModifiedKeyStroke(
                new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, // Hold Ctrl and Shift keys
                VirtualKeyCode.VK_N // Press 'N' key
            );

            Console.WriteLine("Keyboard shortcut 'Ctrl+Shift+N' pressed to create a new folder.");
            Console.WriteLine("Please rename the folder to 'TrumpfMetamation' manually and press Enter.");
            Console.ReadLine(); // Pauses to allow the user to rename the folder.

            // Verify if the folder is correctly renamed
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder '{folderPath}' not found. Please ensure the folder is renamed correctly.");
                return; // Exit the program if the folder is not found.
            }

            // Step 3: Create Welcome.txt file inside the folder
            // Write the predefined content into a new file within the created folder.
            File.WriteAllText(filePath, content);
            Console.WriteLine($"File created: {filePath}");

            // Step 4: Verify the content of the file
            // Read the file content and verify it matches the expected content.
            string fileContent = File.ReadAllText(filePath);
            if (fileContent == content)
            {
                Console.WriteLine("File content verified successfully.");
            }
            else
            {
                Console.WriteLine($"Content verification failed. Found: {fileContent}");
            }

            // Step 5: Delete the file
            // Deletes the file created in the folder.
            File.Delete(filePath);
            Console.WriteLine($"File deleted: {filePath}");

            // Step 6: Delete the folder
            // Deletes the folder created earlier.
            Directory.Delete(folderPath);
            Console.WriteLine($"Folder deleted: {folderPath}");

            // Step 7: Confirm deletion
            // Verify that the folder no longer exists.
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder deletion confirmed.");
            }
        }
        catch (Exception ex)
        {
            // Handles any errors that occur during execution.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
