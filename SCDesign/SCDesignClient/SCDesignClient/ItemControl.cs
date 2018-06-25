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
using System.IO;

namespace SCDesignClient
{
    public partial class ItemControl : UserControl
    {
        #region 配置项
        Point m_downPoint;
        Boolean m_mouseDown = false;
        ItemClass m_itemData;
        Boolean isActive = false;
        Color activeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(206)))), ((int)(((byte)(69)))));
        Color unselColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
        #endregion

        public delegate void ActiveChangeEventHandler(object sender, ActiveEventArgs e);
        //用event 关键字声明事件对象
        public event ActiveChangeEventHandler FocusChangeEvent;

        public void SetActive(Boolean flag)
        {
            if (flag != this.isActive)
            {
                this.isActive = flag;
                this.Refresh();
            }
        }

        public ItemClass getData()
        {
            return m_itemData;
        }
        public ItemControl()
        {
            InitializeComponent();
        }
        public ItemControl(ItemClass itemData)
        {
            m_itemData = itemData;
            this.Width = itemData.size[0];
            this.Height = itemData.size[1];
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            Brush brushes;
            if (isActive)
            {
                brushes = new SolidBrush(activeColor);
                g.FillRectangle(brushes, this.ClientRectangle);
            }
            else
            {
                brushes = new SolidBrush(unselColor);
                g.FillRectangle(brushes, this.ClientRectangle);
            }

            //this.BackColor = Color.Wheat;
            if (this.m_itemData == null)
            {
                this.BackColor = Color.Black;
            }
            else
            {
                string dirResource = Path.Combine(Application.StartupPath, "ItemImage");

                string imageFile = Path.Combine(dirResource, String.Format("{0}.png" , (int)m_itemData.shapetype));

                Image bmp = Image.FromFile(imageFile);

                Rectangle oldRect = new Rectangle(this.Width / 4, this.Width / 10, this.Width / 2, this.Height / 2);//背景图片需要被覆盖的部分
                Rectangle newRect = new Rectangle(0, 0, bmp.Width, bmp.Height);//嵌入图片需要被画入的部分
                g.DrawImage(bmp, oldRect, newRect, GraphicsUnit.Pixel);//在oldBmp中的oldRect画bmp中的newRect部分



                StringFormat stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Center;


                Rectangle rect = new Rectangle(0, this.Height * 65 / 100, this.Width , this.Width * 3/ 10);
                g.DrawString(m_itemData.itemname, new Font("宋体", 10), new SolidBrush(Color.Black), rect, stringFormat);


                Rectangle rectBorder = new Rectangle(1, 1, this.Width - 2, this.Height - 2);
                g.DrawRectangle(new Pen(Color.White), rectBorder);
            }
            
            
            base.OnPaint(e);
        }

        private void ItemControl_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void ItemControl_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void ItemControl_MouseClick(object sender, MouseEventArgs e)
        {
            this.SetActive(!this.isActive);

            //if (this.isActive)
            //{
                ActiveEventArgs args = new ActiveEventArgs();
                args.State = this.isActive;
                FocusChangeEvent(this, args);
            //}
        }

        private void ItemControl_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

    }
}
