using System;        // Importing System namespace for basic functionalities
using System.IO;     // Importing System.IO namespace to work with files and directories

namespace FileHandlingAutomation  // Declaring a namespace for the program
{
    class Program  // Declaring the Program class
    {
        static void Main(string[] args)  // Main method which is the entry point of the program
        {
            // Declaring the path for the folder and the file
            string folderPath = @"C:\TrumpfMetamation";  // Path for the folder we want to create
            string filePath = Path.Combine(folderPath, "Welcome.txt");  // Path for the file inside the folder

            try
            {
                // Step 1: Create the folder 'TrumpfMetamation' if it doesn't exist
                if (!Directory.Exists(folderPath))  // Check if the folder already exists
                {
                    Directory.CreateDirectory(folderPath);  // Create the folder if it doesn't exist
                    Console.WriteLine("Folder 'TrumpfMetamation' created.");  // Notify the user that the folder has been created
                }

                // Step 2: Create the file 'Welcome.txt' inside the folder if it doesn't exist
                if (!File.Exists(filePath))  // Check if the file already exists
                {
                    File.WriteAllText(filePath, "Welcome to Trumpf Metamation!");  // Create the file and write the content
                    Console.WriteLine("File 'Welcome.txt' created with content.");  // Notify the user that the file has been created with content
                }

                // Step 3: Verify the content of the file
                string fileContent = File.ReadAllText(filePath);  // Read the content of the file
                if (fileContent == "Welcome to Trumpf Metamation!")  // Check if the content matches the expected text
                {
                    Console.WriteLine("File content is correct.");  // If content matches, notify the user
                }
                else
                {
                    Console.WriteLine("File content is incorrect.");  // If content does not match, notify the user
                }

                // Step 4: Delete the file and the folder (commented out to avoid accidental deletion)
                //File.Delete(filePath);  // Delete the file
                //Directory.Delete(folderPath);  // Delete the folder
                //Console.WriteLine("File and Folder deleted successfully.");  // Notify the user that the file and folder were deleted

            }
            catch (Exception ex)  // Catch any exceptions that might occur
            {
                Console.WriteLine($"An error occurred: {ex.Message}");  // Print the error message if an exception occurs
            }
        }
    }
}
