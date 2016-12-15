using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.UITests.Api;

namespace SeleniumRecorderPOC
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
