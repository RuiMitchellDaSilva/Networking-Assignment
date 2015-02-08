using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//
//  Bermuda Triangle : Version 16
//
public class Form1 : Form
{

    private Container components = null;

    // All ports within the airport.
    private ButtonPanelThread port1, port2, port3; 
    // port reletive buttons, except for the last button (btn4) with belongs to ArrivalHubPanelThread arrivalHub.
    private Button btn1, btn2, btn3, btn4;
    // Group box with all RadioButtons contained.
    private GroupBox groupBox;
    // RadioButtons for each port panel.
    private RadioButton radiobtn0, radiobtn1, radiobtn2, radiobtn3;

    private WaitPanelThread runwayA, runwayB, runwayC, runwayD, runwayE;
    private TakeOffPanelThread takeOffRunway;
    private ArrivalHubPanelThread arrivalHub;

    // All ButtonPanelThread, WaitPanelThread, TakeOffPanelThread and ArrivalHubPanelThread threads.
    private Thread portThread1, portThread2, portThread3, runwayNThreadA, runwayNThreadB, runwayNThreadC, runwayNThreadD, arrivalHubThread, takeOffRunwayThread, runwayNThreadE;
    private Semaphore semaphorePort1, semaphorePort2, semaphorePort3, semaphoreA, semaphoreB, semaphoreC, semaphoreD, semaphoreE, semaphoreF;
    private Buffer bufferPort1, bufferPort2, bufferPort3, bufferA, bufferB, bufferC, bufferD, bufferE, bufferF;
    // All panels belonging to ButtonPanelThread, WaitPanelThread, TakeOffPanelThread and ArrivalHubPanelThread
    private Panel portPnl1, portPnl2, portPnl3, runwayNPnlA, runwayNPnlB, runwayNPnlC, runwayNPnlD, arrivalHubPanel, runwayNPnlE, takeOffRunwayPanel;

    private AirportEventLogger airportLogger;
    private PlaneStatusChecker planeStatusChecker;
    private Panel panel1;
    private Panel panel2;
    private Thread airportLoggerThread;

