using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KP
{
    public partial class SolutionGraphForm : Form
    {
        public SolutionGraphForm()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            chartSolution.Series.Clear();
            chartSolution.ChartAreas.Clear();
            chartSolution.Legends.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartSolution.ChartAreas.Add(chartArea);

            Legend legend = new Legend("MainLegend");
            chartSolution.Legends.Add(legend);
        }

        public void UpdateSolutionChart(List<List<Tuple<double, double>>> solutionData)
        {
            try
            {
                chartSolution.Series.Clear();

                if (solutionData == null || solutionData.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения на графике.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                double minX = double.MaxValue, maxX = double.MinValue;
                double minY = double.MaxValue, maxY = double.MinValue;

                const double maxAllowedValue = 1E28; 
                                                 

                var filteredSolutionData = new List<List<Tuple<double, double>>>();

                foreach (var seriesData in solutionData)
                {
                    var filteredSeries = new List<Tuple<double, double>>();

                    foreach (var dataPoint in seriesData)
                    {
                        double x = dataPoint.Item1;
                        double y = dataPoint.Item2;

                        if (double.IsNaN(x) || double.IsInfinity(x) || double.IsNaN(y) || double.IsInfinity(y))
                            continue;

                        if (y > maxAllowedValue)
                            y = maxAllowedValue;
                        else if (y < -maxAllowedValue)
                            y = -maxAllowedValue;

                        if (x > maxAllowedValue)
                            x = maxAllowedValue;
                        else if (x < -maxAllowedValue)
                            x = -maxAllowedValue;

                        filteredSeries.Add(new Tuple<double, double>(x, y));

                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }

                    if (filteredSeries.Count > 0)
                    {
                        filteredSolutionData.Add(filteredSeries);
                    }
                }

                if (filteredSolutionData.Count == 0)
                {
                    MessageBox.Show("Все точки данных содержат недопустимые значения или выходят за пределы допустимого диапазона.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                minX = Math.Floor(minX);
                maxX = Math.Ceiling(maxX);
                minY = Math.Floor(minY);
                maxY = Math.Ceiling(maxY);

                if (minX == maxX)
                {
                    minX -= 1;
                    maxX += 1;
                }

                if (minY == maxY)
                {
                    minY -= 1;
                    maxY += 1;
                }

                const double epsilon = 1E-10;

                if (Math.Abs(minX) < epsilon)
                    minX = 0;
                if (Math.Abs(minY) < epsilon)
                    minY = 0;

                var chartAreaMain = chartSolution.ChartAreas["MainArea"];
                chartAreaMain.AxisX.Minimum = minX;
                chartAreaMain.AxisX.Maximum = maxX;
                chartAreaMain.AxisY.Minimum = minY;
                chartAreaMain.AxisY.Maximum = maxY;

                int maxLabels = 10;
                chartAreaMain.AxisX.Interval = CalculateOptimalInterval(minX, maxX, maxLabels);
                chartAreaMain.AxisY.Interval = CalculateOptimalInterval(minY, maxY, maxLabels);

                chartAreaMain.AxisX.LabelStyle.Format = chartAreaMain.AxisX.Interval % 1 == 0 ? "0" : "0.##";
                chartAreaMain.AxisY.LabelStyle.Format = chartAreaMain.AxisY.Interval % 1 == 0 ? "0" : "0.##";

                chartAreaMain.AxisX.CustomLabels.Clear();
                chartAreaMain.AxisY.CustomLabels.Clear();

                AddCustomLabels(chartAreaMain.AxisX, minX, maxX, chartAreaMain.AxisX.Interval, epsilon);
                AddCustomLabels(chartAreaMain.AxisY, minY, maxY, chartAreaMain.AxisY.Interval, epsilon);

                for (int i = 0; i < filteredSolutionData.Count; i++)
                {
                    Series series = new Series($"y{i + 1}")
                    {
                        ChartType = SeriesChartType.Line,
                        ChartArea = "MainArea",
                        Legend = "MainLegend",
                        BorderWidth = 2
                    };

                    int pointCount = filteredSolutionData[i].Count;
                    int step = 1;
                    if (pointCount > 1000)
                    {
                        step = pointCount / 1000;
                        if (step < 1) step = 1;
                    }

                    for (int j = 0; j < pointCount; j += step)
                    {
                        var dataPoint = filteredSolutionData[i][j];
                        series.Points.AddXY(dataPoint.Item1, dataPoint.Item2);
                    }

                    chartSolution.Series.Add(series);
                }

                chartAreaMain.AxisX.Title = "t";
                chartAreaMain.AxisY.Title = "y";
                chartSolution.Legends["MainLegend"].Docking = Docking.Top;
                chartSolution.Legends["MainLegend"].Alignment = StringAlignment.Center;
            }
            catch (OverflowException ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show("Произошло переполнение при обработке данных для графика. Пожалуйста, проверьте данные на наличие слишком больших значений.", "Ошибка переполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show("Диапазон значений осей некорректен. Пожалуйста, проверьте данные.", "Ошибка осей", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при обновлении графика решения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double CalculateOptimalInterval(double min, double max, int maxLabels)
        {
            double range = Math.Abs(max - min);
            if (range == 0)
                return 1;

            double rawInterval = range / maxLabels;
            double magnitude = Math.Pow(10, Math.Floor(Math.Log10(rawInterval)));
            double residual = rawInterval / magnitude;

            double niceInterval;

            if (residual > 5)
                niceInterval = 10 * magnitude;
            else if (residual > 2)
                niceInterval = 5 * magnitude;
            else if (residual > 1)
                niceInterval = 2 * magnitude;
            else
                niceInterval = magnitude;

            return niceInterval;
        }

        private void AddCustomLabels(Axis axis, double min, double max, double interval, double epsilon)
        {
            for (double i = min; i <= max; i += interval)
            {
                string labelText = i.ToString("0.##");

                if (Math.Abs(i) < epsilon)
                    labelText = "0";

                double start = i - interval / 2;
                double end = i + interval / 2;

                if (start < min)
                    start = min;
                if (end > max)
                    end = max;

                axis.CustomLabels.Add(new CustomLabel(start, end, labelText, 0, LabelMarkStyle.None));
            }
        }

    }
}
