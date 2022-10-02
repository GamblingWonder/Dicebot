namespace DiceBot.UserControls
{
    partial class ProgrammerModeControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            this.pnlProgrammer = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.pnlLoadProgrammer = new System.Windows.Forms.Panel();
            this.btnOpenCode = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnCodeSave = new System.Windows.Forms.Button();
            this.tpConsole = new System.Windows.Forms.TabPage();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.txtConsoleIn = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.lbVariables = new System.Windows.Forms.ListBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pnlControlProgrammer = new System.Windows.Forms.Panel();
            this.btnStopProgrammer = new System.Windows.Forms.Button();
            this.btnStartProgrammer = new System.Windows.Forms.Button();
            this.btnPauseResumeProgrammer = new System.Windows.Forms.Button();
            this.btnStopNextWinProgrammer = new System.Windows.Forms.Button();
            this.pnlProgrammer.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.pnlLoadProgrammer.SuspendLayout();
            this.tpConsole.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.pnlControlProgrammer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlProgrammer
            // 
            this.pnlProgrammer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProgrammer.AutoScroll = true;
            this.pnlProgrammer.Controls.Add(this.tabControl2);
            this.pnlProgrammer.Controls.Add(this.pnlControlProgrammer);
            this.pnlProgrammer.Location = new System.Drawing.Point(0, 0);
            this.pnlProgrammer.Margin = new System.Windows.Forms.Padding(0);
            this.pnlProgrammer.Name = "pnlProgrammer";
            this.pnlProgrammer.Size = new System.Drawing.Size(612, 470);
            this.pnlProgrammer.TabIndex = 10;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tpConsole);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(612, 435);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage10
            // 
            this.tabPage10.AutoScroll = true;
            this.tabPage10.Controls.Add(this.pnlLoadProgrammer);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(604, 409);
            this.tabPage10.TabIndex = 0;
            this.tabPage10.Text = "Code";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // pnlLoadProgrammer
            // 
            this.pnlLoadProgrammer.Controls.Add(this.btnOpenCode);
            this.pnlLoadProgrammer.Controls.Add(this.button3);
            this.pnlLoadProgrammer.Controls.Add(this.btnCodeSave);
            this.pnlLoadProgrammer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoadProgrammer.Location = new System.Drawing.Point(3, 3);
            this.pnlLoadProgrammer.Name = "pnlLoadProgrammer";
            this.pnlLoadProgrammer.Size = new System.Drawing.Size(598, 34);
            this.pnlLoadProgrammer.TabIndex = 10;
            // 
            // btnOpenCode
            // 
            this.btnOpenCode.Location = new System.Drawing.Point(4, 3);
            this.btnOpenCode.Name = "btnOpenCode";
            this.btnOpenCode.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCode.TabIndex = 9;
            this.btnOpenCode.Text = "Open";
            this.btnOpenCode.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(513, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Help!";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnCodeSave
            // 
            this.btnCodeSave.Location = new System.Drawing.Point(85, 3);
            this.btnCodeSave.Name = "btnCodeSave";
            this.btnCodeSave.Size = new System.Drawing.Size(75, 23);
            this.btnCodeSave.TabIndex = 10;
            this.btnCodeSave.Text = "Save";
            this.btnCodeSave.UseVisualStyleBackColor = true;
            // 
            // tpConsole
            // 
            this.tpConsole.AutoScroll = true;
            this.tpConsole.Controls.Add(this.rtbConsole);
            this.tpConsole.Controls.Add(this.txtConsoleIn);
            this.tpConsole.Location = new System.Drawing.Point(4, 22);
            this.tpConsole.Name = "tpConsole";
            this.tpConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tpConsole.Size = new System.Drawing.Size(604, 409);
            this.tpConsole.TabIndex = 1;
            this.tpConsole.Text = "Console";
            this.tpConsole.UseVisualStyleBackColor = true;
            // 
            // rtbConsole
            // 
            this.rtbConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConsole.HideSelection = false;
            this.rtbConsole.Location = new System.Drawing.Point(3, 3);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(598, 334);
            this.rtbConsole.TabIndex = 0;
            this.rtbConsole.Text = "";
            // 
            // txtConsoleIn
            // 
            this.txtConsoleIn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtConsoleIn.Location = new System.Drawing.Point(3, 337);
            this.txtConsoleIn.Multiline = true;
            this.txtConsoleIn.Name = "txtConsoleIn";
            this.txtConsoleIn.Size = new System.Drawing.Size(598, 69);
            this.txtConsoleIn.TabIndex = 1;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.lbVariables);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(604, 409);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "Variables";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // lbVariables
            // 
            this.lbVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVariables.FormattingEnabled = true;
            this.lbVariables.Items.AddRange(new object[] {
            "balance:decimal, RO",
            "win:bool, RO",
            "profit:decimal, RO",
            "currentprofit:decimal, RO",
            "currentstreak:decimal, RO",
            "previousbet:decimal, RO",
            "bets:int, RO",
            "wins:int, RO",
            "losses:int, RO",
            "nextbet:decimal, RW",
            "chance:decimal, RW",
            "bethigh:bool, RW",
            "lastBet:Bet, RO",
            "currencies:string[], RO. List of currencies available at current site",
            "currency:string, RW. Current betting currency",
            "enablersc: bool, RW",
            "enablezz: bool, RW",
            "site:SiteDetails, RO"});
            this.lbVariables.Location = new System.Drawing.Point(3, 3);
            this.lbVariables.Name = "lbVariables";
            this.lbVariables.Size = new System.Drawing.Size(598, 403);
            this.lbVariables.TabIndex = 3;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.listBox1);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(604, 409);
            this.tabPage9.TabIndex = 3;
            this.tabPage9.Text = "Functions/Methods";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Items.AddRange(new object[] {
            "withdraw(amount:decimal, bitcoinaddress:string)",
            "invest(amount:decimal)",
            "tip(username/userid:string, amount:decimal)",
            "stop()",
            "resetseed();",
            "print(messagetoprint:string)",
            "getHistory(): Bet[]",
            "getHistoryByDate(FromDateTime:string (inclusive), UntillDateTime:string (exclusiv" +
                "e)): Bet[]",
            "getHistoryByQuery(SQLiteQuery:string): Bet[]",
            "martingale(win:boolean):decimal",
            "labouchere(win:boolean):decimal",
            "fibonacci(win:bool):decimal",
            "dalembert(win:bool):decimal",
            "presetlist(win:bool):decimal",
            "resetstats(): void",
            "setvalueint(name:string, value:int): void",
            "setvaluestring(name:string, value:string): void",
            "setvaluedecimal(name:string, value:decimal): void",
            "setvaluebool(name:string, value:bool): void",
            "getvalue(name:string): object",
            "loadstrategy(file:string): bool",
            "read(prompt:string, type:int): object --0: bool, 1:int, 2:decimal, 3:string",
            "readadv(prompt:string, type:int, formtext:string, cancelbuttontext:string, okbutt" +
                "ontext:string ): Object",
            "alarm():void",
            "ching():void",
            "resetbuiltin():void",
            "exportsim():void"});
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(598, 403);
            this.listBox1.TabIndex = 8;
            // 
            // pnlControlProgrammer
            // 
            this.pnlControlProgrammer.Controls.Add(this.btnStopNextWinProgrammer);
            this.pnlControlProgrammer.Controls.Add(this.btnPauseResumeProgrammer);
            this.pnlControlProgrammer.Controls.Add(this.btnStopProgrammer);
            this.pnlControlProgrammer.Controls.Add(this.btnStartProgrammer);
            this.pnlControlProgrammer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControlProgrammer.Location = new System.Drawing.Point(0, 435);
            this.pnlControlProgrammer.Name = "pnlControlProgrammer";
            this.pnlControlProgrammer.Size = new System.Drawing.Size(612, 35);
            this.pnlControlProgrammer.TabIndex = 1;
            // 
            // btnStopProgrammer
            // 
            this.btnStopProgrammer.Location = new System.Drawing.Point(128, 7);
            this.btnStopProgrammer.Name = "btnStopProgrammer";
            this.btnStopProgrammer.Size = new System.Drawing.Size(112, 23);
            this.btnStopProgrammer.TabIndex = 1;
            this.btnStopProgrammer.Text = "Stop";
            this.btnStopProgrammer.UseVisualStyleBackColor = true;
            // 
            // btnStartProgrammer
            // 
            this.btnStartProgrammer.Location = new System.Drawing.Point(4, 7);
            this.btnStartProgrammer.Name = "btnStartProgrammer";
            this.btnStartProgrammer.Size = new System.Drawing.Size(112, 23);
            this.btnStartProgrammer.TabIndex = 0;
            this.btnStartProgrammer.Text = "Start";
            this.btnStartProgrammer.UseVisualStyleBackColor = true;
            // 
            // btnPauseResumeProgrammer
            // 
            this.btnPauseResumeProgrammer.BackColor = System.Drawing.SystemColors.Control;
            this.btnPauseResumeProgrammer.Location = new System.Drawing.Point(252, 7);
            this.btnPauseResumeProgrammer.Name = "btnPauseResumeProgrammer";
            this.btnPauseResumeProgrammer.Size = new System.Drawing.Size(112, 23);
            this.btnPauseResumeProgrammer.TabIndex = 2;
            this.btnPauseResumeProgrammer.Text = "Pause";
            this.btnPauseResumeProgrammer.UseVisualStyleBackColor = false;
            // 
            // btnStopNextWinProgrammer
            // 
            this.btnStopNextWinProgrammer.Location = new System.Drawing.Point(376, 7);
            this.btnStopNextWinProgrammer.Name = "btnStopNextWinProgrammer";
            this.btnStopNextWinProgrammer.Size = new System.Drawing.Size(112, 23);
            this.btnStopNextWinProgrammer.TabIndex = 3;
            this.btnStopNextWinProgrammer.Text = "Stop on Next Win";
            this.btnStopNextWinProgrammer.UseVisualStyleBackColor = true;
            // 
            // ProgrammerModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlProgrammer);
            this.Name = "ProgrammerModeControl";
            this.Size = new System.Drawing.Size(615, 473);
            this.pnlProgrammer.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.pnlLoadProgrammer.ResumeLayout(false);
            this.tpConsole.ResumeLayout(false);
            this.tpConsole.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.pnlControlProgrammer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpConsole;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage9;
        public System.Windows.Forms.Panel pnlProgrammer;
        public System.Windows.Forms.Panel pnlControlProgrammer;
        public System.Windows.Forms.TabPage tabPage10;
        public System.Windows.Forms.RichTextBox rtbConsole;
        public System.Windows.Forms.TextBox txtConsoleIn;
        public System.Windows.Forms.ListBox lbVariables;
        public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Panel pnlLoadProgrammer;
        public System.Windows.Forms.Button btnStopNextWinProgrammer;
        public System.Windows.Forms.Button btnPauseResumeProgrammer;
        public System.Windows.Forms.Button btnOpenCode;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button btnCodeSave;
        public System.Windows.Forms.Button btnStopProgrammer;
        public System.Windows.Forms.Button btnStartProgrammer;
    }
}
