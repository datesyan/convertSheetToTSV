using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace convertToTSV
{
    class Program
    {
        static void Main(string[] args)
        {
        labelreset:
            System.Console.Write("input the file path :");
            string filePath;
            try
            {
                filePath = System.Console.ReadLine();
                string fileName = "";
                Console.WriteLine("processing...");
                string text = File.ReadAllText(filePath);
                int index = 0;
                string[] arr = text.Replace("\t", "").Split(new string[] { "Param data" }, StringSplitOptions.None);
            labelnewFile:
                string str2 = "";
                string sheetName = "";
                bool isKeyAdd = false;
                for (int i = index; i < arr.Length; i++)
                {

                    string[] pram = arr[i].Replace("=", "ⓔⓒⓔ").Split(new string[] { "ⓔ", Environment.NewLine }, StringSplitOptions.None);
                    for (int i1 = 0; i1 < pram.Length; i1++)
                    {
                        if (index != i)
                        {
                            if (pram[i1] == "ⓒ")
                            {
                                if (isKeyAdd)
                                {
                                    if (!str2.EndsWith(Environment.NewLine))
                                        str2 += "\t";
                                    str2 += pram[i1 + 1];
                                }
                                else
                                {
                                    if (!str2.EndsWith(Environment.NewLine) && str2 != "")
                                        str2 += "\t";
                                    str2 += pram[i1 - 1];
                                }
                            }
                            if (pram[i1].Contains("Sheet data"))
                            {
                                SafeCreateDirectory("convertedSheet\\" + fileName);
                                File.WriteAllText("convertedSheet\\"+fileName+"\\"+fileName + "." + sheetName + ".tsv", str2);
                                index = i;
                                goto labelnewFile;
                            }
                           
                        }
                        else
                        {
                            if (pram[i1].Contains("string m_Name") && index == 0)
                                fileName = pram[i1 + 2].Replace("\"", "").Replace(" ","");

                            if (pram[i1].Contains("String name"))
                                sheetName = pram[i1 + 2].Replace("\"","").Replace(" ", "");
                        }
                    }
                    if (!isKeyAdd&&index+1==i)
                    {
                        str2 += Environment.NewLine;
                        isKeyAdd = true;
                        i -= 1;
                    }
                    if (index != i)
                    {
                        str2 += Environment.NewLine;
                    }

                }

                Console.WriteLine("succeeeded");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            goto labelreset;

        }
        public static DirectoryInfo SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
        }
    }

}
