namespace KP
{
    partial class SolutionGraphForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSolution;

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
            this.chartSolution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartSolution)).BeginInit();
            this.SuspendLayout();
            // 
            // chartSolution
            // 
            this.chartSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSolution.Location = new System.Drawing.Point(0, 0);
            this.chartSolution.Name = "chartSolution";
            this.chartSolution.Size = new System.Drawing.Size(800, 450);
            this.chartSolution.TabIndex = 0;
            this.chartSolution.Text = "chartSolution";
            // 
            // SolutionGraphForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartSolution);
            this.Name = "SolutionGraphForm";
            this.Text = "График решения системы уравнений";
            ((System.ComponentModel.ISupportInitialize)(this.chartSolution)).EndInit();
            this.ResumeLayout(false);
        }
    }
}