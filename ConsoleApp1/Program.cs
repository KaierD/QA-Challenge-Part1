using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QA_Challenge
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            string filename = "";
            string content = OpenSelectedFile(ref filename); // open file using the FileDialog, its more user-friendly
            List<int> result = FindLeastRepeated(content);
            Console.WriteLine("File: {0}, Number: {1}, Repetead: {2} time(s)", filename, result[0], result[1]);
        }
        static List<int> FindLeastRepeated(string content)
        {
            List<int> _result = new List<int>();
            List<int> num_list = new List<int>();
            foreach (string i in content.Split('\n')) //generating the list of number
            {
                if (IsNumeric(i))
                {
                    num_list.Add(Convert.ToInt32(i));
                }
            }

            int n = num_list.Count;

            if (n <= 0) // check if the opened file is appropriate
            {
                Console.WriteLine("Invalid file.");
                return _result;
            }

            Dictionary<int, int> count = new Dictionary<int, int>(); // time complexity is O(nlogn)
            for (int i = 0; i < n; i++)
            {
                int key = num_list[i];
                if (count.ContainsKey(key))
                {
                    count[key]++;
                }
                else
                {
                    count.Add(key, 1);
                }
            }

            int min_count = n + 1;
            int result = num_list[0];
            foreach (KeyValuePair<int, int> pair in count)
            {
                if (min_count > pair.Value || (min_count == pair.Value && result > pair.Key))
                {
                    result = pair.Key;
                    min_count = pair.Value;
                }
            }
            _result.Add(result);
            _result.Add(min_count);

            return _result;
        }

        static String OpenSelectedFile(ref string filename)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    filename = Path.GetFileName(filePath);

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                else
                {
                    Console.WriteLine("Failed to open the selected file.");
                }
                return fileContent;
            }
        }
        static bool IsNumeric(object Expression)
        {
            double retNum;
            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
    }
}