    public Form1()
    {
        InitializeComponent();

        // Each panel is connected to adjecent panels via semaphore/buffer
        // Since the planes travel clockwise, buffers are also connected clockwise (from A-F)

        // Each port also needs to know which runway it is connected to, in order to begin docking procedures if vacant

        semaphorePort1 = new Semaphore();
        semaphorePort2 = new Semaphore();
        semaphorePort3 = new Semaphore();

        semaphoreA = new Semaphore();
        semaphoreB = new Semaphore();
        semaphoreC = new Semaphore();
        semaphoreD = new Semaphore();
        semaphoreE = new Semaphore();
        semaphoreF = new Semaphore();

        bufferPort1 = new Buffer();
        bufferPort2 = new Buffer();
        bufferPort3 = new Buffer();

        bufferA = new Buffer();
        bufferB = new Buffer();
        bufferC = new Buffer();
        bufferD = new Buffer();
        bufferE = new Buffer();
        bufferF = new Buffer();

        port1 = new ButtonPanelThread(new Point(10, 40),
                             120, false, runwayA, portPnl1,
                             Color.Blue,
                             semaphoreA,
                             semaphorePort1,
                             bufferPort1,
                             bufferA,
                             btn1,
                             radiobtn1);

        port2 = new ButtonPanelThread(new Point(10, 40),
                             120, false, runwayB, portPnl2,
                             Color.Red,
                             semaphoreB,
                             semaphorePort2,
                             bufferPort2,
                             bufferB,
                             btn2,
                             radiobtn2);

        port3 = new ButtonPanelThread(new Point(10, 40),
                             120, false, runwayC, portPnl3,
                             Color.Yellow,
                             semaphoreC,
                             semaphorePort3,
                             bufferPort3,
                             bufferC,
                             btn3,
                             radiobtn3);


        runwayA = new WaitPanelThread(new Point(10, 10),
                     100, true, true, runwayNPnlA, port1,
                     Color.Black,
                     semaphoreA,
                     semaphoreB,
                     bufferA, 
                     bufferB,
                     bufferPort1,
                     semaphorePort1);

        runwayB = new WaitPanelThread(new Point(10, 10),
                     100, true, true, runwayNPnlB, port2,
                     Color.Black,
                     semaphoreB,
                     semaphoreC,
                     bufferB, 
                     bufferC,
                     bufferPort2,
                     semaphorePort2);

        runwayC = new WaitPanelThread(new Point(10, 10),
                     100, false, true, runwayNPnlC, port3,
                     Color.Black,
                     semaphoreC,
                     semaphoreD,
                     bufferC,
                     bufferD,
                     bufferPort3,
                     semaphorePort3);

        runwayD = new WaitPanelThread(new Point(200, 10),
                     100, true, false, runwayNPnlD, null,
                     Color.Black,
                     semaphoreD,
                     semaphoreE,
                     bufferD,
                     bufferE,
                     null,
                     null);

        arrivalHub = new ArrivalHubPanelThread(new Point(250, 10),
                             120, true, arrivalHubPanel,
                             Color.Green,
                             semaphoreD,
                             bufferD,
                             btn4);


        takeOffRunway = new TakeOffPanelThread(new Point(390, 10),
                     100, true, false, takeOffRunwayPanel,
                     Color.Black,
                     semaphoreE,
                     semaphoreF,
                     bufferE,
                     bufferF,
                     radiobtn0);

        runwayE = new WaitPanelThread(new Point(10, 195),
                     100, false, false, runwayNPnlE, null,
                     Color.Black,
                     semaphoreF,
                     semaphoreA,
                     bufferF,
                     bufferA,
                     null,
                     null);

        planeStatusChecker = new PlaneStatusChecker(runwayA, runwayB, runwayC, runwayD, runwayE,
                         takeOffRunway,
                         arrivalHub,
                         port1, port2, port3);

        airportLogger = new AirportEventLogger(takeOffRunway, planeStatusChecker);

        portThread1 = new Thread(new ThreadStart(port1.Start));       
        portThread2 = new Thread(new ThreadStart(port2.Start));
        portThread3 = new Thread(new ThreadStart(port3.Start));

        runwayNThreadA = new Thread(new ThreadStart(runwayA.Start));
        runwayNThreadB = new Thread(new ThreadStart(runwayB.Start));
        runwayNThreadC = new Thread(new ThreadStart(runwayC.Start));
        runwayNThreadD = new Thread(new ThreadStart(runwayD.Start));

        arrivalHubThread = new Thread(new ThreadStart(arrivalHub.Start));

        takeOffRunwayThread = new Thread(new ThreadStart(takeOffRunway.Start));

        runwayNThreadE = new Thread(new ThreadStart(runwayE.Start));

        airportLoggerThread = new Thread(new ThreadStart(airportLogger.Start));


        this.Closing += new CancelEventHandler(this.Form1_Closing);

        portThread1.Start();
        portThread2.Start();
        portThread3.Start();

        runwayNThreadA.Start();
        runwayNThreadB.Start();
        runwayNThreadC.Start();
        runwayNThreadD.Start();

        arrivalHubThread.Start();

        takeOffRunwayThread.Start();

        runwayNThreadE.Start();

        airportLoggerThread.Start();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
                components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.radiobtn0 = new System.Windows.Forms.RadioButton();
            this.radiobtn1 = new System.Windows.Forms.RadioButton();
            this.radiobtn2 = new System.Windows.Forms.RadioButton();
            this.radiobtn3 = new System.Windows.Forms.RadioButton();
            this.portPnl1 = new System.Windows.Forms.Panel();
            this.btn1 = new System.Windows.Forms.Button();
            this.portPnl2 = new System.Windows.Forms.Panel();
            this.btn2 = new System.Windows.Forms.Button();
            this.portPnl3 = new System.Windows.Forms.Panel();
            this.btn3 = new System.Windows.Forms.Button();
            this.runwayNPnlA = new System.Windows.Forms.Panel();
            this.runwayNPnlB = new System.Windows.Forms.Panel();
            this.runwayNPnlC = new System.Windows.Forms.Panel();
            this.runwayNPnlD = new System.Windows.Forms.Panel();
            this.arrivalHubPanel = new System.Windows.Forms.Panel();
            this.btn4 = new System.Windows.Forms.Button();
            this.takeOffRunwayPanel = new System.Windows.Forms.Panel();
            this.runwayNPnlE = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox.SuspendLayout();
            this.portPnl1.SuspendLayout();
            this.portPnl2.SuspendLayout();
            this.portPnl3.SuspendLayout();
            this.arrivalHubPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.radiobtn0);
            this.groupBox.Controls.Add(this.radiobtn1);
            this.groupBox.Controls.Add(this.radiobtn2);
            this.groupBox.Controls.Add(this.radiobtn3);
            this.groupBox.Location = new System.Drawing.Point(662, 290);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(125, 125);
            this.groupBox.TabIndex = 10;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Landing Options";
            // 
            // radiobtn0
            // 
            this.radiobtn0.Location = new System.Drawing.Point(31, 20);
            this.radiobtn0.Name = "radiobtn0";
            this.radiobtn0.Size = new System.Drawing.Size(67, 17);
            this.radiobtn0.TabIndex = 0;
            this.radiobtn0.Text = "Take-off";
            // 
            // radiobtn1
            // 
            this.radiobtn1.Location = new System.Drawing.Point(31, 40);
            this.radiobtn1.Name = "radiobtn1";
            this.radiobtn1.Size = new System.Drawing.Size(67, 17);
            this.radiobtn1.TabIndex = 1;
            this.radiobtn1.Text = "Port 1";
            // 
            // radiobtn2
            // 
            this.radiobtn2.Location = new System.Drawing.Point(31, 60);
            this.radiobtn2.Name = "radiobtn2";
            this.radiobtn2.Size = new System.Drawing.Size(67, 17);
            this.radiobtn2.TabIndex = 2;
            this.radiobtn2.Text = "Port 2";
            // 
            // radiobtn3
            // 
            this.radiobtn3.Location = new System.Drawing.Point(31, 80);
            this.radiobtn3.Name = "radiobtn3";
            this.radiobtn3.Size = new System.Drawing.Size(67, 17);
            this.radiobtn3.TabIndex = 3;
            this.radiobtn3.Text = "Port 3";
            // 
            // portPnl1
            // 
            this.portPnl1.BackColor = System.Drawing.Color.Gray;
            this.portPnl1.Controls.Add(this.btn1);
            this.portPnl1.Location = new System.Drawing.Point(162, 10);
            this.portPnl1.Name = "portPnl1";
            this.portPnl1.Size = new System.Drawing.Size(30, 220);
            this.portPnl1.TabIndex = 0;
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.Pink;
            this.btn1.Location = new System.Drawing.Point(0, 0);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(30, 30);
            this.btn1.TabIndex = 0;
            this.btn1.UseVisualStyleBackColor = false;
            // 
            // portPnl2
            // 
            this.portPnl2.BackColor = System.Drawing.Color.Gray;
            this.portPnl2.Controls.Add(this.btn2);
            this.portPnl2.Location = new System.Drawing.Point(382, 10);
            this.portPnl2.Name = "portPnl2";
            this.portPnl2.Size = new System.Drawing.Size(30, 220);
            this.portPnl2.TabIndex = 1;
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.Pink;
            this.btn2.Location = new System.Drawing.Point(0, 0);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(30, 30);
            this.btn2.TabIndex = 0;
            this.btn2.UseVisualStyleBackColor = false;
            // 
            // portPnl3
            // 
            this.portPnl3.BackColor = System.Drawing.Color.Gray;
            this.portPnl3.Controls.Add(this.btn3);
            this.portPnl3.Location = new System.Drawing.Point(602, 10);
            this.portPnl3.Name = "portPnl3";
            this.portPnl3.Size = new System.Drawing.Size(30, 220);
            this.portPnl3.TabIndex = 2;
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.Color.Pink;
            this.btn3.Location = new System.Drawing.Point(0, 0);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(30, 30);
            this.btn3.TabIndex = 0;
            this.btn3.UseVisualStyleBackColor = false;
            // 
            // runwayNPnlA
            // 
            this.runwayNPnlA.BackColor = System.Drawing.Color.Black;
            this.runwayNPnlA.Location = new System.Drawing.Point(162, 230);
            this.runwayNPnlA.Name = "runwayNPnlA";
            this.runwayNPnlA.Size = new System.Drawing.Size(220, 30);
            this.runwayNPnlA.TabIndex = 3;
            // 
            // runwayNPnlB
            // 
            this.runwayNPnlB.BackColor = System.Drawing.Color.Black;
            this.runwayNPnlB.Location = new System.Drawing.Point(382, 230);
            this.runwayNPnlB.Name = "runwayNPnlB";
            this.runwayNPnlB.Size = new System.Drawing.Size(220, 30);
            this.runwayNPnlB.TabIndex = 4;
            // 
            // runwayNPnlC
            // 
            this.runwayNPnlC.BackColor = System.Drawing.Color.Black;
            this.runwayNPnlC.Location = new System.Drawing.Point(602, 230);
            this.runwayNPnlC.Name = "runwayNPnlC";
            this.runwayNPnlC.Size = new System.Drawing.Size(30, 220);
            this.runwayNPnlC.TabIndex = 5;
            // 
            // runwayNPnlD
            // 
            this.runwayNPnlD.BackColor = System.Drawing.Color.Black;
            this.runwayNPnlD.Location = new System.Drawing.Point(412, 450);
            this.runwayNPnlD.Name = "runwayNPnlD";
            this.runwayNPnlD.Size = new System.Drawing.Size(220, 30);
            this.runwayNPnlD.TabIndex = 6;
            // 
            // arrivalHubPanel
            // 
            this.arrivalHubPanel.BackColor = System.Drawing.Color.Black;
            this.arrivalHubPanel.Controls.Add(this.btn4);
            this.arrivalHubPanel.Location = new System.Drawing.Point(632, 450);
            this.arrivalHubPanel.Name = "arrivalHubPanel";
            this.arrivalHubPanel.Size = new System.Drawing.Size(220, 30);
            this.arrivalHubPanel.TabIndex = 7;
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.Color.Pink;
            this.btn4.Location = new System.Drawing.Point(190, 0);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(30, 30);
            this.btn4.TabIndex = 0;
            this.btn4.UseVisualStyleBackColor = false;
            // 
            // takeOffRunwayPanel
            // 
            this.takeOffRunwayPanel.BackColor = System.Drawing.Color.Black;
            this.takeOffRunwayPanel.Location = new System.Drawing.Point(12, 450);
            this.takeOffRunwayPanel.Name = "takeOffRunwayPanel";
            this.takeOffRunwayPanel.Size = new System.Drawing.Size(400, 30);
            this.takeOffRunwayPanel.TabIndex = 8;
            // 
            // runwayNPnlE
            // 
            this.runwayNPnlE.BackColor = System.Drawing.Color.Black;
            this.runwayNPnlE.Location = new System.Drawing.Point(162, 260);
            this.runwayNPnlE.Name = "runwayNPnlE";
            this.runwayNPnlE.Size = new System.Drawing.Size(30, 200);
            this.runwayNPnlE.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.BackgroundImage = global::Assignment.Properties.Resources.BluePlane;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(759, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 69);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.BackgroundImage = global::Assignment.Properties.Resources.NewGrass2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(867, 501);
            this.panel2.TabIndex = 12;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(861, 490);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.portPnl1);
            this.Controls.Add(this.portPnl2);
            this.Controls.Add(this.portPnl3);
            this.Controls.Add(this.runwayNPnlA);
            this.Controls.Add(this.runwayNPnlB);
            this.Controls.Add(this.runwayNPnlC);
            this.Controls.Add(this.runwayNPnlD);
            this.Controls.Add(this.takeOffRunwayPanel);
            this.Controls.Add(this.runwayNPnlE);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.arrivalHubPanel);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Airplane Terminal Simulator";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.groupBox.ResumeLayout(false);
            this.portPnl1.ResumeLayout(false);
            this.portPnl2.ResumeLayout(false);
            this.portPnl3.ResumeLayout(false);
            this.arrivalHubPanel.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    private void Form1_Closing(object sender, CancelEventArgs e)
    {
        // Environment is a System class.
        // Kill off all threads on exit.
        Environment.Exit(Environment.ExitCode);
    }


}// end class form1


