using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VisaCzech.UI
{
    public partial class TouchListBox : ListBox
    {
        public class DrawTextEventArgs
        {
            public DrawItemEventArgs DrawItemArgs;
            public string Text;
            public Font TextFont;
            public Brush TextBrush;
            public PointF SuggestedPosition;
            public bool ItemIsSelected;
        }

        public enum ArrowSizeModes
        {
            Percents,
            Pixels
        }

        public delegate void OnDrawText(object sender, DrawTextEventArgs e);

        public event OnDrawText DrawItemText;

        protected Color _arrowColor;
        protected Color _frameColor;
        protected Color _selItemColor;
        protected bool _showArrow;
        protected Color _visitedColor;
        protected Color _itemColor;
        protected bool _markVisited;
        protected Dictionary<object, bool> _visitedItems;
        protected int _arrowSize;
        protected int _arrowSizePercents;
        protected ArrowSizeModes _arrowSizeMode;

        public bool ShowArrow
        {
            get
            {
                return _showArrow;
            }
            set
            {
                _showArrow = value;
                Refresh();
            }
        }

        public Color ArrowColor
        {
            get
            {
                return _arrowColor;
            }
            set
            {
                _arrowColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Arrow size (pixels of the height)
        /// </summary>
        public int ArrowSize
        {
            get
            {
                return _arrowSize;
            }
            set
            {
                _arrowSize = value;
                if (_arrowSizeMode == ArrowSizeModes.Pixels)
                    Refresh();
            }
        }

        /// <summary>
        /// Arrow size (pixels of the height)
        /// </summary>
        public int ArrowSizePercents
        {
            get
            {
                return _arrowSizePercents;
            }
            set
            {
                _arrowSizePercents = value;
                if (_arrowSizePercents > 100) _arrowSizePercents = 100;
                if (_arrowSizePercents < 0) _arrowSizePercents = 0;
                if (_arrowSizeMode == ArrowSizeModes.Percents)
                    Refresh();
            }
        }

        public ArrowSizeModes ArrowSizeMode
        {
            get
            {
                return _arrowSizeMode;
            }
            set
            {
                _arrowSizeMode = value;
                Refresh();
            }
        }

        public Color FrameColor
        {
            get
            {
                return _frameColor;
            }
            set
            {
                _frameColor = value;
                Refresh();
            }
        }

        public Color SelectedItemColor
        {
            get
            {
                return _selItemColor;
            }
            set
            {
                _selItemColor = value;
                Refresh();
            }
        }

        public int DefaultSelectedIndex
        {
            get
            {
                return SelectedIndex;
            }
            set
            {
                if ((value >= 0) && (value < this.Items.Count))
                    SelectedIndex = value;
            }
        }

        public Color VisitedColor
        {
            get
            {
                return _visitedColor;
            }
            set
            {
                _visitedColor = value;
                Refresh();
            }
        }

        public Color NotVisitedColor
        {
            get
            {
                return _itemColor;
            }
            set
            {
                _itemColor = value;
            }
        }

        public bool MarkVisitedItems
        {
            get
            {
                return _markVisited;
            }
            set
            {
                _markVisited = value;
                Refresh();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TouchListBox()
        {
            _visitedItems = new Dictionary<object, bool>();

            IntegralHeight = false;
            DrawMode = DrawMode.OwnerDrawFixed;
            //BorderStyle = BorderStyle.None;
            ItemHeight = 30;
            DrawItem += new DrawItemEventHandler(TouchListBox_DrawItem);

            _arrowColor = Color.FromKnownColor(KnownColor.Control);
            _selItemColor = Color.FromArgb(200, 200, 255);
            _frameColor = Color.FromKnownColor(KnownColor.Silver);

            _showArrow = true;
            _arrowSizePercents = 70;
            _arrowSize = 20;
            _arrowSizeMode = ArrowSizeModes.Pixels;
            _markVisited = true;
            _visitedColor = Color.FromArgb(245, 245, 245);
            _itemColor = Color.White;
            
        }

        public void VisitItem(object item)
        {
            MarkItemVisit(item, true);
        }

        public void ClearVisitedItems()
        {
            _visitedItems.Clear();
        }

        public void UnvisitItem(object item)
        {
            MarkItemVisit(item, false);
        }

        protected void MarkItemVisit(object item, bool visit)
        {
            if (item == null) return;
            if (_visitedItems.ContainsKey(item))
            {
                _visitedItems[item] = visit;
            }
            else
            {
                _visitedItems.Add(item, visit);
            }
        }

        void TouchListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            using (Brush backBrush = new SolidBrush(BackColor),
                         itemBrush = new SolidBrush(_itemColor),
                         selBackBrush = new SolidBrush(_selItemColor),
                         textBrush = new SolidBrush(ForeColor),
                         visitedBrush = new SolidBrush(_visitedColor),
                         arrowBrush = new SolidBrush(_arrowColor))
            {
                using (Pen borderPen = new Pen(_frameColor),
                           backPen = new Pen(BackColor))
                {
                    // Если мы рисуем за границей элементов списка, то кроме фона нам ничего не надо
                    if ((e.Index < 0) && (e.Index >= this.Items.Count))
                    {
                        // закрашиваем фон и выходим
                        e.Graphics.FillRectangle(backBrush, e.Bounds);
                        return;
                    }

                    try
                    {
                        bool itemSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

                        // закрашиваем фон в сответствии с выбранным элементом
                        Brush fillBrush;
                        if (itemSelected)
                            fillBrush = selBackBrush;
                        else
                        {
                            if (_markVisited)
                                fillBrush = (ItemVisited(e.Index) ? visitedBrush : itemBrush);
                            else
                                fillBrush = itemBrush;
                        }
                        e.Graphics.FillRectangle(fillBrush, e.Bounds);

                        // рисуем текст
                        string text = this.Items[e.Index].ToString();
                        int x, y, w, h;
                        w = e.Bounds.Width;
                        h = e.Bounds.Height;
                        x = e.Bounds.Left + 5;
                        y = e.Bounds.Top + (e.Bounds.Height / 2);
                        SizeF textSize;
                        while (true)
                        {
                            textSize = e.Graphics.MeasureString(text, Font);
                            if (textSize.Width < w - 5) break;
                            text = text.Substring(0, text.Length - 1);
                        }

                        y -= (int)(textSize.Height / 2);

                        // проверяем, есть ли обработчик события на рисование текста. если нет, то рисуем сами
                        AskToDrawText(e, textBrush, text, x, y, itemSelected);

                        // рисуем рамку
                        e.Graphics.DrawLine(borderPen,
                            e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);
                        e.Graphics.DrawLine(borderPen,
                            e.Bounds.Left, e.Bounds.Bottom, e.Bounds.Right, e.Bounds.Bottom);

                        if (_showArrow && itemSelected)
                        {
                            int aHeight;
                            if (_arrowSizeMode == ArrowSizeModes.Percents)
                                aHeight = (int)(h * ((double)_arrowSizePercents / 100.0d));
                            else
                                aHeight = _arrowSize;
                            int delta = aHeight % 2;
                            
                            y = (h - aHeight) / 2;
                            Point[] pts =
                                new Point[] 
                                { 
                                    new Point(e.Bounds.Right, e.Bounds.Top+y+delta),
                                    new Point(e.Bounds.Right-aHeight/2, e.Bounds.Top+h/2), 
                                    new Point(e.Bounds.Right, e.Bounds.Top+y+aHeight)                                   
                                };
                            e.Graphics.FillPolygon(arrowBrush, pts);
                            e.Graphics.DrawLines(borderPen, pts);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected void AskToDrawText(DrawItemEventArgs e, Brush textBrush, string text, int x, int y, bool itemSelected)
        {
            Font textFont = new Font(Font, FontStyle.Bold);
            DrawTextEventArgs args = new DrawTextEventArgs();
            args.DrawItemArgs = e;
            args.SuggestedPosition = new PointF(x, y);
            args.Text = text;
            args.TextBrush = textBrush;
            args.ItemIsSelected = itemSelected;

            args.TextFont = itemSelected ? textFont : Font;

            if (DrawItemText != null)
                DrawItemText(this, args);
            else 
                DrawTextDefault(args);

            textFont.Dispose();
        }

        protected bool ItemVisited(int index)
        {
            if ((index < 0) && (index >= this.Items.Count)) return false;
            object item = Items[index];
            if (!_visitedItems.ContainsKey(item)) return false;
            return _visitedItems[item];
        }

        protected static void DrawTextDefault(DrawTextEventArgs e)
        {
            
            e.DrawItemArgs.Graphics.DrawString(e.Text, e.TextFont, e.TextBrush, e.SuggestedPosition);
        }


    }
}
