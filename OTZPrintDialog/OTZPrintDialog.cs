using System;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace OTZPrintDialog {
    /// <summary>
    /// OTZPrintDialog
    /// </summary>
    public class OTZPrintDialog : CommonDialog {

        [DllImport("comdlg32.dll", EntryPoint = "PrintDlgW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool PrintDlg(ref PRINTDLG_32 lpPrintdlg);

        [DllImport("comdlg32.dll", EntryPoint = "PrintDlgW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool PrintDlg(ref PRINTDLG_64 lpPrintdlg);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalFree(IntPtr hMem);

        private PageSettings pageSettings;
        private PrintDocument printDocument;
        private PrinterSettings printerSettings;
        private const int PD_PRINTSETUP = 0x40;
        private const int PD_ENABLESETUPHOOK = 0x2000;
        private delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
        private struct PRINTDLG_32 {
            public int lStructSize;
            public IntPtr hwndOwner;
            public IntPtr hDevMode;
            public IntPtr hDevNames;
            public IntPtr hDC;
            public int Flags;
            public short nFromPage;
            public short nToPage;
            public short nMinPage;
            public short nMaxPage;
            public short nCopies;
            public IntPtr hInstance;
            public IntPtr lCustData;
            public WndProc lpfnPrintHook;
            public WndProc lpfnSetupHook;
            public string lpPrintTemplateName;
            public string lpSetupTemplateName;
            public IntPtr hPrintTemplate;
            public IntPtr hSetupTemplate;
            //public PRINTDLG_32() {

            //}
        }

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct PRINTDLG_64 {
            public int lStructSize;
            public IntPtr hwndOwner;
            public IntPtr hDevMode;
            public IntPtr hDevNames;
            public IntPtr hDC;
            public int Flags;
            public short nFromPage;
            public short nToPage;
            public short nMinPage;
            public short nMaxPage;
            public short nCopies;
            public IntPtr hInstance;
            public IntPtr lCustData;
            public WndProc lpfnPrintHook;
            public WndProc lpfnSetupHook;
            public string lpPrintTemplateName;
            public string lpSetupTemplateName;
            public IntPtr hPrintTemplate;
            public IntPtr hSetupTemplate;
            //public PRINTDLG_64() {
            //}
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OTZPrintDialog() {
            this.Reset();
        }

        /// <summary>
        /// Document
        /// </summary>
        public PrintDocument Document {
            get { return this.printDocument; }
            set {
                this.printDocument = value;
                if (this.printDocument != null) {
                    this.pageSettings = this.printDocument.DefaultPageSettings;
                    this.printerSettings = this.printDocument.PrinterSettings;
                }
            }
        }

        /// <summary>
        /// Reset
        /// </summary>
        public override void Reset() {
            pageSettings = null;
            printDocument = null;
            printerSettings = null;
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <param name="hwndOwner"></param>
        /// <returns></returns>
        protected override bool RunDialog(IntPtr hwndOwner) {
            WndProc hookProcPtr = new WndProc(this.HookProc);
            // 32bitの場合
            if (IntPtr.Size == 4) {
                PRINTDLG_32 printDlg = new PRINTDLG_32();
                try {
                    printDlg.lStructSize = Marshal.SizeOf(printDlg);
                    printDlg.hwndOwner = hwndOwner;
                    printDlg.lpfnSetupHook = hookProcPtr;
                    printDlg.nFromPage = (short)this.printerSettings.FromPage;
                    printDlg.nToPage = (short)this.printerSettings.ToPage;
                    printDlg.nMinPage = (short)this.printerSettings.MinimumPage;
                    printDlg.nMaxPage = (short)this.printerSettings.MaximumPage;
                    printDlg.nCopies = this.printerSettings.Copies;
                    if (this.printerSettings == null) {
                        printDlg.hDevMode = this.printerSettings.GetHdevmode();
                    } else {
                        printDlg.hDevMode = this.printerSettings.GetHdevmode(this.pageSettings);
                    }
                    printDlg.hDevNames = this.printerSettings.GetHdevnames();
                    printDlg.Flags = PD_PRINTSETUP | PD_ENABLESETUPHOOK;
                    // ダイアログ表示
                    if (PrintDlg(ref printDlg)) {
                        Cursor.Current = Cursors.WaitCursor;
                        this.printerSettings.SetHdevmode(printDlg.hDevMode);
                        this.printerSettings.SetHdevnames(printDlg.hDevNames);
                        this.pageSettings.SetHdevmode(printDlg.hDevMode);
                        return true;
                    }
                    return false;
                } finally {
                    if (printDlg.hDevMode != IntPtr.Zero) {
                        GlobalFree(printDlg.hDevMode);
                    }
                    if (printDlg.hDevNames != IntPtr.Zero) {
                        GlobalFree(printDlg.hDevNames);
                    }
                }

                // 64bitの場合
            } else {
                PRINTDLG_64 printDlg = new PRINTDLG_64();
                try {
                    printDlg.lStructSize = Marshal.SizeOf(printDlg);
                    printDlg.hwndOwner = hwndOwner;
                    printDlg.lpfnSetupHook = hookProcPtr;
                    printDlg.nFromPage = (short)this.printerSettings.FromPage;
                    printDlg.nToPage = (short)this.printerSettings.ToPage;
                    printDlg.nMinPage = (short)this.printerSettings.MinimumPage;
                    printDlg.nMaxPage = (short)this.printerSettings.MaximumPage;
                    printDlg.nCopies = this.printerSettings.Copies;
                    if (this.printerSettings == null) {
                        printDlg.hDevMode = this.printerSettings.GetHdevmode();
                    } else {
                        printDlg.hDevMode = this.printerSettings.GetHdevmode(this.pageSettings);
                    }
                    printDlg.hDevNames = this.printerSettings.GetHdevnames();
                    printDlg.Flags = PD_PRINTSETUP | PD_ENABLESETUPHOOK;
                    // ダイアログ表示
                    if (PrintDlg(ref printDlg)) {
                        Cursor.Current = Cursors.WaitCursor;
                        this.printerSettings.SetHdevmode(printDlg.hDevMode);
                        this.printerSettings.SetHdevnames(printDlg.hDevNames);
                        this.pageSettings.SetHdevmode(printDlg.hDevMode);
                        return true;
                    }
                    return false;
                } finally {
                    if (printDlg.hDevMode != IntPtr.Zero) {
                        GlobalFree(printDlg.hDevMode);
                    }
                    if (printDlg.hDevNames != IntPtr.Zero) {
                        GlobalFree(printDlg.hDevNames);
                    }
                }
            }
        }
    }
}