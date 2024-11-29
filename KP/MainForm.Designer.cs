namespace KP
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dataGridViewEquations;
        private System.Windows.Forms.DataGridViewTextBoxColumn Equation;
        private System.Windows.Forms.DataGridViewTextBoxColumn InitialCondition;
        private System.Windows.Forms.Button buttonAddEquation;
        private System.Windows.Forms.Button buttonRemoveEquation;
        private System.Windows.Forms.Label labelT0;
        private System.Windows.Forms.Label labelT1;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.TextBox textBoxT0;
        private System.Windows.Forms.TextBox textBoxT1;
        private System.Windows.Forms.TextBox textBoxH;
        private System.Windows.Forms.RadioButton radioButtonRK2;
        private System.Windows.Forms.RadioButton radioButtonRK4;
        private System.Windows.Forms.Button buttonCompute;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonShowDependencyGraph;
        private System.Windows.Forms.Button buttonShowSolutionGraph;
        private System.Windows.Forms.ProgressBar progressBarComputation;
        private System.Windows.Forms.Label labelComputationTime;
        private System.Windows.Forms.DataGridView dataGridViewIntermediateResults;
        private System.Windows.Forms.DataGridView dataGridViewFinalResults;

        private System.Windows.Forms.Button buttonExample1;
        private System.Windows.Forms.Button buttonExample2;
        private System.Windows.Forms.Button buttonExample3;
        private System.Windows.Forms.Button buttonExample4;
        private System.Windows.Forms.Button buttonExample5;

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
            dataGridViewEquations = new DataGridView();
            Equation = new DataGridViewTextBoxColumn();
            InitialCondition = new DataGridViewTextBoxColumn();
            buttonAddEquation = new Button();
            buttonRemoveEquation = new Button();
            labelT0 = new Label();
            labelT1 = new Label();
            labelH = new Label();
            textBoxT0 = new TextBox();
            textBoxT1 = new TextBox();
            textBoxH = new TextBox();
            radioButtonRK2 = new RadioButton();
            radioButtonRK4 = new RadioButton();
            buttonCompute = new Button();
            buttonClear = new Button();
            buttonSave = new Button();
            buttonShowDependencyGraph = new Button();
            buttonShowSolutionGraph = new Button();
            progressBarComputation = new ProgressBar();
            labelComputationTime = new Label();
            dataGridViewIntermediateResults = new DataGridView();
            dataGridViewFinalResults = new DataGridView();
            buttonExample1 = new Button();
            buttonExample2 = new Button();
            buttonExample3 = new Button();
            buttonExample4 = new Button();
            buttonExample5 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEquations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewIntermediateResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFinalResults).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewEquations
            // 
            dataGridViewEquations.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewEquations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEquations.Columns.AddRange(new DataGridViewColumn[] { Equation, InitialCondition });
            dataGridViewEquations.Location = new Point(12, 31);
            dataGridViewEquations.Name = "dataGridViewEquations";
            dataGridViewEquations.RowHeadersWidth = 62;
            dataGridViewEquations.Size = new Size(532, 241);
            dataGridViewEquations.TabIndex = 0;
            dataGridViewEquations.CellContentClick += dataGridViewEquations_CellContentClick;
            // 
            // Equation
            // 
            Equation.HeaderText = "Уравнение";
            Equation.MinimumWidth = 8;
            Equation.Name = "Equation";
            Equation.Width = 250;
            // 
            // InitialCondition
            // 
            InitialCondition.HeaderText = "Начальное условие";
            InitialCondition.MinimumWidth = 8;
            InitialCondition.Name = "InitialCondition";
            InitialCondition.Width = 200;
            // 
            // buttonAddEquation
            // 
            buttonAddEquation.Location = new Point(23, 278);
            buttonAddEquation.Name = "buttonAddEquation";
            buttonAddEquation.Size = new Size(203, 41);
            buttonAddEquation.TabIndex = 1;
            buttonAddEquation.Text = "Добавить уравнение";
            buttonAddEquation.UseVisualStyleBackColor = true;
            buttonAddEquation.Click += buttonAddEquation_Click;
            // 
            // buttonRemoveEquation
            // 
            buttonRemoveEquation.Location = new Point(232, 278);
            buttonRemoveEquation.Name = "buttonRemoveEquation";
            buttonRemoveEquation.Size = new Size(203, 41);
            buttonRemoveEquation.TabIndex = 2;
            buttonRemoveEquation.Text = "Удалить уравнение";
            buttonRemoveEquation.UseVisualStyleBackColor = true;
            buttonRemoveEquation.Click += buttonRemoveEquation_Click;
            // 
            // labelT0
            // 
            labelT0.AutoSize = true;
            labelT0.Location = new Point(474, 297);
            labelT0.Name = "labelT0";
            labelT0.Size = new Size(29, 25);
            labelT0.TabIndex = 3;
            labelT0.Text = "t₀:";
            // 
            // labelT1
            // 
            labelT1.AutoSize = true;
            labelT1.Location = new Point(474, 327);
            labelT1.Name = "labelT1";
            labelT1.Size = new Size(28, 25);
            labelT1.TabIndex = 5;
            labelT1.Text = "t₁:";
            // 
            // labelH
            // 
            labelH.AutoSize = true;
            labelH.Location = new Point(474, 357);
            labelH.Name = "labelH";
            labelH.Size = new Size(26, 25);
            labelH.TabIndex = 7;
            labelH.Text = "h:";
            // 
            // textBoxT0
            // 
            textBoxT0.Location = new Point(524, 294);
            textBoxT0.Name = "textBoxT0";
            textBoxT0.Size = new Size(100, 31);
            textBoxT0.TabIndex = 4;
            // 
            // textBoxT1
            // 
            textBoxT1.Location = new Point(524, 324);
            textBoxT1.Name = "textBoxT1";
            textBoxT1.Size = new Size(100, 31);
            textBoxT1.TabIndex = 6;
            // 
            // textBoxH
            // 
            textBoxH.Location = new Point(524, 354);
            textBoxH.Name = "textBoxH";
            textBoxH.Size = new Size(100, 31);
            textBoxH.TabIndex = 8;
            // 
            // radioButtonRK2
            // 
            radioButtonRK2.AutoSize = true;
            radioButtonRK2.Checked = true;
            radioButtonRK2.Location = new Point(23, 328);
            radioButtonRK2.Name = "radioButtonRK2";
            radioButtonRK2.Size = new Size(307, 29);
            radioButtonRK2.TabIndex = 9;
            radioButtonRK2.TabStop = true;
            radioButtonRK2.Text = "Метод Рунге-Кутты 2-го порядка";
            radioButtonRK2.UseVisualStyleBackColor = true;
            // 
            // radioButtonRK4
            // 
            radioButtonRK4.AutoSize = true;
            radioButtonRK4.Location = new Point(23, 363);
            radioButtonRK4.Name = "radioButtonRK4";
            radioButtonRK4.Size = new Size(307, 29);
            radioButtonRK4.TabIndex = 10;
            radioButtonRK4.Text = "Метод Рунге-Кутты 4-го порядка";
            radioButtonRK4.UseVisualStyleBackColor = true;
            // 
            // buttonCompute
            // 
            buttonCompute.Location = new Point(29, 422);
            buttonCompute.Name = "buttonCompute";
            buttonCompute.Size = new Size(150, 40);
            buttonCompute.TabIndex = 11;
            buttonCompute.Text = "Вычислить";
            buttonCompute.UseVisualStyleBackColor = true;
            buttonCompute.Click += buttonCompute_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(183, 422);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(150, 40);
            buttonClear.TabIndex = 12;
            buttonClear.Text = "Очистить";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(337, 422);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(148, 40);
            buttonSave.TabIndex = 13;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonShowDependencyGraph
            // 
            buttonShowDependencyGraph.Location = new Point(731, 422);
            buttonShowDependencyGraph.Name = "buttonShowDependencyGraph";
            buttonShowDependencyGraph.Size = new Size(265, 40);
            buttonShowDependencyGraph.TabIndex = 14;
            buttonShowDependencyGraph.Text = "График зависимости";
            buttonShowDependencyGraph.UseVisualStyleBackColor = true;
            buttonShowDependencyGraph.Click += buttonShowDependencyGraph_Click;
            // 
            // buttonShowSolutionGraph
            // 
            buttonShowSolutionGraph.Location = new Point(491, 422);
            buttonShowSolutionGraph.Name = "buttonShowSolutionGraph";
            buttonShowSolutionGraph.Size = new Size(234, 40);
            buttonShowSolutionGraph.TabIndex = 15;
            buttonShowSolutionGraph.Text = "График решения системы";
            buttonShowSolutionGraph.UseVisualStyleBackColor = true;
            buttonShowSolutionGraph.Click += buttonShowSolutionGraph_Click;
            // 
            // progressBarComputation
            // 
            progressBarComputation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarComputation.Location = new Point(12, 954);
            progressBarComputation.Name = "progressBarComputation";
            progressBarComputation.Size = new Size(1136, 29);
            progressBarComputation.TabIndex = 16;
            // 
            // labelComputationTime
            // 
            labelComputationTime.AutoSize = true;
            labelComputationTime.Location = new Point(12, 926);
            labelComputationTime.Name = "labelComputationTime";
            labelComputationTime.Size = new Size(199, 25);
            labelComputationTime.TabIndex = 17;
            labelComputationTime.Text = "Время вычисления: 0 с";
            // 
            // dataGridViewIntermediateResults
            // 
            dataGridViewIntermediateResults.AllowUserToAddRows = false;
            dataGridViewIntermediateResults.AllowUserToDeleteRows = false;
            dataGridViewIntermediateResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewIntermediateResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewIntermediateResults.Location = new Point(12, 501);
            dataGridViewIntermediateResults.Name = "dataGridViewIntermediateResults";
            dataGridViewIntermediateResults.ReadOnly = true;
            dataGridViewIntermediateResults.RowHeadersWidth = 62;
            dataGridViewIntermediateResults.Size = new Size(1136, 395);
            dataGridViewIntermediateResults.TabIndex = 18;
            // 
            // dataGridViewFinalResults
            // 
            dataGridViewFinalResults.AllowUserToAddRows = false;
            dataGridViewFinalResults.AllowUserToDeleteRows = false;
            dataGridViewFinalResults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewFinalResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFinalResults.Location = new Point(550, 31);
            dataGridViewFinalResults.Name = "dataGridViewFinalResults";
            dataGridViewFinalResults.ReadOnly = true;
            dataGridViewFinalResults.RowHeadersWidth = 62;
            dataGridViewFinalResults.Size = new Size(458, 241);
            dataGridViewFinalResults.TabIndex = 19;
            // 
            // buttonExample1
            // 
            buttonExample1.Location = new Point(1017, 31);
            buttonExample1.Name = "buttonExample1";
            buttonExample1.Size = new Size(119, 53);
            buttonExample1.TabIndex = 20;
            buttonExample1.Text = "Пример 1";
            buttonExample1.UseVisualStyleBackColor = true;
            buttonExample1.Click += buttonExample1_Click;
            // 
            // buttonExample2
            // 
            buttonExample2.Location = new Point(1017, 90);
            buttonExample2.Name = "buttonExample2";
            buttonExample2.Size = new Size(119, 53);
            buttonExample2.TabIndex = 21;
            buttonExample2.Text = "Пример 2";
            buttonExample2.UseVisualStyleBackColor = true;
            buttonExample2.Click += buttonExample2_Click;
            // 
            // buttonExample3
            // 
            buttonExample3.Location = new Point(1017, 149);
            buttonExample3.Name = "buttonExample3";
            buttonExample3.Size = new Size(119, 53);
            buttonExample3.TabIndex = 22;
            buttonExample3.Text = "Пример 3";
            buttonExample3.UseVisualStyleBackColor = true;
            buttonExample3.Click += buttonExample3_Click;
            // 
            // buttonExample4
            // 
            buttonExample4.Location = new Point(1014, 208);
            buttonExample4.Name = "buttonExample4";
            buttonExample4.Size = new Size(122, 55);
            buttonExample4.TabIndex = 23;
            buttonExample4.Text = "Пример 4";
            buttonExample4.UseVisualStyleBackColor = true;
            buttonExample4.Click += buttonExample4_Click;
            // 
            // buttonExample5
            // 
            buttonExample5.Location = new Point(1014, 272);
            buttonExample5.Name = "buttonExample5";
            buttonExample5.Size = new Size(122, 53);
            buttonExample5.TabIndex = 24;
            buttonExample5.Text = "Пример 5";
            buttonExample5.UseVisualStyleBackColor = true;
            buttonExample5.Click += buttonExample5_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Cursor = Cursors.SizeAll;
            label1.Location = new Point(12, 473);
            label1.Name = "label1";
            label1.Size = new Size(251, 25);
            label1.TabIndex = 17;
            label1.Text = "Промежуточные результаты:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(550, 3);
            label2.Name = "label2";
            label2.Size = new Size(93, 25);
            label2.TabIndex = 17;
            label2.Text = "Результат:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 3);
            label3.Name = "label3";
            label3.Size = new Size(176, 25);
            label3.TabIndex = 17;
            label3.Text = "Система уравнений:";
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(1122, 207);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(8, 8);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // MainForm
            // 
            ClientSize = new Size(1160, 990);
            Controls.Add(groupBox1);
            Controls.Add(buttonExample5);
            Controls.Add(buttonExample4);
            Controls.Add(buttonExample3);
            Controls.Add(buttonExample2);
            Controls.Add(buttonExample1);
            Controls.Add(dataGridViewFinalResults);
            Controls.Add(dataGridViewIntermediateResults);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(labelComputationTime);
            Controls.Add(progressBarComputation);
            Controls.Add(buttonShowSolutionGraph);
            Controls.Add(buttonShowDependencyGraph);
            Controls.Add(buttonSave);
            Controls.Add(buttonClear);
            Controls.Add(buttonCompute);
            Controls.Add(radioButtonRK4);
            Controls.Add(radioButtonRK2);
            Controls.Add(textBoxH);
            Controls.Add(labelH);
            Controls.Add(textBoxT1);
            Controls.Add(labelT1);
            Controls.Add(textBoxT0);
            Controls.Add(labelT0);
            Controls.Add(buttonRemoveEquation);
            Controls.Add(buttonAddEquation);
            Controls.Add(dataGridViewEquations);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Решение системы ОДУ методом Рунге-Кутты";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewEquations).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewIntermediateResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFinalResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private Label label3;
        private GroupBox groupBox1;
    }
}
