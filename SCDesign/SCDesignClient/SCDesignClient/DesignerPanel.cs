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
    public partial class DesignerPanel : UserControl
    {
        public Boolean _isWorking = false;
        public ItemClass _currentItem = null;


        public Boolean _mouseDown = false;
        public string _itemUUID = null;

        public List<ItemInstance> _itemsInstance = new List<ItemInstance>();

        public DesignerPanel()
        {
            InitializeComponent();
        }

        internal void setCursor(bool isWorking,ItemClass items)
        {
            if (isWorking)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }

            _currentItem = items;
            _isWorking = isWorking;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            for (int index = 0; index < _itemsInstance.Count; index ++ )
            {
                ItemInstance instance = _itemsInstance[index];

                string dirResource = Path.Combine(Application.StartupPath, "ItemImage");
                string imageFile = Path.Combine(dirResource, String.Format("{0}.png", (int)instance._data.shapetype));
                Image bmp = Image.FromFile(imageFile);

                Rectangle oldRect = new Rectangle(instance._location,instance._size);//背景图片需要被覆盖的部分
                Rectangle newRect = new Rectangle(0, 0, bmp.Width, bmp.Height);//嵌入图片需要被画入的部分
                g.DrawImage(bmp, oldRect, newRect, GraphicsUnit.Pixel);//在oldBmp中的oldRect画bmp中的newRect部分
            }

                base.OnPaint(e);
        }

        private void DesignerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_isWorking)
            {
                return;
            }

            // 移动部分
            ItemInstance selItem = checkSelection(e.Location);
            if (selItem != null)
            {
                _mouseDown = true;
                _itemUUID = selItem._uuid;

                // 
                return;
            }



            // 新增部分
            _currentItem.printValue();

            ItemInstance newObj = new ItemInstance();
            newObj._data = _currentItem;
            newObj._location = new Point(e.Location.X - newObj._size.Width / 2, e.Location.Y - newObj._size.Height / 2);
            

            _itemsInstance.Add(newObj);


            // 刷新指定范围
            this.Invalidate(new Rectangle(newObj._location.X - 1, newObj._location.Y - 1, newObj._size.Width + 2, newObj._size.Height + 2));
        }

        private ItemInstance checkSelection(Point point)
        {
            for (int index = 0; index < _itemsInstance.Count; index ++ )
            {
                ItemInstance item = _itemsInstance[index];

                Rectangle rect = new Rectangle(item._location.X - 1, item._location.Y - 1, item._size.Width + 2, item._size.Height + 2);

                Boolean isContain = rect.Contains(point);

                if (isContain)
                {
                    return item;
                }

            }
                return null;
        }

        private void DesignerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _itemUUID = null;
            _mouseDown = false;
        }

        private void DesignerPanel_MouseLeave(object sender, EventArgs e)
        {
            _itemUUID = null;
            _mouseDown = false;
        }

        private void DesignerPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown)
            {
                return;
            }

            if (_itemUUID == null)
            {
                return;
            }

            for (int index = _itemsInstance.Count - 1; index >= 0; index--)
            {
                ItemInstance item = _itemsInstance[index];

                if (item._uuid == _itemUUID)
                {
                    Point newPt = new Point(e.Location.X - item._size.Width / 2, e.Location.Y - item._size.Height / 2);

                    item._location = newPt;

                    break;
                }
            }

            this.Invalidate();
        }

    }

    public class ItemInstance
    {
        public string _uuid = System.Guid.NewGuid().ToString("N");
        public ItemClass _data;
        public Point _location;
        public Size _size = new Size(64,64);

        public List<ItemInstance> _inputList = new List<ItemInstance>();
        public ItemInstance _outPut;
    }
}
