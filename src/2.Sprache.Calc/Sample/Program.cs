using System;
using System.Linq;
using System.Linq.Expressions;
using Sprache.Calc;

namespace Sample
{
    class Program
    {
        private static readonly XtensibleCalculator Calculator = new XtensibleCalculator();

        static void Main(string[] args)
        {
            // 四则运算
            RunExpression("1 + 2 * 3");
            RunExpression("(1 + 2) * 3");

            // 自定义函数
            // 这里注册了一个名为"阶乘"的函数,用来计算一个数的阶乘
            Calculator.RegisterFunction("阶乘", n =>
            {
                // n的阶乘 = n * (n-1) * (n-2) * ... * 1
                int r = 1;
                for (int i = (int) n; i > 0; i--) r *= i;
                return r;
            });
            RunExpression("阶乘(3) + 阶乘(4)");

            // 参数
            RunExpression("阶乘(a) + 阶乘(b)", a => 4, b => 5); // 将4代入a, 5代入b
        }

        /// <summary>
        /// 运行表达式,输出计算结果
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="parameters">参数</param>
        private static void RunExpression(string text, params Expression<Func<double, double>>[] parameters)
        {
            var expr = Calculator.ParseExpression(text, parameters);
            var func = expr.Compile();
            Console.WriteLine($"{text} = {func()}");
        }
    }
}
