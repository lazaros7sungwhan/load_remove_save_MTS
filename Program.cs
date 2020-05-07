using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace load_remove_save
{
    class Program
    {
        static private void removing()
        {
            using(OpenFileDialog file_di=new OpenFileDialog())
            {
                file_di.Filter = "All_file.(*.*)|*.*";
                file_di.FilterIndex = '1';
                file_di.Multiselect = true;

                if (file_di.ShowDialog()==DialogResult.OK)
                {
                    string[] file_path_1 = file_di.FileNames;
                    foreach(string element in file_path_1)
                    {
                        string sub_path = null;
                        if (element.IndexOf(".txt") != -1)
                            sub_path = element.Remove(element.IndexOf(".txt"), ".txt".Length);
                        if (element.IndexOf(".dat") != -1)
                            sub_path = element.Remove(element.IndexOf(".dat"), ".dat".Length);
                        sub_path += "(R).txt";

                        using (StreamReader filesource=new StreamReader(element))
                        {
                            using (StreamWriter f_out=new StreamWriter(sub_path))
                            {
                                string line_1 = null;
                                int i = 0;
                                while((line_1=filesource.ReadLine())!=null)
                                {
                                    if(i<10)
                                    f_out.WriteLine(line_1);

                                    else
                                    {
                                        if((line_1!="")&&(line_1.IndexOf("MTS")==-1) && (line_1.IndexOf("Time") == -1) && (line_1.IndexOf("Test") == -1) 
                                            && (line_1.IndexOf("s") == -1) && (line_1.IndexOf("Data") == -1) && (line_1.IndexOf("Station") == -1))
                                        {
                                            f_out.WriteLine(line_1);
                                        }
                                    }
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            char func_num = '0';

            do
            {
                Console.WriteLine("1. test_!");
                Console.WriteLine("2. exit.");
                Thread.Sleep(10);
                Console.Clear();
                if(Console.KeyAvailable==true)
                {
                    func_num = Console.ReadKey(false).KeyChar;

                    switch (func_num)
                    {
                        case '1':
                            {
                                removing();
                                break;
                            }
                        case '2':
                            {
                                break;
                            }
                    }
                }
            } while (func_num != '2');
        }
    }
}
