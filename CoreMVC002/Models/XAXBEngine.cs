namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        public int GuessCount { get; set; } // 用於累計猜測次數
        public List<string> GuessHistory { get; set; } // 用於儲存猜測歷史

        public XAXBEngine()
        {
            // 隨機生成四位數的 Secret
            Secret = GenerateRandomSecret();
            Guess = null;
            Result = null;
            GuessCount = 0; // 初始化猜測次數
            GuessHistory = new List<string>(); // 初始化猜測歷史
        }

        // 隨機生成 4 個不重複數字
        private string GenerateRandomSecret()
        {
            Random random = new Random();
            return string.Join("", Enumerable.Range(0, 10).OrderBy(x => random.Next()).Take(4));
        }

        public int numOfA(string guessNumber)
        {
            // 計算 A 的數量 (位置和數字都對)
            int countA = 0;
            for (int i = 0; i < 4; i++)
            {
                if (guessNumber[i] == Secret[i])
                {
                    countA++;
                }
            }
            return countA;
        }

        public int numOfB(string guessNumber)
        {
            // 計算 B 的數量 (數字對但位置錯)
            int countB = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Secret.Contains(guessNumber[i]) && guessNumber[i] != Secret[i])
                {
                    countB++;
                }
            }
            return countB;
        }

        public string GetResult(string guessNumber)
        {
            // 累計猜測次數
            GuessCount++;

            // 生成結果 A 和 B
            int A = numOfA(guessNumber);
            int B = numOfB(guessNumber);
            Result = $"{A}A{B}B";

            // 將猜測結果保存到歷史記錄中
            GuessHistory.Add($"第 {GuessCount} 次猜測: {guessNumber}: {Result}");

            return Result;
        }

        public bool IsGameOver(string guessNumber)
        {
            // 當 A 為 4 時表示猜測成功
            return numOfA(guessNumber) == 4;
        }

        public List<string> GetGuessHistory()
        {
            // 返回猜測歷史
            return GuessHistory;
        }
    }
}