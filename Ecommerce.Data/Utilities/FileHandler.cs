using System;
using System.IO;
using System.Text.Json;

namespace Ecommerce.Data.Utilities
{
    //provide methods to read and write json files
    public static class FileHandler
    {
        private static readonly string BasePath;
        private static readonly JsonSerializerOptions options;
        //This static constructor initializes the BasePath and options for JSON serialization/deserialization.
        static FileHandler()
        {
            BasePath = "Data";
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            // Create the directory if it doesn't exist
            try
            {
                if (!Directory.Exists(BasePath))
                {
                    Directory.CreateDirectory(BasePath);
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"failed to create directory {BasePath} : {ex.Message}", ex);
            }
        }
        //ReadJson = Converting Json to C# objects = Deserialization
        // This method reads a JSON file and deserializes it into an object of type T.
        public static T? ReadJson<T>(string fileName)
        {
            // Check if the file name is null or empty
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            try
            {
                string filePath = Path.Combine(BasePath, fileName);
                if (!File.Exists(filePath))
                {
                    return default;
                }
                // Read the JSON string from the file
                string jsonString = File.ReadAllText(filePath);
                // Deserialize the JSON string into an object of type T
                return JsonSerializer.Deserialize<T>(jsonString, options);
            }
            // Handle specific exceptions for JSON deserialization
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize JSON from {fileName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading from file {fileName}: {ex.Message}", ex);
            }
        }
        //WriteJson = Converting C# objects to Json = Serialization
        // This method serializes an object of type T into a JSON string and writes it to a file.
        public static void WriteJson<T>(string fileName, T data)
        {
            // Check if the file name is null or empty
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));
            try
            {
                string filePath = Path.Combine(BasePath, fileName);
                // Serialize the object into a JSON string
                String jsonString = JsonSerializer.Serialize(data, options);
                // Write the JSON string to the file
                File.WriteAllText(filePath, jsonString);
            }
            // Handle specific exceptions for JSON serialization
            catch (JsonException ex)
            {
                throw new Exception($"Failed to serialize JSON to {fileName}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing file {fileName}: {ex.Message}", ex);
            }
        }
    }
}
