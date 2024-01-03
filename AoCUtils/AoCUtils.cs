namespace AoCUtils
{
    public static class FileUtil
    {
        /// <summary>
        /// Reads lines in input file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>string array of lines</returns>
        public static string[] ReadFileByLine(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines;
        }

        /// <summary>
        /// Reads lines in input file, separates blocks delimited by two newlines
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>string array of lines with embedded newlines</returns>
        public static string[] ReadFileByBlock(string fileName) 
        {
            string allText = File.ReadAllText(fileName);
            
            string nl2x = $"{Environment.NewLine}{Environment.NewLine}";
            string[] blocks = allText.Split(nl2x);

            return blocks;
        }

        public static string[] ReadFileByDelimiter(string fileName, string delimiter)
        {
            string allText = File.ReadAllText(fileName);

            string[] blocks = allText.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            return blocks;
        }

        public static List<List<char>> ReadFileToCharGrid(string fileName)
        {
            string[] input = File.ReadAllLines(fileName);

            List<List<char>> grid = new();

            int colSize = input.Length;

            // fill forest
            for (int y = 0; y < colSize; y++)
            {
                List<char> row = new();

                foreach (char c in input[y])
                    row.Add(c);

                grid.Add(row);
            }

            return grid;
        }

        public static List<List<int>> ReadFileToIntGrid(string fileName)
        {
            string[] input = File.ReadAllLines(fileName);

            List<List<int>> grid = new();

            int colSize = input.Length;

            // fill forest
            for (int y = 0; y < colSize; y++)
            {
                List<int> row = new();

                foreach (char c in input[y])
                    row.Add(c - '0');

                grid.Add(row);
            }

            return grid;
        }

    }

    public static class MathUtil
    {
        // GCD - greatest common factor
        public static int GCF(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static long GCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // LCM - Least Common Multiple
        public static int LCM(int a, int b)
        {
            return (a / GCF(a, b)) * b;
        }

        public static long LCM(long a, long b)
        {
            return (a / GCF(a, b)) * b;
        }

        public static long LCM(List<long> numbers)
        {
            return numbers.Aggregate((x, y) => x * y / GCD(x, y));
        }

        public static long GCD(long a, long b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }
    }
}