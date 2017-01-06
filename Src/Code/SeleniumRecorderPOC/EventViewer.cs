using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Dynamics365.UITests.Browser;

namespace BrowserRecorder
{
    public partial class EventViewer : Form
    {
        public List<BrowserRecordingEvent> events;
        public EventViewer()
        {
            InitializeComponent();
        }

        public EventViewer(List<BrowserRecordingEvent> evts)
        {
            InitializeComponent();
            events = evts;
        }

        private void EventViewer_Load(object sender, EventArgs e)
        {
            if (events != null)
            {
                dgEvents.DataSource = events;
            }
            
        }
    }
}
