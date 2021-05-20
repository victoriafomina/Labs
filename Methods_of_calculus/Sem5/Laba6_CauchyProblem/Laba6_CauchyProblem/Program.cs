using System;

namespace Laba6_CauchyProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var UI = new UserInterface(new MyFunction());
            UI.Run();
        }
    }
}