public class Buffer
{
    private Color planeColor;
    private bool empty = true;

    public void Read(ref Color planeColor)
    {
        lock (this)
        {
            // Check whether the buffer is empty.
            if (empty)
                Monitor.Wait(this);
            empty = true;
            planeColor = this.planeColor;
            Monitor.Pulse(this);
        }
    }

    public void Write(Color planeColor)
    {
        lock (this)
        {
            // Check whether the buffer is full.
            if (!empty)
                Monitor.Wait(this);
            empty = false;
            this.planeColor = planeColor;
            Monitor.Pulse(this);
        }
    }

    public void Start()
    {
    }

}// end class Buffer

public class Semaphore
{
    private int count = 1;

    public void Wait()
    {
        lock (this)
        {
            while (count == 0)
                Monitor.Wait(this);
            count = 0;
        }
    }

    public void Signal()
    {
        lock (this)
        {
            count = 1;
            Monitor.Pulse(this);
        }
    }

    public void Start()
    {
    }

}// end class Semaphore

public class ButtonPanelThread
{
    private Point origin;
    private int delay;
    private Panel panel;
    private bool northSouth;
    // To determine docking procedures
    private WaitPanelThread adjacentRunway;
    // Check if selected by the RadioButton
    private bool isSelected;
    // Check if the plane is at the entrance of the port (buffer has been read)
    private bool isPlaneReady = false;
    private bool planeDocked = true;
    private Color colour;
    private Point plane;
    private int xDelta;
    private int yDelta;
    private Semaphore panelSemaphore;
    private Semaphore thisSemaphore;
    private Buffer buffer1;
    private Buffer buffer2;
    private Button btn;
    private RadioButton radiobtn;
    // For the panel's button to lock if not pressed
    private bool locked = true;
    // This is so the button will reset once the plane has left this panel
    object senderCopy;
    System.EventArgs eCopy;

