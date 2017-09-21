using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCOperationalPlan
{
    class MyGrigUtils
    {
        public static void ArrangeDataGrid(DataGridView grd, Dictionary<string, int> colDetails)
        {
            grd.AutoGenerateColumns = false;
            grd.RowHeadersVisible = false;
            grd.MultiSelect = false;
            grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grd.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grd.AllowUserToAddRows = false;

            int x = colDetails.Count;
            grd.ColumnCount = x;

            for (int i = 0; i < colDetails.Count; i++)
            {
                grd.Columns[i].Name = colDetails.ElementAt(i).Key.ToString();
                grd.Columns[i].Width = colDetails.ElementAt(i).Value;
            }

            grd.Font = new Font("Tahoma", 10);
        }
        public static void ArrangeDataGrid(DataGridView grd, Dictionary<string, int> colDetails,Color clr1, Color clr2)
        {
            grd.AutoGenerateColumns = false;
            grd.RowHeadersVisible = false;
            grd.MultiSelect = false;
            grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grd.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            grd.AlternatingRowsDefaultCellStyle.BackColor = clr1;
            grd.DefaultCellStyle.BackColor = clr2;

            grd.AllowUserToAddRows = false;

            int x = colDetails.Count;
            grd.ColumnCount = x;

            for (int i = 0; i < colDetails.Count; i++)
            {
                grd.Columns[i].Name = colDetails.ElementAt(i).Key.ToString();
                grd.Columns[i].Width = colDetails.ElementAt(i).Value;
            }

            grd.Font = new Font("Tahoma", 10);
        }
        public static void ArrangeDataGrid(DataGridView grd, Dictionary<string, int> colDetails, int[] hideRows, Color clr1, Color clr2)
        {
            grd.AutoGenerateColumns = false;
            grd.RowHeadersVisible = false;
            grd.MultiSelect = false;
            grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grd.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            grd.AlternatingRowsDefaultCellStyle.BackColor = clr1;
            grd.DefaultCellStyle.BackColor = clr2;

            grd.AllowUserToAddRows = false;
            
            int x = colDetails.Count;
            grd.ColumnCount = x;

            for (int i = 0; i < colDetails.Count; i++)
            {
                grd.Columns[i].Name = colDetails.ElementAt(i).Key.ToString();
                grd.Columns[i].Width = colDetails.ElementAt(i).Value;
            }

            for (int i = 0; i < hideRows.Length; i++)
            {
                grd.Columns[hideRows[i]].Visible = false;
            }

            grd.Font = new Font("Tahoma", 10);
        }

        public static void ArrangeListView(ListView lst, Dictionary<string, int> colDetails)
        {
            lst.View = View.Details;
            lst.Font = new Font("Tahoma", 10);
            lst.GridLines = true;

            foreach (KeyValuePair<string, int> pair in colDetails)
            {
                lst.Columns.Add(pair.Key, pair.Value);
            }
        }

        public static void ListViewAlternateRowColor(ListView lv, Color clr1, Color clr2)
        {
            for (int ix = 0; ix < lv.Items.Count; ++ix)
            {
                var item = lv.Items[ix];
                item.BackColor = (ix % 2 == 0) ? clr1 : clr2;
            }
        }
        public static void ListViewAlternateRowColor(ListView lv, Color clr1, Color clr2, int rowToBeColor, int colorInRow)
        {
            foreach (ListViewItem item in lv.Items)
            {
                item.SubItems[rowToBeColor].BackColor = ColorTranslator.FromHtml(item.SubItems[colorInRow].Text);
                item.UseItemStyleForSubItems = false;
                for (int n = 0; n < item.SubItems.Count; n++)
                {
                    if (n != rowToBeColor)
                    {
                        if ((item.Index % 2) == 0)
                            item.SubItems[n].BackColor = clr1;
                        else
                            item.SubItems[n].BackColor = clr2;
                    }
                }
            }
        }

        public static void ColorDataGrid(DataGridView grd)
        {
            //grd.BackgroundColor = clr1;
            //grd.AlternatingRowsDefaultCellStyle.BackColor = clr2;
            try
            {
            }
            catch
            {
            }
            finally
            {
            }
        }
        public static void ColorDataGrid(DataGridView grd, int colorInRow)
        {
            //grd.BackgroundColor = clr1;
            //grd.AlternatingRowsDefaultCellStyle.BackColor = clr2;

            try
            {
                foreach (DataGridViewRow row in grd.Rows)
                {
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml(row.Cells[colorInRow].Value.ToString());
                    row.DefaultCellStyle.SelectionBackColor = row.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionForeColor = row.DefaultCellStyle.ForeColor;

                }
            }
            catch
            {
            }
            finally
            {
            }
        }
        public static void ColorDataGrid(DataGridView grd, int rowToBeColor, int colorInRow)
        {
            //grd.BackgroundColor = clr1;
            //grd.AlternatingRowsDefaultCellStyle.BackColor = clr2;

            try
            {
                foreach (DataGridViewRow row in grd.Rows)
                {
                    row.Cells[rowToBeColor].Style.BackColor = ColorTranslator.FromHtml(row.Cells[colorInRow].Value.ToString());
                    row.Cells[rowToBeColor].Style.SelectionBackColor = row.Cells[rowToBeColor].Style.BackColor;
                    row.Cells[rowToBeColor].Style.SelectionForeColor = row.Cells[rowToBeColor].Style.ForeColor;
                }
            }
            catch
            {

            }
            finally
            {
            }
        }

        public static ListViewItem listViewDeSelectPreviousItems(ListView lst, ItemCheckEventArgs e, ListViewItem lstItem)
        {
            // if we have the lastItem set as checked, and it is different
            // item than the one that fired the event, uncheck it
            if (lstItem != null && lstItem.Checked
                && lstItem != lst.Items[e.Index])
            {
                // uncheck the last item and store the new one
                lstItem.Checked = false;
                lst.Items[lstItem.Index].Selected = false;
            }
            lst.Items[e.Index].Selected = true;

            //select checked item

            // store current item
            return lstItem = lst.Items[e.Index];

        }
    }
}
