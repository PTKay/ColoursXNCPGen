using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoursXncpGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(args[0]));
            string filename = Path.GetFileNameWithoutExtension(args[0]);
            byte[] file = File.ReadAllBytes(args[0]);
            string dxlFilename = $"{filename}.dxl";
            if (Encoding.Default.GetString(file.Range(0, 4)).Equals("FAPC"))
            {
                byte[][] split = FileManager.Split(file);
                File.WriteAllBytes(args[0], split[0]);
                File.WriteAllBytes(dxlFilename, split[1]);
                return;
            }
            
            if (!File.Exists(dxlFilename))
            {
                Console.WriteLine("Could not find associated DXL file, aborting...");
                return;
            }

            byte[] dxlFile = File.ReadAllBytes(dxlFilename);

            byte[] output = FileManager.Combine(file, dxlFile);

            File.Delete(dxlFilename);
            File.WriteAllBytes(args[0], output);

        }
    }
}