    public ButtonPanelThread(Point origin,
                             int delay,
                             bool northSouth,
                             WaitPanelThread adjacentRunway,
                             Panel panel,
                             Color colour,
                             Semaphore panelSemaphore,
                             Semaphore thisSemaphore,
                             Buffer buffer1,
                             Buffer buffer2,
                             Button btn,
                             RadioButton radiobtn)
    {
        this.origin = origin;
        this.delay = delay;
        this.northSouth = northSouth;
        this.adjacentRunway = adjacentRunway;
        this.panel = panel;
        this.colour = colour;
        this.plane = origin;
        this.panel.Paint += new PaintEventHandler(this.panel_Paint);
        this.xDelta = 0;
        this.yDelta = northSouth ? -10 : +10;
        this.panelSemaphore = panelSemaphore;
        this.thisSemaphore = thisSemaphore;
        this.buffer1 = buffer1;
        this.buffer2 = buffer2;
        this.btn = btn;
        this.btn.Click += new System.
                              EventHandler(this.btn_Click);
        this.radiobtn = radiobtn;
        this.radiobtn.CheckedChanged+= new System.
                              EventHandler(this.radioButton_CheckChanged);
    }

    private void btn_Click(object sender,
                           System.EventArgs e)
    {
        senderCopy = sender;
        eCopy = e;
        locked = !locked;
        this.btn.BackColor = locked ? Color.Pink : Color.LightGreen;
        lock (this)
        {
            if (!locked)
                Monitor.Pulse(this);
        }
    }

    private void radioButton_CheckChanged(object sender, System.EventArgs e)
    {
        RadioButton rb = sender as RadioButton;

        // If radioButton relevant to the port is checked, then that means a the port is ready to dock
        if (rb.Checked)
            isSelected = true;
        else
            isSelected = false;
    }

