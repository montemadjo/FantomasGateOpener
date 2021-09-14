using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace FantomasGateOpener
{
    public static class HelperClass
    {
        public static void AppendText(this RichTextBox box, string text, string color, bool addTime = false)
        {
            BrushConverter bc = new BrushConverter();

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                // your code here.
                TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
                if (addTime)
                {
                    tr.Text = "\r" + DateTime.Now.ToLocalTime() + ": " + text;
                }
                else
                {
                    tr.Text = "\r" + text;
                }
                try
                {
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty,
                        bc.ConvertFromString(color));
                }
                catch (FormatException) { }
            }));

            // Scroll to the end.
            box.ScrollToEnd();
        }
    }
}
