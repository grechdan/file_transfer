namespace FileTransfer.Utility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class CsvUtility
    {
        private readonly string _csvFilePath;

        public CsvUtility(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        // Method to read data from the CSV file
        public Dictionary<int, string> ReadCsv()
        {
            var data = new Dictionary<int, string>();
            if (File.Exists(_csvFilePath))
            {
                var lines = File.ReadAllLines(_csvFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int rowNumber))
                    {
                        data[rowNumber] = parts[1];
                    }
                }
            }
            return data;
        }

        public void WriteCsv(Dictionary<int, string> data)
        {
            var lines = data.OrderBy(row => row.Key).Select(row => $"{row.Key},{row.Value}");
            File.WriteAllLines(_csvFilePath, lines);
        }
    }
}