    public void Start()
    {
        Color signal = Color.Red;
        Thread.Sleep(delay);

        // Make sure the panel is always active for planes departing/arriving
        while(true)
        {
            // If there's no plane ported, then check for any planes arriving
            // Else any planes that are docked will depart if the relevant button is pressed
            if (this.colour == Color.Transparent)
            {
                if (thisSemaphore != null)
                    thisSemaphore.Signal();

                buffer1.Read(ref this.colour);

                if (this.colour == Color.Green)
                {
                    for (int i = 1; i <= 17; i++)
                    {
                        this.movePlane(xDelta, -yDelta);
                        Thread.Sleep(delay);
                        panel.Invalidate();
                    }

                    panel.Invalidate();
                    //panel.BackColor = Color.DarkGreen;
                    this.colour = Color.LightGreen;
                    planeDocked = true;
                    isSelected = false;
                }
            }               
            else
            { 
                this.zeroPlane();
                panel.Invalidate();

                lock (this)
                {
                    while (locked)
                    {
                        Monitor.Wait(this);
                    }
                }

                for (int i = 1; i <= 17; i++)
                {
                    this.movePlane(xDelta, yDelta);
                    Thread.Sleep(delay);
                    panel.Invalidate();
                }

                if(panelSemaphore != null)
                    panelSemaphore.Wait();

                buffer2.Write(this.colour);
                this.colour = Color.Transparent;
                planeDocked = false;
                // Reset button
                btn_Click(senderCopy, eCopy);
                panel.Invalidate();
            }
        }     
    }

    private void zeroPlane()
    {
        plane.X = origin.X;
        plane.Y = origin.Y;
    }

    private void movePlane(int xDelta, int yDelta)
    {
        plane.X += xDelta; plane.Y += yDelta;
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        SolidBrush brush = new SolidBrush(colour);
        g.FillRectangle(brush, plane.X, plane.Y, 10, 10);

        brush.Dispose();    //  Dispose graphics resources. 
        g.Dispose();        //  
    }

    public Color GetPlaneColour() { return this.colour; }

    public bool GetIsSelected() { return isSelected; }

    public bool IsThePlaneLocked() { return locked; }

    public void SetIsPlaneReady() { isPlaneReady = !isPlaneReady; }

    public bool IsPlaneDocked() { return planeDocked; }

}// end class ButtonPanelThread

public class WaitPanelThread
{
    private Point origin;
    private int delay;
    private Panel panel;
    private ButtonPanelThread adjacentPort;
    // The Panel goes Up or Down
    private bool verticalHorizontal;
    // Positive or Negative direction to the Panel's relative axis (Up or Down)
    private bool positiveNegative;
    private Color colour;
    private Point plane;
    private int xDelta;
    private int yDelta;
    private Semaphore semaphore1;
    private Semaphore semaphore2;
    private Buffer buffer1;
    private Buffer buffer2;
    private Buffer bufferPort;
    private Semaphore portSemaphore;

    // Runway lights
    private Point[] light;
    private Color lightColour;


    public WaitPanelThread(Point origin,
                       int delay,
                       bool verticalHorizontal,
                       bool positiveNegative,
                       Panel panel,
                       ButtonPanelThread adjacentPort,
                       Color colour,
                       Semaphore semaphore1,
                       Semaphore semaphore2,
                       Buffer buffer1,
                       Buffer buffer2,
                       Buffer bufferPort,
                       Semaphore portSemaphore)
    {
        this.origin = origin;
        this.delay = delay;
        this.positiveNegative = positiveNegative;
        this.panel = panel;
        this.adjacentPort = adjacentPort;
        this.colour = colour;
        this.plane = origin;
        this.panel.Paint += new PaintEventHandler(this.panel_Paint);
        this.verticalHorizontal = verticalHorizontal;
        this.xDelta = positiveNegative ? +10 : -10;
        this.yDelta = positiveNegative ? +10 : -10;
        this.semaphore1 = semaphore1;
        this.semaphore2 = semaphore2;
        this.buffer1 = buffer1;
        this.buffer2 = buffer2;
        this.bufferPort = bufferPort;
        this.portSemaphore = portSemaphore;

        this.light = new Point[10];

        if(!verticalHorizontal)
            for (int i = 0; i < 10; i++)
            {
                if(i < 5)
                    this.light[i] = new Point(0, 30 + (40 * i));
                else
                    this.light[i] = new Point(25, 30 +(40 * i) - 200);
            }
        else
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                    this.light[i] = new Point(30 + (40 * i), 0);
                else
                    this.light[i] = new Point(30 + (40 * i) - 200, 25);
            }

        this.lightColour = Color.Black;
            
    }

    public void Start()
    {
        bool start = true;
        // Thread.Sleep(delay);
        this.colour = Color.Transparent;

        while (start)
        {
            this.zeroPlane();

            if (semaphore1 != null)
                semaphore1.Signal();

            buffer1.Read(ref this.colour);

            if (adjacentPort != null && adjacentPort.GetIsSelected() && this.colour == Color.Green)
            {
                panel.Invalidate();
                this.movePlane(0, 0);

                if (portSemaphore != null)
                    portSemaphore.Wait();                    

                bufferPort.Write(this.colour);
                // Set the adjacent port boolean true, as the plane is ready to dock
                adjacentPort.SetIsPlaneReady();

                if(!adjacentPort.IsPlaneDocked())
                {
                    this.colour = Color.Transparent;
                    panel.Invalidate();
                }

            }
            else           
                MoveAcrossPanel();
            
        }
    }

    private void zeroPlane()
    {
        plane.X = origin.X;
        plane.Y = origin.Y;
    }

    private void MoveAcrossPanel()
    {
        for (int i = 1; i <= 20; i++)
        {
            panel.Invalidate();
            this.movePlane(xDelta, yDelta);

            if (i % 2 == 0)
                lightColour = Color.Gray;
            else
                lightColour = Color.Black;

            Thread.Sleep(delay);
        }

        this.lightColour = Color.Black;

        if (semaphore2 != null)
            semaphore2.Wait();

        buffer2.Write(this.colour);
        this.colour = Color.Transparent;
        panel.Invalidate();
    }

    private void movePlane(int xDelta, int yDelta)
    {

        if (verticalHorizontal)
        {
            plane.X += xDelta; plane.Y += 0;
        }
        else
        {
            plane.X += 0; plane.Y += yDelta;
        }       
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        SolidBrush brush1 = new SolidBrush(colour);
        g.FillRectangle(brush1, plane.X, plane.Y, 10, 10);

        SolidBrush brush2 = new SolidBrush(lightColour);
        for (int i = 0; i < 10; i++)
            g.FillRectangle(brush2, light[i].X, light[i].Y, 5, 5);

        brush1.Dispose();    //  Dispose graphics resources. 
        g.Dispose();        //  
    }

    public Color GetPlaneColour() { return this.colour; }

}// end class WaitPanelThread


