using System;
using System.Linq;
using Sprache;

namespace Sample
{
    class Program
    {
        // 标识符解析规则
        private static Parser<string> Identifier =
            from leading in Parse.WhiteSpace.Many()             // 可以包含前置空格
            from first in Parse.Letter.Once()                   // 第一个字符只能是字母
            from rest in Parse.LetterOrDigit.Many()             // 剩余的字符可以是字母或数字
            from trailing in Parse.WhiteSpace.Many()            // 可以包含后置空格
            select new string(first.Concat(rest).ToArray());    // first+rest做为标识符

        /// <summary>
        /// 检查输入的文本中是否包含合法的标识符
        /// </summary>
        /// <param name="text">文本</param>
        private static void CheckIdentifier(string text)
        {
            var result = Identifier.TryParse(text);
            if (result.WasSuccessful)
            {
                Console.WriteLine($"[{text}]中包含合法的标识符.标识符为: {result.Value}");
            }
            else
            {
                Console.WriteLine($"[{text}]中不包含合法的标识符.");
            }
        }

        static void Main(string[] args)
        {
            CheckIdentifier("    a123  ");
            CheckIdentifier(" 1abc");
        }
    }
}
