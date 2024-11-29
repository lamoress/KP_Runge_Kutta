namespace KP
{
    partial class DependencyGraphForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDependency;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.chartDependency = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartDependency)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDependency
            // 
            this.chartDependency.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                                   | System.Windows.Forms.AnchorStyles.Left)
                                                                                   | System.Windows.Forms.AnchorStyles.Right)));
            this.chartDependency.Location = new System.Drawing.Point(12, 12);
            this.chartDependency.Name = "chartDependency";
            this.chartDependency.Size = new System.Drawing.Size(760, 537);
            this.chartDependency.TabIndex = 0;
            this.chartDependency.Text = "chart1";
            // 
            // DependencyGraphForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.chartDependency);
            this.Name = "DependencyGraphForm";
            this.Text = "График зависимости времени вычислений";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.chartDependency)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
