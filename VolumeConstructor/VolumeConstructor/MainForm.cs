using System;
using Eto.Forms;
using Eto.Drawing;

namespace VolumeConstructor
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
            /// <summary>
            /// Main Functions in Order
            /// </summary>
            /// 

            // Load
            LoadDicomFiles(47);

            // Process
            ConfigureVolume();

            // Ready for use
            GenerateVolume();
            Console.WriteLine("Volume ready for use");

            // Load UI
            ConstructForm();

        }
	}
}