// TakeOffPanel determines which planes are for arrival or takeoff
public class TakeOffPanelThread
{
    private Point origin;
    private int delay;
    private Panel panel;
    // The Panel goes Up or Down
    private bool verticalHorizontal;
    // Positive or Negative direction to the Panel's relative axis (Up or Down)
    private bool positiveNegative;
    private Color colour;
    private Point plane;
    private int xDelta;
    private int yDelta;
    private Semaphore semaphore1;
    private Semaphore semaphore2;
    private Buffer buffer1;
    private Buffer buffer2;
    private RadioButton radiobtn;
    private bool greenTakeOff;
    private float sizeX = 10.0f;
    private float sizeY = 10.0f; 

    // Runway lights
    private Point[] light;
    private Color lightColour;

    public TakeOffPanelThread(Point origin,
                       int delay,
                       bool verticalHorizontal,
                       bool positiveNegative,
                       Panel panel,
                       Color colour,
                       Semaphore semaphore1,
                       Semaphore semaphore2,
                       Buffer buffer1,
                       Buffer buffer2,
                        RadioButton radiobtn)
    {
        this.origin = origin;
        this.delay = delay;
        this.positiveNegative = positiveNegative;
        this.panel = panel;
        this.colour = colour;
        this.plane = origin;
        this.panel.Paint += new PaintEventHandler(this.panel_Paint);
        this.verticalHorizontal = verticalHorizontal;
        this.xDelta = positiveNegative ? +10 : -10;
        this.yDelta = positiveNegative ? +10 : -10;
        this.semaphore1 = semaphore1;
        this.semaphore2 = semaphore2;
        this.buffer1 = buffer1;
        this.buffer2 = buffer2;
        this.radiobtn = radiobtn;
        this.radiobtn.CheckedChanged += new System.
                              EventHandler(this.radioButton_CheckChanged);

        this.light = new Point[20];

        if (!verticalHorizontal)
            for (int i = 0; i < 20; i++)
            {
                if (i < 10)
                    this.light[i] = new Point(0, 30 + (40 * i));
                else
                    this.light[i] = new Point(25, 30 + (40 * i) - 200);
            }
        else
            for (int i = 0; i < 20; i++)
            {
                if (i < 10)
                    this.light[i] = new Point(30 + (40 * i), 0);
                else
                    this.light[i] = new Point(30 + (40 * i) - 400, 25);
            }

        this.lightColour = Color.Black;
    }

    private void radioButton_CheckChanged(object sender, System.EventArgs e)
    {
        RadioButton rb = sender as RadioButton;

        if (rb.Checked)
            greenTakeOff = true;
        else
            greenTakeOff = false;
    }

    public void Start()
    {
        bool start = true;
        // Thread.Sleep(delay);
        this.colour = Color.Transparent;

        while (start)
        {
            this.zeroPlane();

            if (semaphore1 != null)
                semaphore1.Signal();
            else { ;}

            buffer1.Read(ref this.colour);

            if (this.colour == Color.Green && !greenTakeOff)
            {

                for (int i = 1; i <= 23; i++)
                {
                    panel.Invalidate();
                    this.movePlane(xDelta, yDelta);
                    
                    if (i % 2 == 0)
                        lightColour = Color.Red;
                    else
                        lightColour = Color.Black;
                    
                    Thread.Sleep(delay);
                }

                if (semaphore2 != null)
                    semaphore2.Wait();
                else { ;}

                buffer2.Write(this.colour);
            }
            else
            {
                for (int i = 1; i <= 20; i++)
                {
                    panel.Invalidate();
                    this.movePlane(xDelta * (int)(i * 0.5f), yDelta);

                    if (i % 2 == 0)
                        lightColour = Color.ForestGreen;
                    else
                        lightColour = Color.Black;

                    sizeX += 0.75f;
                    sizeY += 0.75f;
                    Thread.Sleep(delay);
                }

                sizeX = 10.0f;
                sizeY = 10.0f;
            }
            this.lightColour = Color.Black;
            this.colour = Color.Transparent;
            panel.Invalidate();
        }
    }

