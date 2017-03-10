using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System.Drawing;

namespace BrowserRecorder
{
    public partial class frmMain : Form
    {
        private readonly string browswerType = "2";
        private XrmBrowser browser;
        private InMemoryBrowserActionLogger logger = new InMemoryBrowserActionLogger();
        private List<BrowserRecordingEvent> events;

        public frmMain()
        {
            InitializeComponent();

            //this Form will always be on top.
            this.TopMost = true;

            //Set the Start position to top middle
            //this.Location = new Point(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y);
            //this.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            //this.StartPosition = FormStartPosition.Manual;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            btnRecord.Enabled = false;
            btnStop.Enabled = true;
            btnViewEvents.Enabled = false;
            
            browser.Record(logger);
            
            
            Application.DoEvents();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            browser.StopRecording();
            events = logger.Events.ToList();
            logger = new InMemoryBrowserActionLogger();

            btnRecord.Enabled = true;
            btnStop.Enabled = false;
            btnViewEvents.Enabled = true;
            
            //browser.Dispose();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            browser = new XrmBrowser(new BrowserOptions()
            {
                BrowserType = (BrowserType) Enum.Parse(typeof(BrowserType), browswerType),
                PrivateMode = false,
                CleanSession = false,
                FireEvents = true,
                EnableRecording = true
            });

            browser.Navigate("https://naosrtw.crm.dynamics.com/");
        }

        private void btnViewEvents_Click(object sender, EventArgs e)
        {
            EventViewer evtForm = new EventViewer(events);
            evtForm.Show();
        }
    }
}
