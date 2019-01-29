using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace VolumeConstructor
{
    /// <summary>
    /// Code to change slice being viewed (and load content)
    /// </summary>
    /// 

    public partial class MainForm
    {
        /// Loading Images (with Grid Lines)
        public void LoadContent()
        {
            // Need to update each slice to move Horizontal/Vertical indicator lines
            LoadTransverse(slicePoints[nT], gridLines);
            LoadSagittal(nS, gridLines);
            LoadCoronal(nC, gridLines);
            UpdateLayout();
        }
        
        /// --- Transverse Commands --- 
        int nT = 40;
        int[] slicePoints;
        // Cycle through images forward and backwards
        public void ChangeT(int x)
        {
            nT += x;
            if (nT >= slicePoints.Length)
            {
                nT = 0;
            }

            if (nT < 0)
            {
                nT = slicePoints.Length - 1;
            }
            LoadContent();
        }


        /// --- Sagittal Commands ---
        int nS = 128;
        // Cycle through images forward and backwards
        public void ChangeS(int x)
        {
            nS += x;
            if (nS >= gridSize)
            {
                nS = 0;
            }

            if (nS < 0)
            {
                nS = gridSize - 1;
            }
            LoadContent();
        }


        /// --- Coronal Commands --- 
        int nC = 128;
        // Cycle through images forward and backwards
        public void ChangeC(int x)
        {
            nC += x;
            if (nC >= gridSize)
            {
                nC = 0;
            }

            if (nC < 0)
            {
                nC = gridSize - 1;
            }
            LoadContent();
        }
    }
}