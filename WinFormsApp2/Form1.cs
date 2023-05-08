using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private TextBox[,] board;
        public Form1()
        {
            InitializeComponent();
            board = new TextBox[,]
            {
                { textBox1, textBox2, textBox3,textBox4,textBox5,textBox6,textBox7 },
                { textBox8, textBox9, textBox10,textBox11,textBox12,textBox13,textBox14 },
                { textBox15, textBox16, textBox17,textBox18,textBox19,textBox20,textBox21 },
                { textBox22, textBox23, textBox24,textBox25,textBox26,textBox27,textBox28 },
                { textBox29, textBox30, textBox31,textBox32,textBox33,textBox34,textBox35 },
                { textBox36, textBox37, textBox38,textBox39,textBox40,textBox41,textBox42 },
                { textBox43, textBox44, textBox45,textBox46,textBox47,textBox48,textBox49 }
            };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Filter = "二元檔案(*.dat)|*.dat";
            if(openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    board[i, j].Text = br.ReadString();
                    board[i, j].ForeColor = Color.Black;
                }
            }

            br.Close();
            fs.Close();
        }

        char findPath(int x, int y)
        {
            if (x == 6 && y == 6)
            {
                board[x, y].Text = "1";
                board[x, y].ForeColor = Color.Blue;
                return '1'; //find the end
            }

            if (x == -1 || x == 7 || y == -1 || y == 7)
                return '2'; //path not found;
            if (board[x, y].Text != "0")
                return '2'; ////path not found;

            board[x, y].Text = "2"; // 佔據位置

            char up = findPath(x, y - 1) ,down = findPath(x,y+1),left = findPath(x-1,y),right=findPath(x+1,y);
            if (up == '1' || down == '1' || left == '1' || right == '1')
            {
                board[x, y].Text = "1";
                board[x,y].ForeColor = Color.Blue;
                return '1';
            }

            return '2';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            findPath(0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "二元檔案(*.dat)|*.dat";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            BinaryWriter br = new BinaryWriter(fs);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                   br.Write(board[i, j].Text);

                }
            }

            br.Close();
            fs.Close();
        }
    }
}
