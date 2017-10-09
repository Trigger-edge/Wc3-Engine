using System.Runtime.InteropServices;


namespace Wc3Engine
{
    class ExportBLP
    {
        [DllImport("BLPConverter.dll", EntryPoint = "Blp2Raw")]
        public static extern bool Blp2Raw(byte[] input, byte[] output, int width, int height, int bpp, int mipmaps, int alphaflag, int compresstype, int pictype, string filename);
        [DllImport("BLPConverter.dll", EntryPoint = "TGA2Raw")]
        public static extern bool TGA2Raw(byte[] input, byte[] output, int width, int height, int bpp, string filename);
        [DllImport("BLPConverter.dll", EntryPoint = "BMP2Raw")]
        public static extern bool BMP2Raw(byte[] input, byte[] output, int width, int height, int bpp, string filename);
        [DllImport("BLPConverter.dll", EntryPoint = "JPG2Raw")]
        public static extern bool JPG2Raw(byte[] input, byte[] output, int width, int height, int bpp, string filename);
        [DllImport("BLPConverter.dll", EntryPoint = "RAW2Tga")]
        public static extern bool RAW2Tga(byte[] input, byte[] output, int width, int height, int bpp, string filename);
        [DllImport("BLPConverter.dll", EntryPoint = "CreatePalettedBLP")]
        public static extern bool CreatePalettedBLP(byte[] rawData, byte[] output, int colors, string filename, int width, int height, int bytespp, int alphaflag, int maxmipmaps);
        [DllImport("BLPConverter.dll", EntryPoint = "CreateJpgBLP")]
        public static extern bool CreateJpgBLP(byte[] rawData, byte[] output, int quality, string filename, int width, int height, int bytespp, int alphaflag, int maxmipmaps);
    }

    public class BLP_TEST
    {
        public static byte[] CreateJpg(byte[] rawData, int quality, int width, int height, int alphaflag)
        {
            byte[] output = null;

            string file = @"C:\Users\Marshall\AppData\Roaming\TriggerEdge\test.blp";

            ExportBLP.CreateJpgBLP(rawData, rawData, quality, file, width, height, 0, alphaflag, 10);

            return output;
        }
    }
}
