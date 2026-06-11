namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.regionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pyroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hydroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.electroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cryoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dendroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espadaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mandobleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalizadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lanzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mondstadtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBox1);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1506, 694);
            this.splitContainer1.SplitterDistance = 502;
            this.splitContainer1.TabIndex = 0;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(514, 79);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 94);
            this.checkedListBox1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.ado_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1000, 694);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regionToolStripMenuItem,
            this.mondstadtToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip2.Size = new System.Drawing.Size(137, 48);
            // 
            // regionToolStripMenuItem
            // 
            this.regionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.claseToolStripMenuItem,
            this.espadaToolStripMenuItem,
            this.mandobleToolStripMenuItem,
            this.arcoToolStripMenuItem,
            this.catalizadorToolStripMenuItem,
            this.lanzaToolStripMenuItem});
            this.regionToolStripMenuItem.Name = "regionToolStripMenuItem";
            this.regionToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.regionToolStripMenuItem.Text = "Calificacion";
            this.regionToolStripMenuItem.Click += new System.EventHandler(this.regionToolStripMenuItem_Click);
            // 
            // claseToolStripMenuItem
            // 
            this.claseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elementoToolStripMenuItem,
            this.pyroToolStripMenuItem,
            this.hydroToolStripMenuItem,
            this.electroToolStripMenuItem,
            this.cryoToolStripMenuItem,
            this.anemoToolStripMenuItem,
            this.geoToolStripMenuItem,
            this.dendroToolStripMenuItem});
            this.claseToolStripMenuItem.Name = "claseToolStripMenuItem";
            this.claseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.claseToolStripMenuItem.Text = "Clase";
            // 
            // elementoToolStripMenuItem
            // 
            this.elementoToolStripMenuItem.Name = "elementoToolStripMenuItem";
            this.elementoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.elementoToolStripMenuItem.Text = "Elemento";
            // 
            // pyroToolStripMenuItem
            // 
            this.pyroToolStripMenuItem.Name = "pyroToolStripMenuItem";
            this.pyroToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pyroToolStripMenuItem.Text = "Pyro";
            // 
            // hydroToolStripMenuItem
            // 
            this.hydroToolStripMenuItem.Name = "hydroToolStripMenuItem";
            this.hydroToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hydroToolStripMenuItem.Text = "Hydro";
            // 
            // electroToolStripMenuItem
            // 
            this.electroToolStripMenuItem.Name = "electroToolStripMenuItem";
            this.electroToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.electroToolStripMenuItem.Text = "Electro";
            // 
            // cryoToolStripMenuItem
            // 
            this.cryoToolStripMenuItem.Name = "cryoToolStripMenuItem";
            this.cryoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cryoToolStripMenuItem.Text = "Cryo";
            // 
            // anemoToolStripMenuItem
            // 
            this.anemoToolStripMenuItem.Name = "anemoToolStripMenuItem";
            this.anemoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.anemoToolStripMenuItem.Text = "Anemo";
            // 
            // geoToolStripMenuItem
            // 
            this.geoToolStripMenuItem.Name = "geoToolStripMenuItem";
            this.geoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.geoToolStripMenuItem.Text = "Geo";
            // 
            // dendroToolStripMenuItem
            // 
            this.dendroToolStripMenuItem.Name = "dendroToolStripMenuItem";
            this.dendroToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dendroToolStripMenuItem.Text = "Dendro";
            // 
            // espadaToolStripMenuItem
            // 
            this.espadaToolStripMenuItem.Name = "espadaToolStripMenuItem";
            this.espadaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.espadaToolStripMenuItem.Text = "Espada";
            // 
            // mandobleToolStripMenuItem
            // 
            this.mandobleToolStripMenuItem.Name = "mandobleToolStripMenuItem";
            this.mandobleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mandobleToolStripMenuItem.Text = "Mandoble";
            // 
            // arcoToolStripMenuItem
            // 
            this.arcoToolStripMenuItem.Name = "arcoToolStripMenuItem";
            this.arcoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.arcoToolStripMenuItem.Text = "Arco";
            // 
            // catalizadorToolStripMenuItem
            // 
            this.catalizadorToolStripMenuItem.Name = "catalizadorToolStripMenuItem";
            this.catalizadorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.catalizadorToolStripMenuItem.Text = "Catalizador";
            // 
            // lanzaToolStripMenuItem
            // 
            this.lanzaToolStripMenuItem.Name = "lanzaToolStripMenuItem";
            this.lanzaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lanzaToolStripMenuItem.Text = "Lanza";
            this.lanzaToolStripMenuItem.Click += new System.EventHandler(this.lanzaToolStripMenuItem_Click);
            // 
            // mondstadtToolStripMenuItem
            // 
            this.mondstadtToolStripMenuItem.Name = "mondstadtToolStripMenuItem";
            this.mondstadtToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.mondstadtToolStripMenuItem.Text = "Mondstadt";
            this.mondstadtToolStripMenuItem.Click += new System.EventHandler(this.mondstadtToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1506, 694);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem regionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mondstadtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pyroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hydroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem electroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cryoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anemoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dendroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espadaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mandobleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catalizadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lanzaToolStripMenuItem;
    }
}

