using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labaratorinis_01_HeapSort_D
{
    class MyFileArray : DataArray
    {
        public MyFileArray(string filename, int n, int seed)
        {
            double[] data = new double[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.NextDouble();
            }
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
                FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public FileStream fs { get; set; }
        public override double this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                fs.Seek(8 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                double result = BitConverter.ToDouble(data, 0);
                return result;
            }
        }

        public override void Swap(int indA, double a, int indB, double b)
        {
            Byte[] data = new Byte[8];          
            BitConverter.GetBytes(a).CopyTo(data, 0);
            fs.Seek(8 * (indB), SeekOrigin.Begin);
            fs.Write(data, 0, 8);
            BitConverter.GetBytes(b).CopyTo(data, 0);
            fs.Seek(8 * (indA), SeekOrigin.Begin);
            fs.Write(data, 0, 8);
        }
    }
}
