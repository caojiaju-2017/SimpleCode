using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.IO;
using SCDesignClient;

namespace GDIPlusDemo
{
    public class FormEx : FormBase
    {
        #region Field

        //窗体圆角矩形半径
        private int _radius = 5;

        //是否允许窗体改变大小
        private bool _canResize = true;

        private Image _fringe = Image.FromFile(@".\Res\fringe_bkg.png");
        private Image _formBkg = Image.FromFile(@".\Res\FormBkg\bkg_stars.jpg");
        private Momo.Forms.MTabControl mTabControl1;
        private Momo.Forms.DTabPage dtpBase;
        private Momo.Forms.DTabPage dtpCollection;
        private Momo.Forms.DTabPage dtpControl;
        private Momo.Forms.MGroupBox mGroupBox1;

        //系统按钮管理器
        private SystemButtonManager _systemButtonManager;

        #endregion
        private Momo.Forms.MRichTextBox itemInfo;
        int rowSize = 4;
        private DesignerPanel dpDrawPanel;

        private List<ItemClass> m_itemLists = new List<ItemClass>();

        #region Constructor

        public FormEx()
        {
            InitializeComponent();
            FormExIni();
            _systemButtonManager = new SystemButtonManager(this);
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(byte), "5")]
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    this.Invalidate();
                }
            }
        }

        public bool CanResize
        {
            get
            {
                return _canResize;
            }
            set
            {
                if (_canResize != value)
                {
                    _canResize = value;
                }
            }
        }

        public override Image BackgroundImage
        {
            get
            {
                return _formBkg;
            }
            set
            {
                if (_formBkg != value)
                {
                    _formBkg = value;
                    Invalidate();
                }
            }
        }

        internal Rectangle IconRect
        {
            get
            {
                if (base.ShowIcon && base.Icon != null)
                {
                    return new Rectangle(8, 6, SystemInformation.SmallIconSize.Width, SystemInformation.SmallIconSize.Width);
                }
                return Rectangle.Empty;
            }
        }

        internal Rectangle TextRect
        {
            get
            {
                if (base.Text.Length != 0)
                {
                    return new Rectangle(IconRect.Right + 2, 4, Width - (8 + IconRect.Width + 2), Font.Height);
                }
                return Rectangle.Empty;
            }
        }

        internal SystemButtonManager SystemButtonManager
        {
            get
            {
                if (_systemButtonManager == null)
                {
                    _systemButtonManager = new SystemButtonManager(this);
                }
                return _systemButtonManager;
            }
        }

        #endregion

        #region Overrides

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    if (MaximizeBox) { cp.Style |= (int)WindowStyle.WS_MAXIMIZEBOX; }
                    if (MinimizeBox) { cp.Style |= (int)WindowStyle.WS_MINIMIZEBOX; }
                    //cp.ExStyle |= (int)WindowStyle.WS_CLIPCHILDREN;  //防止因窗体控件太多出现闪烁
                    cp.ClassStyle |= (int)ClassStyle.CS_DropSHADOW;  //实现窗体边框阴影效果
                }
                return cp;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            RenderHelper.SetFormRoundRectRgn(this, Radius);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RenderHelper.SetFormRoundRectRgn(this, Radius);
            UpdateSystemButtonRect();
            UpdateMaxButton();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_ERASEBKGND:
                    m.Result = IntPtr.Zero;
                    break;
                case Win32.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Down);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                SystemButtonManager.ProcessMouseOperate(e.Location, MouseOperate.Up);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SystemButtonManager.ProcessMouseOperate(Point.Empty, MouseOperate.Leave);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //draw BackgroundImage
            e.Graphics.DrawImage(_formBkg, ClientRectangle, new Rectangle(0, 0, _formBkg.Width, _formBkg.Height), GraphicsUnit.Pixel);

            //draw form main part
            RenderHelper.DrawFromAlphaMainPart(this, e.Graphics);

            //draw system buttons
            SystemButtonManager.DrawSystemButtons(e.Graphics);

            //draw fringe
            RenderHelper.DrawFormFringe(this, e.Graphics, _fringe, Radius);

            //draw icon
            if (Icon != null && ShowIcon)
            {
                e.Graphics.DrawIcon(Icon, IconRect);
            }

            //draw text
            if (Text.Length != 0)
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    Text, Font,
                    TextRect,
                    Color.White,
                    TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            }



        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_systemButtonManager != null)
                {
                    _systemButtonManager.Dispose();
                    _systemButtonManager = null;
                    _formBkg.Dispose();
                    _formBkg = null;
                    _fringe.Dispose();
                    _fringe = null;
                }
            }
        }

        #endregion

        #region Private Methods

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEx));
            this.mGroupBox1 = new Momo.Forms.MGroupBox();
            this.dpDrawPanel = new SCDesignClient.DesignerPanel();
            this.mTabControl1 = new Momo.Forms.MTabControl();
            this.dtpBase = new Momo.Forms.DTabPage();
            this.dtpControl = new Momo.Forms.DTabPage();
            this.dtpCollection = new Momo.Forms.DTabPage();
            this.itemInfo = new Momo.Forms.MRichTextBox();
            this.mGroupBox1.SuspendLayout();
            this.mTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mGroupBox1
            // 
            this.mGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mGroupBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.mGroupBox1.BorderColor = System.Drawing.Color.Empty;
            this.mGroupBox1.Controls.Add(this.dpDrawPanel);
            this.mGroupBox1.Location = new System.Drawing.Point(12, 40);
            this.mGroupBox1.Name = "mGroupBox1";
            this.mGroupBox1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.mGroupBox1.Size = new System.Drawing.Size(667, 582);
            this.mGroupBox1.TabIndex = 1;
            this.mGroupBox1.Text = "设计区";
            this.mGroupBox1.TitleColor = System.Drawing.Color.Black;
            this.mGroupBox1.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.mGroupBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mGroupBox1_MouseDown);
            this.mGroupBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mGroupBox1_MouseMove);
            this.mGroupBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mGroupBox1_MouseUp);
            // 
            // dpDrawPanel
            // 
            this.dpDrawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpDrawPanel.BackColor = System.Drawing.SystemColors.HighlightText;
            this.dpDrawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dpDrawPanel.Location = new System.Drawing.Point(12, 40);
            this.dpDrawPanel.Name = "dpDrawPanel";
            this.dpDrawPanel.Size = new System.Drawing.Size(643, 526);
            this.dpDrawPanel.TabIndex = 0;
            // 
            // mTabControl1
            // 
            this.mTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mTabControl1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.mTabControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.mTabControl1.Controls.Add(this.dtpCollection);
            this.mTabControl1.Controls.Add(this.dtpBase);
            this.mTabControl1.Controls.Add(this.dtpControl);
            this.mTabControl1.GridLineColor = System.Drawing.Color.White;
            this.mTabControl1.Location = new System.Drawing.Point(685, 40);
            this.mTabControl1.Name = "mTabControl1";
            this.mTabControl1.SelectedIndex = 2;
            this.mTabControl1.Size = new System.Drawing.Size(358, 415);
            this.mTabControl1.TabIndex = 0;
            this.mTabControl1.Text = "mTabControl1";
            this.mTabControl1.SelectedChanged += new Momo.Forms.Action(this.mTabControl1_SelectedChanged);
            // 
            // dtpBase
            // 
            this.dtpBase.AutoScroll = true;
            this.dtpBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BackColorGradint = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BorderBottomWidth = 1;
            this.dtpBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BorderLeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BorderLeftWidth = 1;
            this.dtpBase.BorderRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BorderRightWidth = 1;
            this.dtpBase.BorderTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpBase.BorderTopWidth = 1;
            this.dtpBase.BorderWidth = 1;
            this.dtpBase.LinearGradientMode = Momo.Forms.GradientMode.Horizontal;
            this.dtpBase.Location = new System.Drawing.Point(0, 40);
            this.dtpBase.Name = "dtpBase";
            this.dtpBase.Padding = new System.Windows.Forms.Padding(0);
            this.dtpBase.Size = new System.Drawing.Size(358, 375);
            this.dtpBase.TabIndex = 1;
            this.dtpBase.Text = "组件";
            // 
            // dtpControl
            // 
            this.dtpControl.AutoScroll = true;
            this.dtpControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BackColorGradint = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BorderBottomWidth = 1;
            this.dtpControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BorderLeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BorderLeftWidth = 1;
            this.dtpControl.BorderRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BorderRightWidth = 1;
            this.dtpControl.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.dtpControl.BorderTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpControl.BorderTopWidth = 1;
            this.dtpControl.BorderWidth = 1;
            this.dtpControl.LinearGradientMode = Momo.Forms.GradientMode.Horizontal;
            this.dtpControl.Location = new System.Drawing.Point(0, 40);
            this.dtpControl.Name = "dtpControl";
            this.dtpControl.Padding = new System.Windows.Forms.Padding(0);
            this.dtpControl.Size = new System.Drawing.Size(358, 375);
            this.dtpControl.TabIndex = 3;
            this.dtpControl.Text = "控制";
            // 
            // dtpCollection
            // 
            this.dtpCollection.AutoScroll = true;
            this.dtpCollection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BackColorGradint = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BorderBottomWidth = 1;
            this.dtpCollection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BorderLeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BorderLeftWidth = 1;
            this.dtpCollection.BorderRightColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BorderRightWidth = 1;
            this.dtpCollection.BorderTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.dtpCollection.BorderTopWidth = 1;
            this.dtpCollection.BorderWidth = 1;
            this.dtpCollection.LinearGradientMode = Momo.Forms.GradientMode.Horizontal;
            this.dtpCollection.Location = new System.Drawing.Point(0, 40);
            this.dtpCollection.Name = "dtpCollection";
            this.dtpCollection.Padding = new System.Windows.Forms.Padding(0);
            this.dtpCollection.Size = new System.Drawing.Size(358, 375);
            this.dtpCollection.TabIndex = 2;
            this.dtpCollection.Text = "集合";
            // 
            // itemInfo
            // 
            this.itemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.itemInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.itemInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.itemInfo.Location = new System.Drawing.Point(685, 461);
            this.itemInfo.MaxLines = 0;
            this.itemInfo.Name = "itemInfo";
            this.itemInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.itemInfo.Size = new System.Drawing.Size(358, 161);
            this.itemInfo.TabIndex = 2;
            this.itemInfo.Text = "";
            this.itemInfo.WaterForeColor = System.Drawing.SystemColors.WindowText;
            this.itemInfo.WaterText = "水印文字";
            // 
            // FormEx
            // 
            this.ClientSize = new System.Drawing.Size(1055, 634);
            this.Controls.Add(this.itemInfo);
            this.Controls.Add(this.mGroupBox1);
            this.Controls.Add(this.mTabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEx";
            this.Text = "SCD助手";
            this.Load += new System.EventHandler(this.FormEx_Load);
            this.mGroupBox1.ResumeLayout(false);
            this.mTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void FormExIni()
        {
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            SetStyles();
        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void WmNcHitTest(ref Message m)  //调整窗体大小
        {
            int wparam = m.LParam.ToInt32();
            Point mouseLocation = new Point(RenderHelper.LOWORD(wparam), RenderHelper.HIWORD(wparam));
            mouseLocation = PointToClient(mouseLocation);

            if (WindowState != FormWindowState.Maximized)
            {
                if (CanResize == true)
                {
                    if (mouseLocation.X < 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32.HTTOPLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32.HTTOPRIGHT);
                        return;
                    }

                    if (mouseLocation.X < 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOMLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOMRIGHT);
                        return;
                    }

                    if (mouseLocation.Y < 3)
                    {
                        m.Result = new IntPtr(Win32.HTTOP);
                        return;
                    }

                    if (mouseLocation.Y > Height - 3)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOM);
                        return;
                    }

                    if (mouseLocation.X < 3)
                    {
                        m.Result = new IntPtr(Win32.HTLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 3)
                    {
                        m.Result = new IntPtr(Win32.HTRIGHT);
                        return;
                    }
                }
            }
            m.Result = new IntPtr(Win32.HTCLIENT);
        }

        private void UpdateMaxButton()  //根据窗体的状态更换最大(还原)系统按钮
        {
            bool isMax = WindowState == FormWindowState.Maximized;
            if (isMax)
            {
                SystemButtonManager.SystemButtonArray[1].NormalImg = Image.FromFile(@".\Res\SystemButton\restore_normal.png"); 
                SystemButtonManager.SystemButtonArray[1].HighLightImg = Image.FromFile(@".\Res\SystemButton\restore_highlight.png"); 
                SystemButtonManager.SystemButtonArray[1].PressedImg = Image.FromFile(@".\Res\SystemButton\restore_press.png");
                SystemButtonManager.SystemButtonArray[1].ToolTip = "还原";
            }
            else
            {
                SystemButtonManager.SystemButtonArray[1].NormalImg = Image.FromFile(@".\Res\SystemButton\max_normal.png"); 
                SystemButtonManager.SystemButtonArray[1].HighLightImg = Image.FromFile(@".\Res\SystemButton\max_highlight.png"); 
                SystemButtonManager.SystemButtonArray[1].PressedImg = Image.FromFile(@".\Res\SystemButton\max_press.png");
                SystemButtonManager.SystemButtonArray[1].ToolTip = "最大化";
            }
        }

        protected void UpdateSystemButtonRect()
        {
            bool isShowMaxButton = MaximizeBox;
            bool isShowMinButton = MinimizeBox;
            Rectangle closeRect = new Rectangle(
                    Width - SystemButtonManager.SystemButtonArray[0].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[0].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[0].NormalImg.Height);

            //update close button location rect.
            SystemButtonManager.SystemButtonArray[0].LocationRect = closeRect;
                
            //Max
            if (isShowMaxButton)
            {
                SystemButtonManager.SystemButtonArray[1].LocationRect = new Rectangle(
                    closeRect.X - SystemButtonManager.SystemButtonArray[1].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[1].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[1].NormalImg.Height);
            }
            else
            {
                SystemButtonManager.SystemButtonArray[1].LocationRect = Rectangle.Empty;
            }

            //Min
            if (!isShowMinButton)
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = Rectangle.Empty;
                return;
            }
            if (isShowMaxButton)
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(
                    SystemButtonManager.SystemButtonArray[1].LocationRect.X - SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                    -1,
                    SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                    SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
            else
            {
                SystemButtonManager.SystemButtonArray[2].LocationRect = new Rectangle(
                   closeRect.X - SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                   -1,
                   SystemButtonManager.SystemButtonArray[2].NormalImg.Width,
                   SystemButtonManager.SystemButtonArray[2].NormalImg.Height);
            }
        }

        #endregion

        private void FormEx_Load(object sender, EventArgs e)
        {
            // config path
            string configPath = Path.Combine(Application.StartupPath, "ItemConfigs");

            if (Directory.Exists(configPath))
            {
                Console.WriteLine("exist path");
                initConfig(configPath);
            }
            else
            {
                Console.WriteLine("not exist path");
            }

            // 初始化可用组件
            createControlItems();
            createBaseItems();
            createCollectionItems();
        }

        private void createControlItems()
        {
            List<ItemClass> currentItems = new List<ItemClass>();

            for (int indexTemp = 0; indexTemp < m_itemLists.Count; indexTemp++)
            {
                if (m_itemLists[indexTemp].itemtype != ItemType.Control)
                {
                    continue;
                }

                currentItems.Add(m_itemLists[indexTemp]);
            }
            // 计算每个item 尺寸
            int eachCtrlWd = (int)((mTabControl1.Width / rowSize * 1.0));

            for (int index = 0; index < currentItems.Count; index++)
            {
                int tmpColIndex = index % rowSize;
                int tmpRowIndex = index / rowSize;

                ItemClass ctrlWd = currentItems[index];

                ctrlWd.location = new List<int> { tmpColIndex * (eachCtrlWd), tmpRowIndex * (eachCtrlWd) };
                ctrlWd.size = new List<int> { (int)(eachCtrlWd), (int)(eachCtrlWd) };

                ItemControl ic = new ItemControl(ctrlWd);
                ic.Location = ctrlWd.MyLocation();
                ic.Name = "test" + index;

                ic.FocusChangeEvent += ic_FocusChangeEvent;

                dtpControl.Controls.Add(ic);
            }
        }

        private void createBaseItems()
        {
            List<ItemClass> currentItems = new List<ItemClass>();

            for (int indexTemp = 0; indexTemp < m_itemLists.Count; indexTemp++)
            {
                if (m_itemLists[indexTemp].itemtype != ItemType.Module)
                {
                    continue;
                }

                currentItems.Add(m_itemLists[indexTemp]);
            }
            // 计算每个item 尺寸
            int eachCtrlWd = (int)((mTabControl1.Width / rowSize * 1.0));

            for (int index = 0; index < currentItems.Count; index++)
            {
                int tmpColIndex = index % rowSize;
                int tmpRowIndex = index / rowSize;

                ItemClass ctrlWd = currentItems[index];

                ctrlWd.location = new List<int> { tmpColIndex * (eachCtrlWd), tmpRowIndex * (eachCtrlWd) };
                ctrlWd.size = new List<int> { (int)(eachCtrlWd), (int)(eachCtrlWd ) };

                ItemControl ic = new ItemControl(ctrlWd);
                ic.Location = ctrlWd.MyLocation();
                ic.Name = "test" + index;

                ic.FocusChangeEvent += ic_FocusChangeEvent;

                dtpBase.Controls.Add(ic);
            }
        }

        private void createCollectionItems()
        {
            List<ItemClass> currentItems = new List<ItemClass>();

            for (int indexTemp = 0; indexTemp < m_itemLists.Count; indexTemp++)
            {
                if (m_itemLists[indexTemp].itemtype != ItemType.Collection)
                {
                    continue;
                }

                currentItems.Add(m_itemLists[indexTemp]);
            }
            // 计算每个item 尺寸
            int eachCtrlWd = (int)((mTabControl1.Width / rowSize * 1.0));

            for (int index = 0; index < currentItems.Count; index++)
            {
                int tmpColIndex = index % rowSize;
                int tmpRowIndex = index / rowSize;

                ItemClass ctrlWd = currentItems[index];

                ctrlWd.location = new List<int> { tmpColIndex * (eachCtrlWd), tmpRowIndex * (eachCtrlWd) };
                ctrlWd.size = new List<int> { (int)(eachCtrlWd), (int)(eachCtrlWd) };

                ItemControl ic = new ItemControl(ctrlWd);
                ic.Location = ctrlWd.MyLocation();
                ic.Name = "test" + index;

                ic.FocusChangeEvent += ic_FocusChangeEvent;

                dtpCollection.Controls.Add(ic);
            }
        }
        private void ic_FocusChangeEvent(object sender, ActiveEventArgs e)
        {
            ItemControl ic = sender as ItemControl;
            if (ic == null)
            {
                return;
            }


            // 修改鼠标状态
            ItemClass itemData = ic.getData();
            dpDrawPanel.setCursor(e.State, itemData);

            if (itemData.itemtype == ItemType.Control)
            {
                for (int index = 0; index < dtpControl.Controls.Count; index++)
                {
                    ItemControl oneCtrl = dtpControl.Controls[index] as ItemControl;

                    if (oneCtrl.Name == ic.Name)
                    {
                        continue;
                    }
                    oneCtrl.SetActive(false);
                }
            }
            else if (itemData.itemtype == ItemType.Module)
            {
                for (int index = 0; index < dtpBase.Controls.Count; index++)
                {
                    ItemControl oneCtrl = dtpBase.Controls[index] as ItemControl;

                    if (oneCtrl.Name == ic.Name)
                    {
                        continue;
                    }
                    oneCtrl.SetActive(false);
                }
            }
            else if (itemData.itemtype == ItemType.Collection)
            {
                for (int index = 0; index < dtpCollection.Controls.Count; index++)
                {
                    ItemControl oneCtrl = dtpCollection.Controls[index] as ItemControl;

                    if (oneCtrl.Name == ic.Name)
                    {
                        continue;
                    }
                    oneCtrl.SetActive(false);
                }
            }

            itemInfo.Text = itemData.itemInfo;
        }
        public string Read(string path)
        {
            string rtnValue = null;

            try
            {

            
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                if (rtnValue == null)
                {
                    rtnValue = line.ToString();
                }
                else
                {
                    rtnValue = rtnValue + line.ToString();
                }
                
            }
                }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtnValue;
        }
        private void initConfig(string configPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(configPath);
                //不是目录
                if (dir == null) return;

                FileSystemInfo[] files = dir.GetFileSystemInfos();
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo file = files[i] as FileInfo;
                    //是文件
                    if (file != null && file.Extension.ToLower() == ".setting")
                    {
                        Console.WriteLine(file.FullName);

                        // 读取文件内容
                        string cfgValue = Read(file.FullName);
                        var entity = cfgValue.FromJSON<ItemClass>();//Json转为实体对象

                        m_itemLists.Add(entity);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (System.Exception err)
            {
                Console.WriteLine(err.Message);
            }        
            
        }

        private void mGroupBox1_MouseDown(object sender, MouseEventArgs e)
        {
           // Console.WriteLine("FormEx-MouseDown");
        }

        private void mGroupBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("FormEx-MouseMove");
        }

        private void mGroupBox1_MouseUp(object sender, MouseEventArgs e)
        {
           // Console.WriteLine("FormEx-MouseUp");
        }

        private void mTabControl1_SelectedChanged()
        {
            #region 关闭激活
            for (int index = 0; index < dtpControl.Controls.Count; index++)
            {
                ItemControl oneCtrl = dtpControl.Controls[index] as ItemControl;

                oneCtrl.SetActive(false);
            }
            for (int index = 0; index < dtpBase.Controls.Count; index++)
            {
                ItemControl oneCtrl = dtpBase.Controls[index] as ItemControl;

                oneCtrl.SetActive(false);
            }

            for (int index = 0; index < dtpCollection.Controls.Count; index++)
            {
                ItemControl oneCtrl = dtpCollection.Controls[index] as ItemControl;


                oneCtrl.SetActive(false);
            }
            itemInfo.Text = "";
            #endregion
        }
    }
}
