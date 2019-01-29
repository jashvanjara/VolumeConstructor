using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using Dicom;

namespace VolumeConstructor
{
    public partial class MainForm
    {
        /// <summary>
        /// Layout for the Form
        /// TODO: Store images in Panel and use mouse data!!!
        /// </summary>
        /// <param name="layout">Layout Variable (reference)</param>
        /// 

        public void SidebarLayout(ref DynamicLayout layout)
        {
            // General Info
            layout.BeginVertical(null, null, false, false);
            layout.Add(new Label { Text = "Patient Name: " + RetrieveTag<string>(DicomTag.PatientName, 0), TextColor = Colors.Green });

            // Patient Info
            layout.AddRow(new Label { Text = "" });
            layout.AddRow(new Label { Text = "Patient Information" , TextColor = Colors.Red });
            layout.Add(new Label { Text = "Gender: " + RetrieveTag<string>(DicomTag.PatientSex, 0) });
            layout.Add(new Label { Text = "Age: " + RetrieveTag<string>(DicomTag.PatientAge, 0) });
            layout.Add(new Label { Text = "Weight: " + RetrieveTag<string>(DicomTag.PatientWeight, 0) });

            // Image Info
            layout.AddRow(new Label { Text = "" });
            layout.AddRow(new Label { Text = "Image Information" , TextColor = Colors.Red});
            layout.Add(new Label { Text = "Modality: " + RetrieveTag<string>(nT, DicomTag.Modality, 0) });
            //layout.Add(new Label { Text = "Description: " + RetrieveTag<string>(nT, DicomTag.SeriesDescription, 0) });
            layout.Add(new Label { Text = "Date: " + Date(RetrieveTag<string>(nT, DicomTag.AcquisitionDate, 0)) });
            layout.Add(new Label { Text = "Time: " + Time(RetrieveTag<string>(nT, DicomTag.AcquisitionTime, 0)) });
            layout.Add(new Label { Text = "Slice Thickness: " + slices[nT].sliceThickness });
            layout.Add(new Label { Text = "Slice Position: " + slices[nT].sliceLocation + "mm"});

            // Buttons
            layout.AddRow(new Label { Text = "" });
            layout.BeginHorizontal();
            layout.AddAutoSized(new Button { Text = "<", Command = prevT });
            layout.AddAutoSized(new Button { Text = ">", Command = nextT });

            // End of UI
            layout.EndHorizontal();
            layout.AddRow(null);
            layout.EndVertical();
            layout.EndVertical();
        }
        public void ImagesLayout(ref DynamicLayout layout)
        {
            // Transverse
            layout.BeginVertical(2, null, true, true);
            layout.Add(new ImageView { Image = bmpT }, true, true);
            layout.EndVertical();

            /// Other Views
            layout.BeginVertical(2, null, true);

            // Sagittal
            layout.BeginVertical(2, null, true, true);
            layout.Add(new ImageView { Image = bmpS }, true);
            layout.EndVertical();

            // Coronal
            layout.BeginVertical(2, null, true, true);
            layout.Add(new ImageView { Image = bmpC }, true);
            layout.EndVertical();

            // End Other Views
            layout.EndVertical();
        }
        public void OtherSidebarLayout(ref DynamicLayout layout)
        {
            // Other Sidebar
            layout.BeginVertical(null, null, false, true);

            // Sagittal
            layout.BeginVertical(null, null, false, true);
            layout.Add(new Label { Text = "Slice Thickness: " + RetrieveTag<decimal>(DicomTag.PixelSpacing, 0) });
            layout.Add(new Label { Text = "Slice Position: " + RetrieveTag<decimal>(DicomTag.PixelSpacing, 0) * nS });

            // Buttons
            layout.AddRow(new Label { Text = "" });
            layout.BeginHorizontal();
            layout.AddAutoSized(new Button { Text = "<", Command = prevS });
            layout.AddAutoSized(new Button { Text = ">", Command = nextS });
            layout.EndHorizontal();
            layout.AddRow(null);

            layout.EndVertical();

            // Coronal
            layout.BeginVertical(null, null, false, true);
            layout.Add(new Label { Text = "Slice Thickness: " + RetrieveTag<decimal>(DicomTag.PixelSpacing, 0) });
            layout.Add(new Label { Text = "Slice Position: " + RetrieveTag<decimal>(DicomTag.PixelSpacing, 0) * nC });

            // Buttons
            layout.AddRow(new Label { Text = "" });
            layout.BeginHorizontal();
            layout.AddAutoSized(new Button { Text = "<", Command = prevC });
            layout.AddAutoSized(new Button { Text = ">", Command = nextC });
            layout.EndHorizontal();
            layout.AddRow(null);

            layout.EndVertical();

            // End Other Sidebar
            layout.EndVertical();
        }

        // Functions to Retrieve Tags
        public type RetrieveTag<type>(DicomTag tag, int index)
        {
            // Retrieves Dicom Attribute from first DICOM image held
            return slices[0].data.GetValue<type>(tag, index);
        }
        public type RetrieveTag<type>(int n, DicomTag tag, int index)
        {
            // Retrieves Dicom Attribute from n'th DICOM image held
            return slices[n].data.GetValue<type>(tag, index);
        }
        
        // Functions to parse Date
        public string Date(string DicomDateTime)
        {
            string d = DicomDateTime;
            return d.Substring(6, 2) + "/"
                + d.Substring(4, 2) + "/"
                + d.Substring(0, 4);
        }
        public string Time(string DicomDateTime)
        {
            string d = DicomDateTime;
            return d.Substring(0, 2) + ":"
                + d.Substring(2, 2) + ":"
                + d.Substring(4, 2);
        }
    }
}