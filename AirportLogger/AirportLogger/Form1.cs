using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


//////////////////////////////////////////////////////////////////////////////////////////
//                                                                                      //
//           Code based on :                                                            //
//           "C# for Programmers Second Edition", Deitel & Deitel .                     //
//           [Chapter 23, pages 1033-1037]                                              //
//                                                                                      //
//////////////////////////////////////////////////////////////////////////////////////////

namespace AirportLogger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NetworkStream output;
        private BinaryReader reader;
        private Thread readThread;
        private string planeReport;
        private string message = " ";
        private int castIntVariable;
        private int planeTakeoffCount = 0;
        private bool startUp;

        private void AirportLoggerForm_Load(object sender, EventArgs e)
        {
            readThread = new Thread(new ThreadStart(RunClient));
            readThread.Start();
        }

        private void AirportLoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private delegate void DisplayDelegate(string message);

        private delegate void TakeoffDisplayDelegate(int planeTakeoffCount);

        private void DisplayMessage(string message)
        {
            if (mainSummaryTextBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayMessage), new object[] { message });
            }
            else //mainSummaryTextBox.Text += message;
                mainSummaryTextBox.AppendText(message);
        }

        private void DisplayBlueStatus(string message)
        {
            if (bluePlaneStatusBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayBlueStatus), new object[] { message });
            }
            else 
                bluePlaneStatusBox.Text = message;
        }

        private void DisplayBlueObjective(string message)
        {
            if (bluePlaneObjBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayBlueObjective), new object[] { message });
            }
            else
                bluePlaneObjBox.Text = message;
        }

        private void DisplayRedStatus(string message)
        {
            if (redPlaneStatusBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayRedStatus), new object[] { message });
            }
            else 
                redPlaneStatusBox.Text = message;
        }

        private void DisplayRedObjective(string message)
        {
            if (redPlaneObjBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayRedObjective), new object[] { message });
            }
            else
                redPlaneObjBox.Text = message;
        }

        private void DisplayGreenStatus(string message)
        {
            if (greenPlaneStatusBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayGreenStatus), new object[] { message });
            }
            else 
                greenPlaneStatusBox.Text = message;
        }

        private void DisplayGreenObjective(string message)
        {
            if (greenPlaneObjBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayGreenObjective), new object[] { message });
            }
            else
                greenPlaneObjBox.Text = message;
        }

        private void DisplayYellowStatus(string message)
        {
            if (yellowPlaneStatusBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayYellowStatus), new object[] { message });
            }
            else 
                yellowPlaneStatusBox.Text = message;
        }

        private void DisplayYellowObjective(string message)
        {
            if (yellowPlaneObjBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(DisplayYellowObjective), new object[] { message });
            }
            else
                yellowPlaneObjBox.Text = message;
        }

        private void DisplayTakeoffCount(int planeTakeoffCount)
        {
            if (planesLeftIntegerBox.InvokeRequired)
            {
                Invoke(new TakeoffDisplayDelegate(DisplayTakeoffCount), new object[] { planeTakeoffCount });
            }
            else           
                planesLeftIntegerBox.Text = planeTakeoffCount.ToString();
                       
        }

        private delegate void DisableInputDelegate(bool value);

        private void SortPlaneReport(string report)
        {
            string planeStatuses = report.Substring(0, 10);
            // When finding the plane objectives, the index of the letter relative to the plane within the substring "planeObjectives" needs to be found
            string planeObjectives = report.Substring(10);

            // Blue Plane
            DisplayBlueStatus(PlaneStatus(planeStatuses.IndexOf("B")));
            DisplayBlueObjective(PlaneObjective(planeObjectives.Substring(1,1)));

            // Red Plane
            DisplayRedStatus(PlaneStatus(planeStatuses.IndexOf("R")));
            DisplayRedObjective(PlaneObjective(planeObjectives.Substring(3,1)));
            

            // Yellow Plane
            DisplayYellowStatus(PlaneStatus(planeStatuses.IndexOf("Y")));
            DisplayYellowObjective(PlaneObjective(planeObjectives.Substring(5,1)));

            // Green Plane
            DisplayGreenStatus(PlaneStatus(planeStatuses.IndexOf("G")));
            DisplayGreenObjective(PlaneObjective(planeObjectives.Substring(7,1)));
        }

        private string PlaneStatus(int index)
        {
            if (index == 0)
                return "Moving through Runway A";

            if (index == 1)
                return "Moving through Runway B";

            if (index == 2)
                return "Moving through Runway C";

            if (index == 3)
                return "Moving through Runway D";

            if (index == 4)
                return "Moving through Runway E";

            if (index == 5)
                return "On the Takeoff Runway";

            if (index == 6)
                return "On the Arrival Hub";

            if (index == 7)
                return "Docked at Port1";

            if (index == 8)
                return "Docked at Port2";

            if (index == 9)
                return "Docked at Port3";

            return "Taken Off";
        }

        private string PlaneObjective(string message)
        {
            if (message == "1")
                return "No Current Objectives";

            if (message == "2")
                return "Take-off";

            if (message == "3")
                return "Reach Destination";

            if (message == "4")
                return "Land at Airport";

            if (message == "5")
                return "Dock at Port 1";

            if (message == "6")
                return "Dock at Port 2";

            if (message == "7")
                return "Dock at Port 3";

            if (message == "8")
                return "Awaiting Objective";

            return "ERROR";
        }

        public void RunClient()
        {

            TcpClient client;

            try
            {
                DisplayMessage("Attempting to connect\r\n");
                client = new TcpClient();
                client.Connect("127.0.0.1", 5000);
                output = client.GetStream();
                reader = new BinaryReader(output);
                DisplayMessage("\r\nGot IO stream\r\n");

                do
                {
                    try
                    {                       
                        message = reader.ReadString();

                        if(startUp)
                        {
                            planeReport = message.Substring(0, 18);
                            SortPlaneReport(planeReport);
                            if(message.Length != 18)
                            {
                                planeTakeoffCount = Int32.Parse(message.Substring(18, 2));
                                message = message.Substring(20);
                                DisplayMessage(message + "\r\n");
                            }
                        }
                        else
                        {
                            startUp = true;
                            DisplayMessage(message + "\r\n");
                        }                         
                        
                        DisplayTakeoffCount(planeTakeoffCount);
                    }
                    catch (Exception)
                    {
                        System.Environment.Exit(System.Environment.ExitCode);
                    }
                }
                while (message != "SERVER>>> TERMINATE");

                reader.Close();
                output.Close();
                client.Close();
                Application.Exit();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                System.Environment.Exit(System.Environment.ExitCode);
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void blueLabel_Click(object sender, EventArgs e)
        {

        }
        private void redLabel_Click(object sender, EventArgs e)
        {

        }

        private void yellowLabel_Click(object sender, EventArgs e)
        {

        }
        private void greenLabel_Click(object sender, EventArgs e)
        {

        }





    }
}