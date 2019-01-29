using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace VolumeConstructor
{
    /// <summary>
    /// Generates form (uses Layout Code)
    /// </summary>
    /// 

    public partial class MainForm
    {
        // Commands
        Command quitCommand;
        Command nextT, prevT, nextS, prevS, nextC, prevC;
        bool gridLines;

        public void ConstructForm()
        {
            Title = "Volume Viewer";
            ClientSize = new Size(1450, 500);

            // Create Controls
			CreateCommands();

			// Create menu
			Menu = new MenuBar
			{
                Items = {
                    new ButtonMenuItem
                    {
                        Text = "Transverse",
                        Items =
                        {
					        // File submenu
					        prevT, nextT
                        }
                    },
                    new ButtonMenuItem
                    {
                        Text = "Sagittal",
                        Items =
                        {
					        // File submenu
					        prevS, nextS
                        }
                    },
                    new ButtonMenuItem
                    {
                        Text = "Coronal",
                        Items =
                        {
					        // File submenu
					        prevC, nextC
                        }
                    }
                },
                QuitItem = quitCommand

			};

            gridLines = true;
            LoadContent();
        }

        
        public void UpdateLayout()
        {
            var layout = new DynamicLayout();
			
			/// Main Layout
			layout.BeginHorizontal();

            // Functions for loading layout
            SidebarLayout(ref layout);
            ImagesLayout(ref layout);
            OtherSidebarLayout(ref layout);

			layout.EndHorizontal();

			// Save
            Content = layout;
        }

        public void CreateCommands()
        {
            // Close App
            quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();


            /// Transvrse Slices
            // Next Slice
            nextT = new Command { MenuText = " Next >> ", Shortcut = Keys.E };
            nextT.Executed += (sender, e) => ChangeT(+1);

            // Previous Slice
            prevT = new Command { MenuText = " << Prev ", Shortcut = Keys.Q };
            prevT.Executed += (sender, e) => ChangeT(-1);


            /// Sagittal Slices
            // Next Slice
            nextS = new Command { MenuText = " Next >> ", Shortcut = Keys.D };
            nextS.Executed += (sender, e) => ChangeS(+1);

            // Previous Slice
            prevS = new Command { MenuText = " << Prev ", Shortcut = Keys.A };
            prevS.Executed += (sender, e) => ChangeS(-1);


            /// Coronal Slices
            // Next Slice
            nextC = new Command { MenuText = " Next >> ", Shortcut = Keys.S };
            nextC.Executed += (sender, e) => ChangeC(+1);

            // Previous Slice
            prevC = new Command { MenuText = " << Prev ", Shortcut = Keys.W };
            prevC.Executed += (sender, e) => ChangeC(-1);

        }
    }
}