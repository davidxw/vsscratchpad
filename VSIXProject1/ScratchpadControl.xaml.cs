//------------------------------------------------------------------------------
// <copyright file="ScratchpadControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VSIXProject1
{
    using global::Scratchpad;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;



    /// <summary>
    /// Interaction logic for ScratchpadControl.
    /// </summary>
    public partial class ScratchpadControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScratchpadControl"/> class.
        /// </summary>
        public ScratchpadControl()
        {
            this.InitializeComponent();
            ScratchPadContent.Text = FileContentManager.Instance.Load();

            FileContentManager.Instance.ContentChanged += FileContentManager_ContentChanged;

            Timer writeTimer = new Timer(Constants.SaveIntervalInMilliseconds);
            writeTimer.Elapsed += WriteTimer_Elapsed;
            writeTimer.Start();
        }

        private void WriteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string text = null;

            this.Dispatcher.Invoke(() =>
            {
                text = ScratchPadContent.Text;
            });

            FileContentManager.Instance.Save(text);
        }

        private void FileContentManager_ContentChanged(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                ScratchPadContent.Text = ((ContentChangedEventArgs)e).NewContent;
            });
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ScratchPadContent.Text = String.Empty;
        }
    }
}