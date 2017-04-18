#region usings

using System;
using BrazilianCompany.Logic.Interface;

#endregion

namespace BrazilianCompany.Logic.Implementation
{
    public class ConsoleInterface : IUserInterface
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string format, params string[] args)
        {
            Console.WriteLine(format, args);
        }
    }
}