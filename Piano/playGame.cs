using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Piano
{
    public partial class playGame : Form
    {
        #region Все Переменные 
        
        private int col_pol = Data.col_pol;//получаем значение уровня сложности и кол-во строк
        private string lvl = Data.lvl;//из статичесокого класса Data

        public  int[,] map = new int[8, Convert.ToInt32(Data.col_pol)];//запихиваем размер сетки в массив
        public int cellWidth = 70;//размер сетки 
        public int cellHeight = 100;

        private int record = 0;//переменные для таймера
        private int easyLvl =0;
        private int MediumLvl =0;
        private int DifficultLvl = 0;
        private int hell = 0;
        #endregion
        
        public playGame()// конструктор 
        {
            InitializeComponent();
            label1.Text = "Рекорд: 0";
            this.Width = cellWidth * col_pol + 15;
            this.Height = cellHeight * 8 + 40;
            this.Paint += new PaintEventHandler(Repaint);
            this.KeyUp += new KeyEventHandler(OnKeyboardPressed);

            Start();
        }
        public void Start()
        {
            
            timer1.Enabled = false;
            ClearMap();
            GenerateMap();
            label1.Text = "Рекорд: 0";
           
            Invalidate();
        }

        #region Работа с картой 
        public void ClearMap()//чистильщик карты
        {
            record = 0;
               easyLvl = 0;
            MediumLvl = 0;
            DifficultLvl = 0;
            hell = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < col_pol; j++)
                {
                    map[i, j] = 0;
                }
            }
        }
        public void GenerateMap()//генератор черных прямоугольников
        {
            Random random = new Random();
           
            for (int i = 0; i < 8; i++)
            {
                int j = random.Next(0, col_pol);
                map[i, j] = 1;  //!!!!  нолик  - белая клетка . единичка -черная 
            }
        }

        public void DrawMap(Graphics g)//закрашиваем сетку единичками и ноликами
        {
            
            for (int i = 0; i < 8; i++) //!!!!!!! col_pol это количество столбцов 4/5/6
                // а 8 это кол-во прямоугольников в гору 
            {
                for (int j = 0; j < col_pol; j++)// поэтому 8 всегда будет неизменная цифра ,будет только меняться col_pol
                {
                   if(map[i,j]==0)  // сорян за название так переменных )) :3
                        g.FillRectangle(new SolidBrush(Color.White), cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                   if(map[i,j]==1)
                        g.FillRectangle(new SolidBrush(Color.Black), cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                   //рисовали прямоугольники 
                }
            }

            for (int i = 0; i < 8; i++)//рисуем горизонтальные линии
               
             g.DrawLine(new Pen(new SolidBrush(Color.Black)), 0, i * cellHeight, col_pol * cellWidth, i * cellHeight);
            
            for (int i = 0; i < col_pol; i++)//Вертикальные линии
            g.DrawLine(new Pen(new SolidBrush(Color.Black)),i*cellWidth,0,i*cellWidth,8*cellHeight);

            
        }
     
        public void AddNewLine()
        {
            Random r = new Random();
            int j = r.Next(0, Data.col_pol);
            for (int k = 0; k < Data.col_pol; k++)
                map[0, k] = 0;
            map[0, j] = 1;
        }
        public void MoveMap()// После того как ты правильно тыкнул - карта смещается и добавляется новая линия
        {
            for (int i = 7; i > 0; i--)
            {
                for (int j = 0; j < Data.col_pol; j++)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
            AddNewLine();
            Invalidate();
        }
        #endregion
        private void Repaint(object sender,PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }

        #region Работа с счетчиком/кнопками/звуком
        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString()) //d1 d2... это 1,2,3 на клаве 
            {
                case "D1":
                    timer1.Enabled = true;
                    CheckForPressedButton(0);
                    
                    break;
                case "D2":
                    timer1.Enabled = true;
                    CheckForPressedButton(1);
                    
                    break;
                case "D3":
                    timer1.Enabled = true;
                    CheckForPressedButton(2);
                    
                    break;
                case "D4":
                    timer1.Enabled = true;
                    CheckForPressedButton(3);
                    
                    break;
                case "D5":
                    timer1.Enabled = true;
                    CheckForPressedButton(4);
                    
                    break;
                case "D6":
                    timer1.Enabled = true;
                    CheckForPressedButton(5);
                    
                    break;

                   
            }
            
        }
      
        

       private void timer1_Tick(object sender, EventArgs e)
        {
            if (easyLvl == 5 | MediumLvl == 3 | DifficultLvl == 2 | hell == 1)
            {
                timer1.Enabled = false;

                MessageBox.Show($"Игра окончена");

                Start();
            }

            if (Data.lvl == "Легкий")
                easyLvl += 1;
            else if (Data.lvl == "Средний")
                MediumLvl += 1;
            else if (Data.lvl == "Сложный")
                DifficultLvl += 1;
            else if (Data.lvl == "Невозможный")
                hell += 1;

            
            }
        public void CheckForPressedButton(int i)// Проверялка на туда ли ты тыкнул 
        {
            try
            {
                if (map[7, i] != 0) //если тыкнул не на нолик -good
                {
                    MoveMap();
                    PlaySound(i);
                    record += 10;
                    easyLvl = 0;
                    MediumLvl = 0;
                    DifficultLvl = 0;
                    hell = 0;
                    label1.Text = $"Рекорд: {record}";
                    
                }
                else
                {
                    timer1.Enabled = false;
                    MessageBox.Show($"Игра окончена");
                    Start();
                }
            }
            catch (Exception)
            {

            }
            
        }
        public void PlaySound(int sound)
        {
            System.IO.Stream str = null;
            switch (sound)
            {
                case 0:
                    str = Properties.Resources.g6;
                    break;
                case 1:
                    str = Properties.Resources.f6;
                    break;
                case 2:
                    str = Properties.Resources.d6;
                    break;
                case 3:
                    str = Properties.Resources.e6;
                    break;
                case 4:
                    str = Properties.Resources.g6;
                    break;
                case 5:
                    str = Properties.Resources.f6;
                    break;
            }
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }



        #endregion

        private void playGame_Load(object sender, EventArgs e)
        {

        }
    }
  
}
