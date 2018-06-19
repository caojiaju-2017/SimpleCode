using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SCDesignClient
{
    public partial class ItemControl : UserControl
    {
        #region 配置项
        Point m_downPoint;
        Boolean m_mouseDown = false;
        ItemClass m_itemData;
        #endregion

        public ItemControl()
        {
            InitializeComponent();
        }
        public ItemControl(ItemClass itemData)
        {
            m_itemData = itemData;
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            if (this.m_itemData == null)
            {
                this.BackColor = Color.Black;
            }
            else
            {
                this.Size = new Size(this.m_itemData.size[0], this.m_itemData.size[1]);

                if (this.m_itemData.shapetype == ItemShape.Triangle0)
                {
                    this.Triangle0(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Triangle1)
                {
                    this.Triangle1(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Circle)
                {
                    this.Circle(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Square)
                {
                    this.Square(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Rectangle0)
                {
                    this.Rectangle0(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Rectangle1)
                {
                    this.Rectangle1(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Trapezoid0)
                {
                    this.Trapezoid0(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Trapezoid1)
                {
                    this.Trapezoid1(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Parallelogram0)
                {
                    this.Parallelogram0(g);
                }
                else if (this.m_itemData.shapetype == ItemShape.Parallelogram1)
                {
                    this.Parallelogram1(g);
                }


            }
            
            
            base.OnPaint(e);
        }

        /// <summary>
        /// 上三角形
        /// </summary>
        /// <param name="g"></param>
        private void Triangle0(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            Point[] point = new Point[3];
            point[0] = new Point(this.Size.Width / 2, 0);
            point[1] = new Point(0, this.Size.Height);
            point[2] = new Point(this.Size.Width, this.Size.Height);
            g.FillPolygon(brushes, point);
        }

        /// <summary>
        /// 到三角形
        /// </summary>
        /// <param name="g"></param>
        private void Triangle1(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            Point[] point = new Point[3];
            point[0] = new Point(0, 0);
            point[1] = new Point(this.Size.Width, 0);
            point[2] = new Point(this.Size.Width/2, this.Size.Height);
            g.FillPolygon(brushes, point);
        }

        /// <summary>
        /// 圆形
        /// </summary>
        /// <param name="g"></param>
        private void Circle(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);
            g.FillEllipse(brushes, 1, 1, this.Size.Width - 2, this.Size.Height - 2);
        }

        /// <summary>
        /// 正方形
        /// </summary>
        /// <param name="g"></param>
        private void Square(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);
            g.FillRectangle(brushes, 1, 1, this.Width - 2, this.Height - 2);
        }


        /// <summary>
        /// 长方形 长 > 宽
        /// </summary>
        /// <param name="g"></param>
        private void Rectangle0(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            float rate = (float)0.8;

            g.FillRectangle(brushes, 0, (int)(this.Height * (1-rate)/2), this.Width, (int)(this.Height * rate));
        }

        /// <summary>
        /// 长方形 长 < 宽
        /// </summary>
        /// <param name="g"></param>
        private void Rectangle1(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            float rate = (float)0.8;

            g.FillRectangle(brushes, (int)(this.Width * (1 - rate) / 2), 0, (int)(this.Width * rate), this.Height);
        }

        /// <summary>
        /// 梯形
        /// </summary>
        /// <param name="g"></param>
        private void Trapezoid0(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            double rate = 0.6;

            int yOffset = (int)(this.Size.Height * (1 - rate) / 4);
            Point[] point = new Point[4];
            point[0] = new Point((int)(this.Size.Width * (1 - rate) / 2), yOffset);
            point[1] = new Point((int)(this.Size.Width * (1 + rate) / 2), yOffset);
            point[2] = new Point(this.Size.Width, this.Size.Height - 2 * yOffset);
            point[3] = new Point(0, this.Size.Height - 2 * yOffset);
            g.FillPolygon(brushes, point);

        }


        /// <summary>
        /// 倒梯形
        /// </summary>
        /// <param name="g"></param>
        private void Trapezoid1(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            double rate = 0.6;

            int yOffset = (int)(this.Size.Height * (1 - rate) / 4);
            Point[] point = new Point[4];
            point[0] = new Point(0, yOffset);
            point[1] = new Point(this.Size.Width, yOffset);

            point[2] = new Point((int)(this.Size.Width * (1 + rate) / 2), this.Size.Height - 2 * yOffset);
            point[3] = new Point((int)(this.Size.Width * (1 - rate) / 2), this.Size.Height - 2 * yOffset);
            
            g.FillPolygon(brushes, point);
        }

        /// <summary>
        /// 平行四边形
        /// </summary>
        /// <param name="g"></param>
        private void Parallelogram0(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            double rate = 0.2;

            int yOffset = (int)(this.Size.Height * rate);
            Point[] point = new Point[4];
            point[0] = new Point(0, yOffset);
            point[1] = new Point((int)(this.Width*(1-rate)), yOffset);

            point[2] = new Point((int)(this.Size.Width), this.Size.Height - yOffset);
            point[3] = new Point((int)(this.Size.Width * rate), this.Size.Height - yOffset);
            g.FillPolygon(brushes, point);
        }

        /// <summary>
        /// 平行四边形
        /// </summary>
        /// <param name="g"></param>
        private void Parallelogram1(Graphics g)
        {
            Color color = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            Brush brushes = new SolidBrush(color);

            double rate = 0.2;

            int yOffset = (int)(this.Size.Height * rate);
            Point[] point = new Point[4];
            point[0] = new Point((int)(this.Size.Width * rate), yOffset);
            point[1] = new Point((int)(this.Width), yOffset);

            point[2] = new Point((int)(this.Size.Width*(1-rate)), this.Size.Height - yOffset);
            point[3] = new Point(0, this.Size.Height - yOffset);
            g.FillPolygon(brushes, point);
        }
        private void ItemControl_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void ItemControl_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        //private void ItemControl_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Console.WriteLine("mGroupBox1_MouseDown");

        //    // 记录鼠标按下位置
        //    m_downPoint = e.Location;

        //    m_mouseDown = true;
        //}

        //private void ItemControl_MouseMove(object sender, MouseEventArgs e)
        //{
        //    //Console.WriteLine("mGroupBox1_MouseMove");
        //    if (!m_mouseDown)
        //    {
        //        return;
        //    }


        //    int offsetX = e.Location.X - m_downPoint.X;
        //    int offsetY = e.Location.Y - m_downPoint.Y;

        //    if (offsetX > 2 || offsetY >2)
        //    {
                
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    m_downPoint = e.Location;

        //    this.Location = new Point(this.Location.X + offsetX, this.Location.Y + offsetY);

        //    //this.Refresh();
        //}

        //private void ItemControl_MouseUp(object sender, MouseEventArgs e)
        //{
        //    m_mouseDown = false;
        //}
    }
}
