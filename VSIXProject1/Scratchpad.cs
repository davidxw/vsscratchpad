//------------------------------------------------------------------------------
// <copyright file="Scratchpad.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VSIXProject1
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Timers;
    using Microsoft.VisualStudio.Shell;
    using System.Diagnostics;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("ddbe1f11-da6b-45a5-8598-037c5ba13893")]
    public class Scratchpad : ToolWindowPane
    {
        private string fileSavePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scratchpad"/> class.
        /// </summary>
        public Scratchpad() : base(null)
        {
            fileSavePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "vs_scratchpad.txt");

            this.Caption = "Scratchpad";

            this.Content = new ScratchpadControl();
        }

        protected override void OnCreate()
        {
            try
            {
                ((ScratchpadControl)this.Content).ScratchPadContent.Text = System.IO.File.ReadAllText(fileSavePath);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        protected override void OnClose()
        {
            try
            {
                System.IO.File.WriteAllText(fileSavePath, ((ScratchpadControl)this.Content).ScratchPadContent.Text);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        
    }
}
