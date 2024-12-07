namespace Skaner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbUrzadzenie = new ComboBox();
            cbTryb = new ComboBox();
            cbFormat = new ComboBox();
            cbCzescObrazu = new ComboBox();
            bSkanuj = new Button();
            bZapisz = new Button();
            bPolacz = new Button();
            obraz = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)obraz).BeginInit();
            SuspendLayout();
            // 
            // cbUrzadzenie
            // 
            cbUrzadzenie.FormattingEnabled = true;
            cbUrzadzenie.Location = new Point(11, 34);
            cbUrzadzenie.Name = "cbUrzadzenie";
            cbUrzadzenie.Size = new Size(121, 23);
            cbUrzadzenie.TabIndex = 2;
            // 
            // cbTryb
            // 
            cbTryb.FormattingEnabled = true;
            cbTryb.Items.AddRange(new object[] { "Kolorowy", "Czarno-biały" });
            cbTryb.Location = new Point(11, 97);
            cbTryb.Name = "cbTryb";
            cbTryb.Size = new Size(121, 23);
            cbTryb.TabIndex = 3;
            // 
            // cbFormat
            // 
            cbFormat.FormattingEnabled = true;
            cbFormat.Items.AddRange(new object[] { "jpeg", "png" });
            cbFormat.Location = new Point(11, 158);
            cbFormat.Name = "cbFormat";
            cbFormat.Size = new Size(121, 23);
            cbFormat.TabIndex = 5;
            // 
            // cbCzescObrazu
            // 
            cbCzescObrazu.FormattingEnabled = true;
            cbCzescObrazu.Items.AddRange(new object[] { "1/1", "1/2", "1/4" });
            cbCzescObrazu.Location = new Point(11, 225);
            cbCzescObrazu.Name = "cbCzescObrazu";
            cbCzescObrazu.Size = new Size(121, 23);
            cbCzescObrazu.TabIndex = 7;
            // 
            // bSkanuj
            // 
            bSkanuj.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bSkanuj.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bSkanuj.Location = new Point(11, 723);
            bSkanuj.Name = "bSkanuj";
            bSkanuj.Size = new Size(683, 48);
            bSkanuj.TabIndex = 10;
            bSkanuj.Text = "Skanuj";
            bSkanuj.UseVisualStyleBackColor = true;
            bSkanuj.Click += bSkanuj_click;
            // 
            // bZapisz
            // 
            bZapisz.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bZapisz.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bZapisz.Location = new Point(12, 779);
            bZapisz.Name = "bZapisz";
            bZapisz.Size = new Size(683, 46);
            bZapisz.TabIndex = 11;
            bZapisz.Text = "Zapisz";
            bZapisz.UseVisualStyleBackColor = true;
            bZapisz.Click += bZapisz_click;
            // 
            // bPolacz
            // 
            bPolacz.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bPolacz.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bPolacz.Location = new Point(12, 669);
            bPolacz.Name = "bPolacz";
            bPolacz.Size = new Size(683, 48);
            bPolacz.TabIndex = 12;
            bPolacz.Text = "Połącz";
            bPolacz.UseVisualStyleBackColor = true;
            bPolacz.Click += bPolacz_click;
            // 
            // obraz
            // 
            obraz.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            obraz.Location = new Point(161, 12);
            obraz.Name = "obraz";
            obraz.Size = new Size(533, 651);
            obraz.TabIndex = 13;
            obraz.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 14;
            label1.Text = "Urządzenie";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 79);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 15;
            label2.Text = "Tryb";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 140);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 16;
            label3.Text = "Format zapisu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 207);
            label4.Name = "label4";
            label4.Size = new Size(76, 15);
            label4.TabIndex = 17;
            label4.Text = "Część obrazu";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 837);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(obraz);
            Controls.Add(bPolacz);
            Controls.Add(bZapisz);
            Controls.Add(bSkanuj);
            Controls.Add(cbCzescObrazu);
            Controls.Add(cbFormat);
            Controls.Add(cbTryb);
            Controls.Add(cbUrzadzenie);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)obraz).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private ComboBox cbUrzadzenie;
        private ComboBox cbTryb;
        private ComboBox cbFormat;
        private ComboBox cbCzescObrazu;
        private Button bSkanuj;
        private Button bZapisz;
        private Button bPolacz;
        private PictureBox obraz;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
