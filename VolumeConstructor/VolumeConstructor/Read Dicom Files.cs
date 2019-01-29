using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using Dicom;
using Dicom.Imaging;

namespace VolumeConstructor
{
    
    public partial class MainForm
    {
        /// <summary>
        /// Read through a group of single Dicom Files
        /// Load as Slice-Class for processing/accessability
        /// </summary>
        /// 

        List<Slice> slices;
        // PET Imagine slices I worked with were 256 by 256
        // TODO: detect different sizes
        public static int gridSize = 256; 

        public void LoadDicomFiles(int nFiles)
        {
            slices = new List<Slice>();
            slicePoints = new int[nFiles];

            // Load sample transverse slices
            for (int i = 0; i < nFiles; i++)
            {
                string fileNum = i.ToString();

                // Account for files 0-9 missing leading '0'
                if (i < 10)
                {
                    fileNum = "0" + fileNum;
                }

                // Account for files 0-99 missing leading '0'
                if (i < 100)
                {
                    fileNum = "0" + fileNum;
                }

                slices.Add(new Slice(DicomFile.Open(@".\DICOM\image-000" + fileNum + ".dcm")));
            }

        }
    }
}