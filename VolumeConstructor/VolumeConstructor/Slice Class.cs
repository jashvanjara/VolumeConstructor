using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using Dicom;
using Dicom.Imaging;

namespace VolumeConstructor
{
    public class Slice
    {
        /// <summary>
        /// Slice class is how 2D dicom slices are stored and processed
        /// </summary>
        /// 

        public DicomDataset data;
        DicomImage dicomImage;
        Color[] LUT;

        // Storing SliceData as Color Array for now
        // Maybe better storing it as int data for 3D?
        public Color[][] sliceData;
        byte[] pixelData;
        int Width, Height, Depth;
        public decimal sliceLocation, sliceThickness;

        public Slice(DicomFile dF)
        {
            data = dF.Dataset;
            dicomImage = new DicomImage(data);
            Width = dicomImage.PixelData.Width;
            Height = dicomImage.PixelData.Height;

            // TODO: Read image depth
            Depth = 16;
            sliceLocation = dF.Dataset.GetValue<decimal>(DicomTag.SliceLocation, 0);
            sliceThickness = dF.Dataset.GetValue<decimal>(DicomTag.SliceThickness, 0);

            // TODO: Account for dicom files with more then one Frame
            pixelData = dicomImage.PixelData.GetFrame(0).Data;

            LoadLUT(@".\HotRed.lut");

            LoadPixels();
        }


        public void LoadLUT(string fileName)
        {
            LUT = new Color[ColorTable.LoadLUT(fileName).Length];
            int i = 0;
            foreach (var c in ColorTable.LoadLUT(fileName))
            {
                LUT[i++] = Color.FromArgb(c.Value);
            }
        }

        public void LoadPixels()
        {
            // Populate color array with pixel values from Dicom File
            sliceData = new Color[Width][];
            for (int x = 0; x < Width; x++)
            {
                sliceData[x] = new Color[Height];
                for (int y = 0; y < Height; y++)
                {
                    sliceData[x][y] = GetPixel(x, y);
                }
            }
        }

        public Color GetPixel(int x, int y)
        {
            // TODO: Add code to work with CT's/MRI's
            // Using raw data Array
            int cCount = Depth / 8;
            int pos = ((y * Width) + x) * cCount;

            if (pos + cCount > pixelData.Length)
            {
                // On error return Magenta
                return Colors.Magenta;
            }
            int a = pixelData[pos];
            int b = pixelData[pos + 1];

            // PET Data seems to leave the first 8 bits for something else?
            // Unsure what to do with variable `a`
            return LUT[b];

            // Raw Render (without LUT): 
            return Color.FromArgb(b,b,b);
        }
    }
}