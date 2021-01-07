using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sqlsearch sqlsearch = new Sqlsearch(sqltext.Text);
            sqlsearch.analysis();
         List<List<string>> a =  sqlsearch.GetData();
            while (dataGridView1.Rows.Count > 1) { dataGridView1.Rows.RemoveAt(0); }//第二次按下按钮的时候先清除表格
            while (dataGridView1.Columns.Count > 0) { dataGridView1.Columns.RemoveAt(0); }//第二次按下按钮的时候先清除表格
            for (int num = 0; num < a.Count; num++)
            {
                int index=0;
                if (num != 0)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                  index = this.dataGridView1.Rows.Add(dr);
                   
                }//先要创建列才能创建行
                for (int num1 = 0; num1 < a[num].Count; num1++)
                {
                    if (num == 0)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        acCode.Name = a[num][num1];
                        this.dataGridView1.Columns.Add(acCode);
                    }//第一次创建列并以文件第一行命名
                    else
                    this.dataGridView1.Rows[index].Cells[num1].Value = a[num][num1];
                }
            }
        }//提交

        private void button2_Click(object sender, EventArgs e)
        {
            sqltext.Text = "";
        }//清除

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }
    }
}
