using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KP
{
    public partial class DependencyGraphForm : Form
    {
       
        private readonly List<string> modelNames = new List<string>()
        {
            "Маятник",
            "Популяционная динамика",
            "Электрическая цепь RLC",
            "Эпидемия",
            "Вращение спутника"
        };

        public DependencyGraphForm()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chartDependency.Series.Clear();
            chartDependency.ChartAreas.Clear();
            chartDependency.Legends.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartDependency.ChartAreas.Add(chartArea);

            Legend legend = new Legend("MainLegend");
            chartDependency.Legends.Add(legend);
        }

        public void UpdateChart(List<Tuple<int, double>> dependencyData)
        {
            try
            {
                chartDependency.Series.Clear();

                Series series = new Series("Время вычисления");
                series.ChartType = SeriesChartType.Column; // Гистограмма
                series.ChartArea = "MainArea";
                series.Legend = "MainLegend";
                series.BorderWidth = 2;

                for (int i = 0; i < dependencyData.Count; i++)
                {
                    string modelName = (i < modelNames.Count) ? modelNames[i] : $"Система {dependencyData[i].Item1}";
                    double time = dependencyData[i].Item2;
                    series.Points.AddXY(modelName, time);
                }

                chartDependency.Series.Add(series);
                chartDependency.ChartAreas["MainArea"].AxisX.Title = "Модели";
                chartDependency.ChartAreas["MainArea"].AxisY.Title = "Время вычисления (с)";
                chartDependency.Legends["MainLegend"].Docking = Docking.Top;
                chartDependency.Legends["MainLegend"].Alignment = StringAlignment.Center;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при обновлении графика зависимости: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
