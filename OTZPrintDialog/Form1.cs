using System.Diagnostics;
using System.Drawing.Printing;

namespace OTZPrintDialog {

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            OTZPrintDialog dialog = new OTZPrintDialog();
            dialog.Document = new PrintDocument();
            if (dialog.ShowDialog() == DialogResult.OK) {
                // ƒvƒŠƒ“ƒ^–¼
                Debug.WriteLine(dialog.Document.PrinterSettings.PrinterName);
            }
        }
    }
}