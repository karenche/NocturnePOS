using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace CardHelper
{
    public partial class CardEmulatorForm : Form
    {
        private readonly ServiceHost _host;
        private readonly RfidMonitor _rfidMonitor;

        public CardEmulatorForm()
        {
            InitializeComponent();
            Text = "Card emulator";
            _host = new ServiceHost(typeof(HelperService));
            _rfidMonitor = new RfidMonitor("COM3");
        }

        private void CardEmulatorForm_Load(object sender, EventArgs e)
        {
            _host.Open();
            _rfidMonitor.Open();

        }

        private void CardEmulatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _host.Close();
            _rfidMonitor.Close();
        }

        private void btnInsertRfidCard_Click(object sender, EventArgs e)
        {
            ulong result;
            if (ulong.TryParse(tbRfid.Text, out result))
            {
                HelperService.UID = result;
            }
        }

        private void btnInsertIdCard_Click(object sender, EventArgs e)
        {
            HelperService.IdCardData = new IdCardData
            {
                IdCode = tbRegCode.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text
            };
        }

        private void btnWithoutRole_Click(object sender, EventArgs e)
        {
            HelperService.UID = 10000000000003;
        }

        private void btnWorkerRole_Click(object sender, EventArgs e)
        {
            HelperService.UID = 10000000000002;
        }

        private void btnAdminRole_Click(object sender, EventArgs e)
        {
            HelperService.UID = 10000000000001;
        }

        private void btnAminAndWorkerRole_Click(object sender, EventArgs e)
        {
            HelperService.UID = 10000000000004;
        }
    }
}
