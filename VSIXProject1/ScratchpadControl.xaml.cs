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

        private const int currentPositionComparionLength = 10;

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
                var comparisonOffset = 0;
                var currentPositionString = string.Empty;

                var currentCursorPosition = ScratchPadContent.SelectionStart;
                var textLengthGreaterThanSampleSize = ScratchPadContent.Text.Length > currentPositionComparionLength;

                if (textLengthGreaterThanSampleSize)
                {
                    comparisonOffset = currentCursorPosition >= currentPositionComparionLength
                    ? currentPositionComparionLength
                    : 0;

                    currentPositionString = ScratchPadContent.Text.Substring(currentCursorPosition - comparisonOffset, currentPositionComparionLength);
                }
                else
                {
                    comparisonOffset = currentCursorPosition;
                    currentPositionString = ScratchPadContent.Text;
                }

                ScratchPadContent.Text = ((ContentChangedEventArgs)e).NewContent;

                var currentPositionFoundIndex = ScratchPadContent.Text.IndexOf(currentPositionString);

                if (currentPositionFoundIndex == -1)
                    ScratchPadContent.SelectionStart = currentCursorPosition;
                else
                    ScratchPadContent.SelectionStart = currentPositionFoundIndex + comparisonOffset;

            });
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ScratchPadContent.Text = String.Empty;
        }
    }
}