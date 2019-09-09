namespace UniqueValueRenderer
{
    partial class FrmUniqueValueRenderer
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxLayers = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbxFields = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbxLayers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFields.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxLayers
            // 
            this.cbxLayers.Location = new System.Drawing.Point(88, 33);
            this.cbxLayers.Name = "cbxLayers";
            this.cbxLayers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxLayers.Size = new System.Drawing.Size(268, 20);
            this.cbxLayers.TabIndex = 0;
            this.cbxLayers.SelectedIndexChanged += new System.EventHandler(this.cbxLayers_SelectedIndexChanged);
            // 
            // cbxFields
            // 
            this.cbxFields.Location = new System.Drawing.Point(88, 78);
            this.cbxFields.Name = "cbxFields";
            this.cbxFields.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxFields.Size = new System.Drawing.Size(268, 20);
            this.cbxFields.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(36, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "图层：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(36, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "字段：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(281, 116);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(184, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmUniqueValueRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 161);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cbxFields);
            this.Controls.Add(this.cbxLayers);
            this.Name = "FrmUniqueValueRenderer";
            this.Text = "唯一值符号化";
            this.Load += new System.EventHandler(this.FrmUniqueValueRenderer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbxLayers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFields.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbxLayers;
        private DevExpress.XtraEditors.ComboBoxEdit cbxFields;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}