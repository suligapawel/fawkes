using Fawkes.BLL.Abstracts;
using System;

namespace Fawkes.NetFramework.Models
{
    public class Displayer : IDisplayerAdapter
    {
        public void Show(string text)
        {
            Console.WriteLine(text);
        }
    }
}
