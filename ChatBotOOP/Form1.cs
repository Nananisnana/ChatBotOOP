using System;
using System.Windows.Forms;

namespace ChatBotOOP
{
    public partial class Form1 : Form
    {
        
        private IChatService chatService;

        public Form1()
        {
            InitializeComponent();

            
            chatService = new GeminiService();
        }

        
        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userText = txtMessage.Text;
            if (string.IsNullOrWhiteSpace(userText)) return;

            
            txtHistory.AppendText("Sen: " + userText + "\r\n");
            txtMessage.Clear();
            txtHistory.AppendText("Bot yazıyor...\r\n");

           
            string botResponse = await chatService.GetResponseAsync(userText);

           
            txtHistory.AppendText("Gemini: " + botResponse + "\r\n");
            txtHistory.AppendText("--------------------------------\r\n");

           
            txtHistory.SelectionStart = txtHistory.Text.Length;
            txtHistory.ScrollToCaret();
        }
    }
}