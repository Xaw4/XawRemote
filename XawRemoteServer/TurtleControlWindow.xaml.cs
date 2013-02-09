using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XawRemoteServer
{
    /// <summary>
    /// Interaction logic for TurtleControlWindow.xaml
    /// </summary>
    public partial class TurtleControlWindow : Window
    {

        public TurtleControlWindow()
        {
            InitializeComponent();
        }

        public void TurtleCommand_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String command = "turtle.";

            switch (button.Name)
            {
                case "buttonUp":
                    command += "up()";
                    break;
                case "buttonForward":
                    command += "forward()";
                    break;
                case "buttonDown":
                    command += "down()";
                    break;
                case "buttonLeft":
                    command += "turnLeft()";
                    break;
                case "buttonBackward":
                    command += "back()";
                    break;
                case "buttonRight":
                    command += "turnRight()";
                    break;
            }

            MainWindow parent = this.Owner as MainWindow;
            parent.sendCommandToSelectedComputer(command);

        }
    }
}
