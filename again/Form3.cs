using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace again
{
    public partial class Form3 : Form
    {
                
        public Form3()
        {
           InitializeComponent();
           this.groupBox1.MouseClick +=new MouseEventHandler(this.groupBox1_MouseClick);//给groupbox添加MouseClick事件
        	
           //建立棋子数组
           int x, y;
           for (x = 0; x < 15; x++)
               for (y = 0; y < 15; y++)
               {            
            		chesspb[x, y] = new PictureBox();
                    chesspb[x, y].Location = new Point(46+32*x,46+32*y);//这是棋子图的左上角坐标
                    chesspb[x, y].Size = new Size(32,32);///棋子图尺寸
                    chesspb[x, y].BackColor = Color.Transparent;
                    chesspb[x, y].SizeMode = PictureBoxSizeMode.CenterImage;
                    chesspb[x, y].Visible = false;//先隐藏棋子
                    groupBox1.Controls.Add(chesspb[x, y]);
               }
           for(i=0;i<15;i++)
       		for(j=0;j<15;j++)
       			common.qizisy[i, j].color=0;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //播放器
            axWindowsMediaPlayer1.currentPlaylist.appendItem(axWindowsMediaPlayer1.newMedia("music.mp3"));
            axWindowsMediaPlayer1.currentPlaylist.appendItem(axWindowsMediaPlayer1.newMedia("music2.mp3"));
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        //一系列的初始化~
        public struct qizi//定义结构，存棋子信息
        {
            public int heng, zong,color;
        }
        public static class common
        {
            public static qizi[,] qizisy = new qizi[15, 15];
        }         
         
        qizi[] shunxu=new qizi[225];//用于悔棋的数组
        PictureBox[,] chesspb=new PictureBox[15,15];//定义棋子数组
        int i=0,j=0,sum=0;
        
        //平局之后
        public void pingju()
        {
            DialogResult result = MessageBox.Show("棋盘已满，本局平手！再来一局？", "本局结果", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)//用户点击“是”
            {
                
                //隐藏棋子图，清除chess数组
                for (i = 0; i < 15; i++)
                    for (j = 0; j < 15; j++)
                    {
                        chesspb[i, j].Visible = false;
                        common.qizisy[i, j].color = 0;//清除记录恢复初始值
                    }
                sum = 0;
            }
            if (result == DialogResult.Cancel)//用户点击“否”
            {
                Form f1 = new Form1();
                this.Hide();
                f1.ShowDialog();
                this.Close();//返回主页
            }
        }

        //黑棋赢之后
        public void blackwin()
        {
            DialogResult result = MessageBox.Show("O赢了！再来一局？", "本局结果", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)//用户点击“是”
            {

                //隐藏棋子图，清除chess数组
                for (i = 0; i < 15; i++)
                    for (j = 0; j < 15; j++)
                    {
                        chesspb[i, j].Visible = false;
                        common.qizisy[i, j].color = 0;//清除记录恢复初始值
                    }
                sum = 0;
            }
            if (result == DialogResult.Cancel)//用户点击“否”
            {
                Form f1 = new Form1();
                this.Hide();
                f1.ShowDialog();
                this.Close();//返回主页
            }
        }

        //白棋赢之后
        public void whitewin()
        {
            DialogResult result = MessageBox.Show("X赢了！再来一局？", "本局结果", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)//用户点击“是”
            {

                //隐藏棋子图，清除chess数组
                for (i = 0; i < 15; i++)
                    for (j = 0; j < 15; j++)
                    {
                        chesspb[i, j].Visible = false;
                        common.qizisy[i, j].color = 0;//清除记录恢复初始值
                    }
                sum = 0;
            }
            if (result == DialogResult.Cancel)//用户点击“否”
            {
                Form f1 = new Form1();
                this.Hide();
                f1.ShowDialog();
                this.Close();//返回主页
            }
        }

        //检查棋盘的自定义函数
        public void check(int x,int y)
        {
            int i, j, count; 
            //检查行
            count = 1;
            for (i = x; i >= 0 && common.qizisy[i, y].color == common.qizisy[x, y].color; i--)
                count++;
            for (i = x; i < 15 && common.qizisy[i, y].color == common.qizisy[x, y].color; i++)
                count++;    
            if (count == 7)
            {
                if ( common.qizisy[x, y].color == 1)
                {
                    blackwin();//黑棋赢
                }
                if (common.qizisy[x, y].color == 2)
                {
                    whitewin();//白棋赢
                }
            }

            //检查列
            count = 1;
            for (j = y; j >= 0 && common.qizisy[x, j].color == common.qizisy[x, y].color; j--)
                count++;
            for (j = y; j < 15 && common.qizisy[x, j].color == common.qizisy[x, y].color; j++)
                count++; 
            if (count == 7)
            {
                if (common.qizisy[x, y].color == 1)
                {
                    blackwin();//黑棋赢
                }
                if (common.qizisy[x, y].color == 2)
                {
                    whitewin();//白棋赢
                }
            }

            //检查右斜的斜线
            count = 1;
            for (i = x, j = y; i < 15 && j >= 0 && common.qizisy[i, j].color == common.qizisy[x, y].color; i++, j--)
                count++;
            for (i = x, j = y; i >= 0 && j < 15 && common.qizisy[i, j].color == common.qizisy[x, y].color; i--, j++)
                count++; 
            if (count == 7)
            {
                if (common.qizisy[x, y].color == 1)
                {
                    blackwin();//黑棋赢
                }
                if (common.qizisy[x, y].color == 2)
                {
                    whitewin();//白棋赢
                }
            }

            //检查左斜的斜线
            count = 1;
            for (i = x, j = y; j < 15 && i < 15 && common.qizisy[i, j].color == common.qizisy[x, y].color; i++, j++)
                count++;
            for (i = x, j = y; i >= 0 && j >= 0 && common.qizisy[i, j].color == common.qizisy[x, y].color; i--, j--)
                count++;
            if (count == 7)
            {
                if (common.qizisy[x, y].color == 1)
                {
                    blackwin();//黑棋赢
                }
                if (common.qizisy[x, y].color == 2)
                {
                    whitewin();//白棋赢
                }
            }
            //棋盘满棋，平局
            if (sum == 225)
                pingju();            
        }
        
        //下棋的函数
        private void groupBox1_MouseClick(object sender, MouseEventArgs e)
    	{
            
            sum++;//记录下棋次数

            //要显示的棋子序号与鼠标坐标的关系
            int x, y;
            x = (e.X -46) / 32;
            y = (e.Y - 46) / 32;
            //注意防止越界：在棋盘以内点击才会下棋
            if (e.X>=30&&e.X<=542&&e.Y>=30&&e.Y<=542)
                chesspb[x, y].Visible = true;
            else ;
            
            //声明黑白棋子图片路径
            string blackchess = Application.StartupPath + "\\image\\B.png";
            string whitechess = Application.StartupPath + "\\image\\W.png";
           
            if (sum%2==1)//sum为奇数，下黑棋
            {
                chesspb[x, y].Image = Image.FromFile(blackchess);
                common.qizisy[x, y].heng = x;
                common.qizisy[x, y].zong = y;
                common.qizisy[x, y].color = 1;
                //表示黑棋
                check(x, y);
            }
            else//sum为偶数，下白棋
            {
                chesspb[x, y].Image = Image.FromFile(whitechess);
                common.qizisy[x, y].heng = x;
                common.qizisy[x, y].zong = y;
                common.qizisy[x, y].color = 2;//表示白棋
                check(x, y);
            }
            shunxu[sum-1]=common.qizisy[x,y];//将刚下的棋的信息（结构）放入数组中
            
    	}

        //返回主页的按键
        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            this.Hide();
        	Form f1 = new Form1();            
            f1.ShowDialog();
            
        }

        //退出游戏的按键
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //注意这里面的坐标参考系为GroupBox
        void GroupBox1Paint(object sender, PaintEventArgs e)
        {
        	//画格子
            int i;
            Graphics gr = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            Pen margin = new Pen(Color.Black, 2);
            //这里是在给棋盘加一个框框哦~
            gr.DrawLine(margin, 30, 30, 30, 542);
            gr.DrawLine(margin, 30, 30, 542, 30);
            gr.DrawLine(margin, 30, 542, 542, 542);
            gr.DrawLine(margin, 542,30, 542, 542);
            for (i = 0; i < 15; i++)
            {
                gr.DrawLine(myPen,62+i * 32, 62, 62+i * 32,510);
                gr.DrawLine(myPen, 62, 62+i * 32, 510, 62+i * 32);//棋盘起点（30,30），大小32
            }
            
        }

        //悔棋的按键
        private void button3_Click(object sender, EventArgs e)
        {       
        	if(sum>0)
        	{
        		DialogResult result = MessageBox.Show("确定悔棋吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            	if (result == DialogResult.OK)
           		 {            	
            		qizi a;
            		a=shunxu[sum-1];
                	chesspb[a.heng,a.zong].Visible=false;                
					sum--;
					common.qizisy[a.heng,a.zong].color = 0;            		            	
            	}  
        	}         	
        	   
        }

        //播放音乐
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                
            }
        }

        //再来一局的按键
        private void button4_Click(object sender, EventArgs e)
        {
            sum = 0;
            for (i = 0; i < 15; i++)
                for (j = 0; j < 15; j++)
                {
                    common.qizisy[i, j].color = 0;
                    chesspb[i,j].Visible = false;
                }
        }      

    }
}

