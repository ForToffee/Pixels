using Microsoft.JSInterop;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;

namespace Pixels.Pages
{
    public class Pixel
    {
        public Color color { get; set; } = Color.Black;
        public string letter { get; set; } = "c";
    }
    public partial class Index
    {
        Pixel[] pixels = new Pixel[64];

        Dictionary<string, Color> colours = new Dictionary<string, Color>()
        {
           { "a" , Color.FromArgb( 255, 255, 255 ) },
            {"b" , Color.FromArgb (105, 105, 105)},
            {"c" , Color.FromArgb (0, 0, 0)},
            {"d" , Color.FromArgb (100, 149, 237)},
            {"e" , Color.FromArgb (0, 0, 205)},
            {"f" , Color.FromArgb (25, 25, 112)},
            {"g" , Color.FromArgb (0, 191, 255)},
            {"h" , Color.FromArgb (0, 255, 255)},
            {"j" , Color.FromArgb(143, 188, 143)},
            {"k" , Color.FromArgb(46, 139, 87)},
            {"l" , Color.FromArgb(0, 255, 127)},
            {"m" , Color.FromArgb(34, 139, 34)},
            {"n" , Color.FromArgb(154, 205, 50)},
            {"o" , Color.FromArgb(128, 128, 0)},
            {"p" , Color.FromArgb(240, 230, 140)},
            {"q" , Color.FromArgb(255, 255, 0)},
            {"r" , Color.FromArgb(184, 134, 11)},
            {"s" , Color.FromArgb(139, 69, 19)},
            {"t" , Color.FromArgb(255, 140, 0)},
            {"u" , Color.FromArgb(178, 34, 34)},
            {"v" , Color.FromArgb(255, 0, 0)},
            {"w" , Color.FromArgb(255, 192, 203)},
            {"y" , Color.FromArgb(255, 20, 147)},
            {"z" , Color.FromArgb(153, 50, 204) } 
        };

        protected override async Task OnInitializedAsync()
        {
            ResetBoard();
        }

        private Pixel selectedColour { get; set; }

        private void ColourClick(string key)
        {
            Color c = colours[key];
            selectedColour = new Pixel() { color = c, letter = key };
        }

        private async Task PixelClick(int idx)
        {
            pixels[idx] = selectedColour;
            //board[idx] = player;
            //player = player == "X" ? "O" : "X";

            //foreach (int[] combo in winningCombos)
            //{
            //    int p1 = combo[0];
            //    int p2 = combo[1];
            //    int p3 = combo[2];
            //    if (board[p1] == String.Empty || board[p2] == String.Empty || board[p3] == String.Empty) continue;
            //    if (board[p1] == board[p2] && board[p2] == board[p3] && board[p1] == board[p3])
            //    {
            //        string winner = player == "X" ? "Player TWO" : "Player ONE";
            //        await JS.InvokeVoidAsync("ShowSwal", winner);
            //        ResetGame();
            //    }
            //}

            //if (board.All(x => x != ""))
            //{
            //    await JS.InvokeVoidAsync("ShowTie");
            //    ResetGame();
            //}
        }

        private string GetCode()
        {
            List<string> variables = new List<string>();
            StringBuilder pixellist = new StringBuilder();

            pixellist.AppendLine( "image = [");
            pixellist.Append("  ");
            int count = 0;

            foreach (Pixel p in  pixels)
            {
                if (!variables.Contains(p.letter))
                    variables.Add(p.letter);

                pixellist.Append(p.letter + ",");
                if ((count + 1) % 8 == 0  && count < 63)
                {
                    pixellist.AppendLine();
                    pixellist.Append("  ");
                }

                count++;
            }
            pixellist.Remove(pixellist.Length-1,1);
            pixellist.AppendLine("]");

            StringBuilder ret = new StringBuilder();
            foreach (string v in variables)
            {
                Color c = colours[v];
                ret.AppendLine(v + " = (" + c.R + "," + c.G + "," + c.B + ")");
            }
            ret.AppendLine();
            ret.Append(pixellist);

            return ret.ToString();
        }

        private string HTMLHex(Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";

        private void ResetBoard()
        {
            selectedColour = new Pixel();
            for (int i = 0; i < 64; ++i)
            {
                pixels[i] = selectedColour;
            }
        }
    }
    
}