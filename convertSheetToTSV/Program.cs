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
                Console.WriteLine("processing...");
                string text = File.ReadAllText(filePath);

                string[] arr = text.Replace("\t", "").Replace(" ", "").Split(new string[] { "Paramdata" }, StringSplitOptions.None);
                string str2 = "";
                bool isKeyAdd = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    
                    string[] pram = arr[i].Replace("=", "ⓔⓒⓔ").Split(new string[] { "ⓔ", Environment.NewLine }, StringSplitOptions.None);
                    switch (i)
                    {
                        case 0:
                            continue;
                        case 1:
                            if (!isKeyAdd)
                            {
                                for (int i2 = 0; i2 < pram.Length; i2++)
                                {
                                    if (pram[i2] == "ⓒ")
                                    {
                                        if (!str2.EndsWith(Environment.NewLine) && str2 != "")
                                            str2 += "\t";
                                        str2 += pram[i2 - 1];
                                    }
                                }
                                str2 += Environment.NewLine;
                                isKeyAdd = true;
                                i -= 1;
                                break;
                            }
                            else
                                goto default;
                        default:
                            for (int i3 = 0; i3 < pram.Length; i3++)
                            {
                                if (pram[i3] == "ⓒ")
                                {
                                    if (!str2.EndsWith(Environment.NewLine))
                                        str2 += "\t";
                                    str2 += pram[i3 + 1];
                                }
                            }
                            str2 += Environment.NewLine;
                            break;

                    }

                }
                File.WriteAllText(filePath + ".tsv", str2);
                System.Diagnostics.Process.Start(filePath + ".tsv");
                Console.WriteLine("succeeeded");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            goto labelreset;

        }
    }
}