    private void zeroPlane()
    {
        plane.X = origin.X;
        plane.Y = origin.Y;
    }

    private void movePlane(int xDelta, int yDelta)
    {

        if (verticalHorizontal)
        {
            plane.X += xDelta; plane.Y += 0;
        }
        else
        {
            plane.X += 0; plane.Y += yDelta;
        }
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        SolidBrush brush1 = new SolidBrush(colour);
        g.FillRectangle(brush1, plane.X, plane.Y, sizeX, sizeY);

        SolidBrush brush2 = new SolidBrush(lightColour);
        for (int i = 0; i < 20; i++)
            g.FillRectangle(brush2, light[i].X, light[i].Y, 5, 5);
        
        brush1.Dispose();    //  Dispose graphics resources. 
        brush2.Dispose();
        g.Dispose();        //  
    }

    public Color GetPlaneColour() { return this.colour; }

    public bool GetGreenTakeOff() { return greenTakeOff; }

}// end class TakeOffPanelThread

public class ArrivalHubPanelThread
{
    private Point origin;
    private int delay;
    private Panel panel;
    private bool leftRight;
    private Color colour;
    private Point plane;
    private int xDelta;
    private int yDelta;
    private Semaphore semaphore;
    private Buffer buffer;
    private Button btn;
    private GroupBox groupBox;
    private bool locked = true;

    public ArrivalHubPanelThread(Point origin,
                                 int delay,
                                 bool leftRight,
                                 Panel panel,
                                 Color colour,
                                 Semaphore semaphore,
                                 Buffer buffer,
                                 Button btn)
    {
        this.origin = origin;
        this.delay = delay;
        this.leftRight = leftRight;
        this.panel = panel;
        this.colour = colour;
        this.plane = origin;
        this.panel.Paint += new PaintEventHandler(this.panel_Paint);
        this.xDelta = leftRight ? -10 : +10;
        this.yDelta = 0;
        this.semaphore = semaphore;
        this.buffer = buffer;
        this.btn = btn;
        this.btn.Click += new System.
                              EventHandler(this.btn_Click);
    }

    private void btn_Click(object sender,
                           System.EventArgs e)
    {
        locked = !locked;
        this.btn.BackColor = locked ? Color.Pink : Color.LightGreen;
        lock (this)
        {
            if (!locked)
                Monitor.Pulse(this);
        }
    }

    public void Start()
    {
        Color signal = Color.Red;
        Thread.Sleep(delay);
        this.zeroPlane();
        panel.Invalidate();

        lock (this)
        {
            while (locked)
            {
                Monitor.Wait(this);
            }
        }

        for (int i = 1; i <= 13; i++)
        {
            this.movePlane(((xDelta*4) / (int)(i)) - 10, yDelta);
            Thread.Sleep(delay);
            panel.Invalidate();
        }
        semaphore.Wait();
        buffer.Write(this.colour);
        this.colour = Color.Transparent;
        panel.Invalidate();
    }

    private void zeroPlane()
    {
        plane.X = origin.X;
        plane.Y = origin.Y;
    }

    private void movePlane(int xDelta, int yDelta)
    {
        plane.X += xDelta; plane.Y += yDelta;
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        SolidBrush brush = new SolidBrush(colour);
        g.FillRectangle(brush, plane.X, plane.Y, 10, 10);
        brush.Dispose();    //  Dispose graphics resources. 
        g.Dispose();        //  
    }

    public Color GetPlaneColour() { return this.colour; }

    public bool IsThePlaneLocked() { return locked; }

}// end class ArrivalHubPanelThread

public class AirportEventLogger
{
    private Socket connection;
    private NetworkStream socketStream;
    private BinaryWriter writer;

    private Color colour;
    private Color previousColour;

    private TakeOffPanelThread takeOffRunway;
    private PlaneStatusChecker planeStatusChecker;

    public AirportEventLogger(TakeOffPanelThread takeOffRunway,
                            PlaneStatusChecker planeStatusChecker) 
    {
        this.takeOffRunway = takeOffRunway;
        this.planeStatusChecker = planeStatusChecker;
    }

