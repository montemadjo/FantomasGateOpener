﻿using System.Windows;

namespace FantomasGateOpener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("kazablanka");
        }
    }
}
