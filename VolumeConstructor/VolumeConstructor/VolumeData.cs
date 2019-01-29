using System;
using Eto.Forms;
using Eto.Drawing;
using Dicom;
using Dicom.Imaging;

namespace VolumeConstructor
{
    public partial class MainForm
    {
        /// <summary>
        /// Process the position of each slice
        /// Then Generate a volume using it
        /// </summary>
        /// 

        decimal minPos;
        decimal maxPos;
        Color[][][] volume;

        public void ConfigureVolume()
        {
            // Find min and max positions
            minPos = slices[0].sliceLocation;
            maxPos = slices[0].sliceLocation;

            // Calculate position for each slice and store as location
            foreach (var slice in slices)
            {
                if (slice.sliceLocation - slice.sliceThickness / 2 < minPos)
                {
                    minPos = slice.sliceLocation - slice.sliceThickness / 2;
                }
                if (slice.sliceLocation + slice.sliceThickness / 2 > maxPos)
                {
                    maxPos = slice.sliceLocation + slice.sliceThickness / 2;
                }
            }
            
            // Output range of volume
            Console.WriteLine("Depth of volume: {0}", maxPos - minPos);

            // Prepare volume based on this range (rounded)
            volume = new Color[(int)Math.Round(maxPos - minPos) - 1][][];

            // After configuring volume, store each slicepoint for Transverse viewing
            int i = 0;
            foreach (var slice in slices)
            {
                // Relative position
                slicePoints[i++] = (int)Math.Round(slice.sliceLocation - minPos);
            }

            // Specify dimensions for each view
            bmpT = new Bitmap(gridSize, gridSize, PixelFormat.Format32bppRgba);
            bmpS = new Bitmap(gridSize, volume.Length, PixelFormat.Format32bppRgba);
            bmpC = new Bitmap(gridSize, volume.Length, PixelFormat.Format32bppRgba);
        }
        public void GenerateVolume()
        {
            foreach (var s in slices)
            {
                for (int i = (int)(Math.Round(s.sliceLocation - s.sliceThickness / 2) - minPos);
                    i < (int)(Math.Round(s.sliceLocation + s.sliceThickness / 2) - minPos); i++)
                {
                    // Use slice position to generate volume
                    volume[i] = s.sliceData;
                }
            }
        }
    }
}