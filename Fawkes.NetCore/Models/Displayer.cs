using Fawkes.BLL.Abstracts;
using System;

namespace Fawkes.NetCore.Models
{
    public class Displayer : IDisplayerAdapter
    {
        public void Show(string text)
        {
            Console.WriteLine(text);
        }
    }
}
