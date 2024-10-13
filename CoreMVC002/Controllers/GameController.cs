using CoreMVC002.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC002.Controllers
{
    public class GameController : Controller
    {
        //[HttpGet]
        private static XAXBEngine game = new XAXBEngine();

        public IActionResult Index()
        {
            // 顯示猜數字遊戲主頁
            ViewBag.GuessHistory = game.GetGuessHistory(); // 傳遞猜測歷史
            ViewBag.GuessCount = game.GuessCount; // 傳遞猜測次數
            ViewBag.GameOver = null; // 初始化狀態，未結束遊戲
            return View(game);
        }

        [HttpPost]
        public IActionResult Guess(string userGuess)
        {
            // 驗證用戶輸入是否為有效的四位數字
            if (string.IsNullOrEmpty(userGuess) || userGuess.Length != 4 || !userGuess.All(char.IsDigit))
            {
                ViewBag.Error = "請輸入四位有效數字！"; // 傳遞錯誤信息
                ViewBag.GuessHistory = game.GetGuessHistory(); // 保留猜測歷史
                ViewBag.GuessCount = game.GuessCount; // 保留猜測次數
                return View("Index", game); // 返回主頁
            }

            // 設定用戶猜測並生成結果
            game.Guess = userGuess;
            string result = game.GetResult(userGuess); // 取得當前猜測結果

            // 判斷是否遊戲結束
            if (game.IsGameOver(userGuess))
            {
                ViewBag.GameOver = "恭喜！你猜對了！"; // 顯示遊戲結束信息
                ViewBag.Result = result; // 顯示最後猜測結果
                ViewBag.GuessHistory = game.GetGuessHistory(); // 顯示猜測歷史
                ViewBag.GuessCount = game.GuessCount; // 顯示猜測次數

                // 顯示選擇重玩或結束遊戲的按鈕
                ViewBag.RestartPrompt = true;
            }
            else
            {
                ViewBag.GameOver = null; // 繼續遊戲
                ViewBag.Result = result; // 顯示當前猜測結果
                ViewBag.GuessHistory = game.GetGuessHistory(); // 顯示猜測歷史
                ViewBag.GuessCount = game.GuessCount; // 顯示猜測次數
            }

            return View("Index", game);
        }

        // 重置遊戲
        [HttpPost]
        public IActionResult ResetGame()
        {
            game = new XAXBEngine();
            return RedirectToAction("Index");
        }
    }
}