    public void Start()
    {
        // Run server
        TcpListener listener;
        int counter = 1;
        // Number of takeoffs
        int takeoffCount = 0;
        // Filler to space out the sentences when writing to the logger
        string filler = " ";
        string previousReport = "";

        try
        {
            IPAddress local = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(local, 5000);
            listener.Start();

            while (true)
            {              
                connection = listener.AcceptSocket(); // Block while waiting for a
                // connection from the client.
                socketStream = new NetworkStream(connection);
                writer = new BinaryWriter(socketStream);

                writer.Write("SERVER>>> Connection " + counter + " succesful");             
                do
                {
                    try
                    {
                        string report = planeStatusChecker.CheckAllPlaneStatus();

                        colour = takeOffRunway.GetPlaneColour();

                        if (colour != previousColour && ((colour == Color.LightGreen || colour == Color.Red || colour == Color.Blue || colour == Color.Yellow) || (colour == Color.Green && takeOffRunway.GetGreenTakeOff() == true)))
                        {
                            takeoffCount++;

                            if (takeoffCount > 9)
                                filler = "";
                            
                            writer.Write(report + takeoffCount + filler + "SERVER>>>" + colour.ToString() + " Plane has taken off");
                        }
                        else
                            if (previousReport != report)
                                writer.Write(report);
                        // This is so the server only sends one message at a time
                        previousColour = colour;
                        previousReport = report;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                while (connection.Connected);

                writer.Close();
                socketStream.Close();
                connection.Close();
                counter++;
            }
        }
        catch (Exception error)
        {
            MessageBox.Show(error.ToString());
        }
    }
}

public class PlaneStatusChecker // Checks every panel and returns its current colour, if the color is any of the planes, then write the first letter into a string to send to the airport logger
{
     // The overall string being sent to the logger will contain a single letter from the colour of the plane from each of these threads in the order they are in this class
     // (First a letter is from runwayA, then runwayB, up to port3)
    private WaitPanelThread runwayA, runwayB, runwayC, runwayD, runwayE;
    private TakeOffPanelThread takeOffRunway;
    private ArrivalHubPanelThread arrivalHub;
    private ButtonPanelThread port1, port2, port3; 
    
    private string planeStatuses;
    
    public PlaneStatusChecker(WaitPanelThread runwayA,
                             WaitPanelThread runwayB,
                             WaitPanelThread runwayC,
                             WaitPanelThread runwayD,
                             WaitPanelThread runwayE,
                             TakeOffPanelThread takeOffRunway,
                             ArrivalHubPanelThread arrivalHub,
                             ButtonPanelThread port1,
                             ButtonPanelThread port2,
                             ButtonPanelThread port3)
     {
         this.runwayA = runwayA;
         this.runwayB = runwayB;
         this.runwayC = runwayC;
         this.runwayD = runwayD;
         this.runwayE = runwayE;
         this.takeOffRunway = takeOffRunway;
         this.arrivalHub = arrivalHub;
         this.port1 = port1;
         this.port2 = port2;
         this.port3 = port3;
     }

    public string CheckAllPlaneStatus()
    {
        string current = "";

        current += ColourCode(runwayA.GetPlaneColour());
        current += ColourCode(runwayB.GetPlaneColour());
        current += ColourCode(runwayC.GetPlaneColour());
        current += ColourCode(runwayD.GetPlaneColour());
        current += ColourCode(runwayE.GetPlaneColour());
        current += ColourCode(takeOffRunway.GetPlaneColour());
        current += ColourCode(arrivalHub.GetPlaneColour());
        current += ColourCode(port1.GetPlaneColour());
        current += ColourCode(port2.GetPlaneColour());
        current += ColourCode(port3.GetPlaneColour());

        current += CheckPlaneObjective(current);

        return current;
    }
    
    public string ColourCode(Color colour)
    {
        // By default the returning letter will be "N" for NULL
        string returningColour = "N";
    
        if (colour == Color.Yellow || colour == Color.Blue || colour == Color.Red || colour == Color.Green || colour == Color.LightGreen)
        {
            // So if the colour of the plane was Blue, then the returning letter would be "B"
            returningColour = colour.ToString();
            return returningColour.Substring(7, 1);           
        }
    
        return returningColour.Substring(0, 1);
    }

    public string CheckPlaneObjective(string current)
    {
        string message = "";

        // Check Blue Plane Objective
        if(current.IndexOf("B") != -1)
        {
            if (port1.IsThePlaneLocked() && port1.GetPlaneColour() == Color.Blue)
                message += "B1";
            else
                message += "B2";
        }
        else
        {
            message += "B3";
        }

        // Check Red Plane Objective
        if (current.IndexOf("R") != -1)
        {
            if (port2.IsThePlaneLocked() && port2.GetPlaneColour() == Color.Red)
                message += "R1";
            else
                message += "R2";
        }
        else
        {
            message += "R3";
        }

        // Check Yellow Plane Objective
        if (current.IndexOf("Y") != -1)
        {
            if (port3.IsThePlaneLocked() && port3.GetPlaneColour() == Color.Yellow)
                message += "Y1";
            else
                message += "Y2";
        }
        else
        {
            message += "Y3";
        }

        // Check Green Plane Objective
        if (current.IndexOf("G") != -1 || 
            (port1.GetPlaneColour() == Color.LightGreen && port1.IsThePlaneLocked()) ||
            (port2.GetPlaneColour() == Color.LightGreen && port2.IsThePlaneLocked()) ||
            (port3.GetPlaneColour() == Color.LightGreen && port3.IsThePlaneLocked()))
        {
            if (port1.GetPlaneColour() == Color.LightGreen || port2.GetPlaneColour() == Color.LightGreen || port3.GetPlaneColour() == Color.LightGreen)
                message += "G1";
            else if (arrivalHub.IsThePlaneLocked() && arrivalHub.GetPlaneColour() == Color.Green)
                message += "G4";
            else if (port1.GetIsSelected())
                message += "G5";
            else if (port2.GetIsSelected())
                message += "G6";
            else if (port3.GetIsSelected())
                message += "G7";
            else if (takeOffRunway.GetGreenTakeOff())
                message += "G2";
            else
                message += "G8";
        }
        else
        {
            message += "G3";
        }

        return message;
    }

}// end class PlaneStatusChecker

public class TheOne
{
    public static void Main()//
    {
        Application.Run(new Form1());
    }
}// end class TheOne
