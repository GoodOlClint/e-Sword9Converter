using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoComplete
{
    public class AutoCompleteTextBox : TextBox
    {
        #region SHLWAPI SHAutoComplete API

        [DllImport("SHLWAPI", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int SHAutoComplete(IntPtr hwndEdit,
            SHAutoCompleteFlags dwFlags);

        [Flags]
        enum SHAutoCompleteFlags : uint
        {
            /// <summary>
            /// Currently (FileSystem | UrlAll)
            /// </summary>
            Default = 0x00000000,

            /// <summary>
            /// This includes the File System as well as the rest 
            /// of the shell (Desktop\My Computer\Control Panel\)
            /// </summary>
            FileSystem = 0x00000001,

            /// <summary>
            /// URLs in the User's History and URLs in the User's 
            /// Recently Used list.
            /// </summary>
            UrlAll = (UrlHistory | UrlMRU),

            /// <summary>
            /// URLs in the User's History
            /// </summary>
            UrlHistory = 0x00000002,

            /// <summary>
            /// URLs in the User's Recently Used list.
            /// </summary>
            UrlMRU = 0x00000004,

            /// <summary>
            ///  Use the tab to move thru the autocomplete 
            ///  possibilities instead of to the next dialog/window
            ///  control.
            /// </summary>
            UseTab = 0x00000008,

            /// <summary>
            /// Don't AutoComplete non-File System items.
            /// </summary>
            FileSysOnly = 0x00000010,

            /// <summary>
            /// Ignore the registry default and force the feature on.
            /// </summary>
            AutoSuggestForceOn = 0x10000000,

            /// <summary>
            /// Ignore the registry default and force the feature off.
            /// </summary>
            AutoSuggestForceOff = 0x20000000,

            /// <summary>
            /// Ignore the registry default and force the feature on. 
            /// (Also known as AutoComplete)
            /// </summary>
            AutoAppendForceOn = 0x40000000,

            /// <summary>
            /// Ignore the registry default and force the feature off. 
            /// (Also known as AutoComplete)
            /// </summary>
            AutoAppendForceOff = 0x80000000
        }

        #endregion

        private System.ComponentModel.Container components = null;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
        }

        bool m_autocomplete_urls = false;
        bool m_autocomplete_files = false;

        [Browsable(true),
        DesignOnly(true),
        Description("Gets or sets whether or not to autocomplete URLs."),
        Category("Behavior")]
        public bool AutoCompleteUrls
        {
            get
            {
                return this.m_autocomplete_urls;
            }
            set
            {
                this.m_autocomplete_urls = value;
            }
        }

        [Browsable(true),
        DesignOnly(true),
        Description("Gets or sets whether or not to autocomplete file paths."),
        Category("Behavior")]
        public bool AutoCompleteFiles
        {
            get
            {
                return this.m_autocomplete_files;
            }
            set
            {
                this.m_autocomplete_files = value;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (this.AutoCompleteUrls || this.AutoCompleteFiles)
            {
                Application.OleRequired();

                SHAutoCompleteFlags flags = SHAutoCompleteFlags.AutoSuggestForceOn |
                    SHAutoCompleteFlags.AutoAppendForceOn;

                if (this.AutoCompleteUrls) flags |= SHAutoCompleteFlags.UrlAll;
                if (this.AutoCompleteFiles) flags |= SHAutoCompleteFlags.FileSystem;

                SHAutoComplete(this.Handle, flags);
            }

            base.OnHandleCreated(e);
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

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion
    }
}