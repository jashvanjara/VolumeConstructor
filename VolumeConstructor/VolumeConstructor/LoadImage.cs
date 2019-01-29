using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace VolumeConstructor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Loads 2D Image out of 3D volume
        /// </summary>
        /// 
                
        // Transverse Slice
        BitmapData bmpDataT;
        Bitmap bmpT;
        public void LoadTransverse(int layer, bool grid)
        {
            bmpDataT = bmpT.Lock();
            SetData(volume, 0, layer, ref bmpDataT);
            if (grid)
            {
                DrawHorizontal(ref bmpDataT, Colors.Yellow, nC, 256, 256);
                DrawVertical(ref bmpDataT, Colors.LightGreen, nS, 256, 256);
            }
            bmpDataT.Dispose();
        }

        // Sagital Slice
        BitmapData bmpDataS;
        Bitmap bmpS;
        public void LoadSagittal( int layer, bool grid)
        {
            bmpDataS = bmpS.Lock();
            SetData(volume, 1, layer, ref bmpDataS);
            if (grid)
            {
                DrawHorizontal(ref bmpDataS, Colors.CornflowerBlue, slicePoints[nT], 256, volume.Length);
                DrawVertical(ref bmpDataS, Colors.Yellow, nC, 256, volume.Length);
            }
            bmpDataS.Dispose();
        }

        // Coronal Slice
        BitmapData bmpDataC;
        Bitmap bmpC;
        public void LoadCoronal(int layer, bool grid)
        {
            bmpDataC = bmpC.Lock();
            SetData(volume, 2, layer, ref bmpDataC);
            if (grid)
            {
                DrawHorizontal(ref bmpDataC, Colors.CornflowerBlue, slicePoints[nT], 256, volume.Length);
                DrawVertical(ref bmpDataC, Colors.LightGreen, nS, 256, volume.Length);
            }
            bmpDataC.Dispose();
        }

        // Code to add horizontal and vertical lines
        public void DrawVertical(ref BitmapData bmpData, Color c, int x, int width, int height)
        {
            if (x > 0 && x < width)
            {
                for (int y = 0; y < height; y++)
                {
                    bmpData.SetPixel(x, y, c);
                }
            }
        }
        public void DrawHorizontal(ref BitmapData bmpData, Color c, int y, int width, int height)
        {
            if (y > 0 && y < height)
            {
                for (int x = 0; x < width; x++)
                {
                    bmpData.SetPixel(x, y, c);
                }
            }
        }

        // Main volume reader
        public void SetData(Color[][][] data, int plane, int layer, ref BitmapData bmpData)
        {
            // Determine plane for reading
            switch (plane)
            {
                case 0:
                    for (int x = 0; x < data[layer].Length; x++)
                    {
                        for (int y = 0; y < data[layer][x].Length; y++)
                        {
                            bmpData.SetPixel(x, y, data[data.Length - layer - 1][x][y]);
                        }
                    }
                    break;
                case 1:
                    for (int z = 0; z < data.Length; z++)
                    {
                        for (int y = 0; y < data[z][layer].Length; y++)
                        {
                            bmpData.SetPixel(y, data.Length - z - 1, data[z][layer][y]);
                        }
                    }
                    break;
                case 2:
                    for (int z = 0; z < data.Length; z++)
                    {
                        for (int x = 0; x < data[z].Length; x++)
                        {
                            bmpData.SetPixel(x, data.Length - z - 1, data[z][x][layer]);
                        }
                    }
                    break;
            }


            // Old code for recursing through entire volume at once
            // Very slow
            /* 
            for (int z = 0; z < data.Length; z++)
            {
                for (int x = 0; x < data[z].Length; x++)
                {
                    for (int y = 0; y < data[z][x].Length; y++)
                    {
                        switch (plane)
                        {
                            case 0:
                                if (z == layer)
                                    bmpData.SetPixel(x, y, data[z][x][y]);
                                break;
                            case 1:
                                if (x == layer)
                                    bmpData.SetPixel(y, data.Length - z - 1, data[z][x][y]);
                                break;
                            case 2:
                                if (y == layer)
                                    bmpData.SetPixel(x, data.Length - z - 1, data[z][x][y]);
                                break;
                        }
                    }
                }
            }*/
        }
    }
}