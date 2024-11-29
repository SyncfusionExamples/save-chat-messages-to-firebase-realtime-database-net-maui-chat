using Syncfusion.Maui.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMaui
{
    public class ChatMessage
    {
        public Author Author { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string Url { get; set; }
        public ImageSource Thumbnail { get; set; }
    }
}
