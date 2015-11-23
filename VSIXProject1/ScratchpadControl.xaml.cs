//------------------------------------------------------------------------------
// <copyright file="ScratchpadControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VSIXProject1
{
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
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ScratchPadContent.Text = String.Empty;
        }
    }
}