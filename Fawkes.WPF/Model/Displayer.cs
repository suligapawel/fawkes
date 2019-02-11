using Fawkes.BLL.Abstracts;
using System.Windows.Controls;

namespace Fawkes.WPF.Model
{
    public class Displayer : IDisplayerAdapter
    {
        private readonly TextBlock _textBlock;

        public Displayer(TextBlock textBlock)
        {
            _textBlock = textBlock;
        }

        public void Show(string text)
        {
            _textBlock.Text += $"\n{text}";
        }
    }
}
