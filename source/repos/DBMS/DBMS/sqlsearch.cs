using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS
{
    class Sqlsearch
    {
        public static Dictionary<string, int> pairs = new Dictionary<string, int> { { "SELECT", 1 }, { "FROM", 2 } };//关键词表
        public string sqlword;//sql语句
        public string txtname;//文件名前缀，文件在Bin/Debug里，因为是相对路径，注意不需要打后缀的txt
        public List<string> column=new List<string>();//sql语句中的参数名，包括表名和列名(这名字取得不是很好)
        public List<int> keyword = new List<int>();//截取出来的关键词在字典中所对应的Value
        public List<string> columnname = new List<string>();//选择的列名
        public List<List<string>> row = new List<List<string>>();//经过选择后的表格数据
        //初始化
        public Sqlsearch(string sqlword)
        {
            this.sqlword = sqlword;
        }
        //遇到空格则把之前所记录的string 分类放到链表里，如果字典里有则放到keyword否则放到column里
        public void analysis()
        {
            string read="";
           
            for (int num = 0; num < sqlword.Length; num++)
            {
                if (sqlword[num] == ' '||(num==sqlword.Length-1)&&sqlword[num]!=' ')
                {
                    if (num == sqlword.Length - 1)
                        read += sqlword[num];
                        if (read.Length > 0)
                    {
                        read = read.ToUpper();
                        if (pairs.ContainsKey((read)))
                            keyword.Add(pairs[read]);
                        else
                        {
                            column.Add(read);

                            MessageBox.Show(read);
                        }
                        read = "";
                    }
                   
                    continue;
                }
                else if ((sqlword[num] >= 'A' && sqlword[num] <= 'Z') || (sqlword[num] >= 'a' && sqlword[num] <= 'z'))
                    read += sqlword[num];
                else
                    read += sqlword[num];//这个地方有点问题的，之后肯定要改的


            }
          
            while (keyword.Count > 0)
            {
                switch (keyword[0])
                {
                    case 1: { columnname= new List<string>(column[0].Split(',')); ; column.RemoveAt(0); keyword.RemoveAt(0); break; }//选择的列名以逗号分割
                    case 2: { txtname = column[0]; column.RemoveAt(0); keyword.RemoveAt(0); break; }
                    default: keyword.RemoveAt(0); break;

                }//1，表示select的参数表。2，表示是文件名(即表名)



            }
        }//分析截取关键词和变量，放到两个list里

        //获得文件中表的所有数据
        public List<List<string>> GetData()
        {
            FileStream fs = new FileStream(txtname+".txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string str;
            int count = 0;
            int countnum = 0;
            String[][]strs=new String[100][];
            while ((str = sr.ReadLine())!=null)
            {

                 row.Add( new List<string>( str.Split(' ') ));//表中数据以空格分隔
                if(countnum==0)
                countnum++;
            }
            sr.Close();
            fs.Close();
            Select();
            return row;
            
        }
   
        //选择操作
        public void Select()
        {
            List<int> columnno = new List<int>();
            if (columnname[0] == "*" && columnname.Count == 1)//如果是*的话就什么都不做直接将原表交出去
            {; }
            else
            {

                for (int count = 0; count < columnname.Count; count++)
                    for (int count1 = 0; count1 < row[0].Count; count1++)
                    {
                        if (columnname[count] == row[0][count1])
                        {
                            columnno.Add(count1);
                            continue;
                        }

                    }//找到选择的列名所代表的列号

                for (int count1 = 0; count1 < row[0].Count; count1++)
                {
                    bool selected = false;
                    for (int count = 0; count < columnno.Count; count++)
                    {
                        if (columnname[count] == row[0][count1])
                        {
                            selected = true;
                            MessageBox.Show(columnname[count].ToString());
                            break;
                        }

                    } //select 1,2 from  my
                    if (!selected)
                    {
                        for (int count2 = 0; count2 < row.Count; count2++)
                        {
                            row[count2].RemoveAt(count1);

                        }
                        count1--;

                    }



                }//清除未选择的列



            }
        }



    }
}
