using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KP
{
    public partial class MainForm : Form
    {
        private RungeKuttaSolver solver;
        private List<Equation> equations;
        private const int MaxEquations = 20;
        private const int MaxEquationLength = 200;

        public MainForm()
        {
            InitializeComponent();
            solver = new RungeKuttaSolver();
            equations = new List<Equation>();
        }

        private void buttonAddEquation_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewEquations.Rows.Add();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при добавлении уравнения: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveEquation_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEquations.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridViewEquations.SelectedRows)
                    {
                        dataGridViewEquations.Rows.Remove(row);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите уравнение для удаления.", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при удалении уравнения: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void buttonCompute_Click(object sender, EventArgs e)
        {
            ToggleControls(false);
            await StartComputationAsync();
            ToggleControls(true);
        }


        private void ToggleControls(bool enable)
        {
            buttonAddEquation.Enabled = enable;
            buttonRemoveEquation.Enabled = enable;
            buttonCompute.Enabled = enable;
            textBoxT0.Enabled = enable;
            textBoxT1.Enabled = enable;
            textBoxH.Enabled = enable;
            buttonShowDependencyGraph.Enabled = enable;
            buttonShowSolutionGraph.Enabled = enable;
            buttonSave.Enabled = enable;
            buttonClear.Enabled = enable;
        }

        private async Task StartComputationAsync()
        {
            try
            {
                progressBarComputation.Minimum = 0;
                progressBarComputation.Maximum = 100;
                progressBarComputation.Value = 0;
                labelComputationTime.Text = "Время вычисления: 0 с";

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                solver.Results.Clear();
                solver.TimeSteps.Clear();
                solver.KValues.Clear();
                equations.Clear();

                if (!CollectEquations())
                {
                    return;
                }

                if (!double.TryParse(textBoxT0.Text, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double t0))
                {
                    MessageBox.Show("Некорректное значение t₀.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxT0.Focus();
                    return;
                }

                if (!double.TryParse(textBoxT1.Text, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double t1))
                {
                    MessageBox.Show("Некорректное значение t₁.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxT1.Focus();
                    return;
                }

                if (!double.TryParse(textBoxH.Text, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out double h))
                {
                    MessageBox.Show("Некорректное значение h.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxH.Focus();
                    return;
                }

                if (h <= 0)
                {
                    MessageBox.Show("Шаг h должен быть положительным числом.", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    textBoxH.Focus();
                    return;
                }

                if (t1 <= t0)
                {
                    MessageBox.Show("t1 не может быть больше t0.", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    textBoxT0.Focus();
                    return;
                }

                if ((t1 - t0) / h > 100001)
                {
                    MessageBox.Show(
                        "Шаги вычислений не могут превышать 100001. Пожалуйста, выберите значения для t₀, t₁ и h, чтобы количество шагов оставалось в пределах этого лимита.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxT0.Focus();
                    return;
                }

                int numEquations = equations.Count;
                Func<double, double[], double>[] functions = new Func<double, double[], double>[numEquations];
                double[] y0 = new double[numEquations];

                // Парсинг уравнений
                for (int i = 0; i < numEquations; i++)
                {
                    Equation eq = equations[i];
                    try
                    {
                        functions[i] = EquationParser.ParseEquation(eq.EquationText, numEquations);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Некорректное уравнение в строке {i + 1}: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridViewEquations.CurrentCell = dataGridViewEquations.Rows[i].Cells["Equation"];
                        dataGridViewEquations.BeginEdit(true);
                        return;
                    }

                    y0[i] = eq.InitialCondition;
                }

                var progress = new Progress<int>(value => { progressBarComputation.Value = value; });

                if (radioButtonRK2.Checked)
                {
                    await Task.Run(() => solver.SolveRK2(functions, y0, t0, t1, h, progress));
                }
                else
                {
                    await Task.Run(() => solver.SolveRK4(functions, y0, t0, t1, h, progress));
                }

                stopwatch.Stop();
                labelComputationTime.Text = $"Время вычисления: {stopwatch.Elapsed.TotalSeconds:F2} с";

                MessageBox.Show("Вычисления завершены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DisplayIntermediateResults();
                DisplayFinalResults();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка во время вычислений: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleControls(true);
            }
        }

        private bool CollectEquations()
        {
            try
            {
                equations.Clear();

                if (dataGridViewEquations.Rows.Count - 1 > MaxEquations)
                {
                    MessageBox.Show($"Максимальное количество уравнений: {MaxEquations}.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                foreach (DataGridViewRow row in dataGridViewEquations.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    string equationText = row.Cells["Equation"].Value?.ToString();
                    string initialConditionText = row.Cells["InitialCondition"].Value?.ToString();

                    if (string.IsNullOrWhiteSpace(equationText))
                    {
                        MessageBox.Show($"Уравнение в строке {row.Index + 1} не задано.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridViewEquations.CurrentCell = row.Cells["Equation"];
                        dataGridViewEquations.BeginEdit(true);
                        return false;
                    }

                    if (equationText.Length > MaxEquationLength)
                    {
                        MessageBox.Show(
                            $"Уравнение в строке {row.Index + 1} превышает максимальную длину {MaxEquationLength} символов.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridViewEquations.CurrentCell = row.Cells["Equation"];
                        dataGridViewEquations.BeginEdit(true);
                        return false;
                    }

                    if (!double.TryParse(initialConditionText, System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture, out double initialCondition))
                    {
                        MessageBox.Show($"Начальное условие в строке {row.Index + 1} некорректно.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridViewEquations.CurrentCell = row.Cells["InitialCondition"];
                        dataGridViewEquations.BeginEdit(true);
                        return false;
                    }

                    equations.Add(new Equation(equationText, initialCondition));
                }

                if (equations.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы одно уравнение.", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при сборе уравнений: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private void DisplayIntermediateResults()
        {
            try
            {
                dataGridViewIntermediateResults.Columns.Clear();
                dataGridViewIntermediateResults.Rows.Clear();

                int numEquations = equations.Count;

                dataGridViewIntermediateResults.Columns.Add("Step", "Step");
                dataGridViewIntermediateResults.Columns.Add("t", "t");

                for (int i = 0; i < numEquations; i++)
                {
                    dataGridViewIntermediateResults.Columns.Add($"y{i + 1}", $"y{i + 1}");
                }

                int kCount = solver.KValues.Count > 0 ? solver.KValues[0].Length : 0;
                for (int kIndex = 0; kIndex < kCount; kIndex++)
                {
                    for (int i = 0; i < numEquations; i++)
                    {
                        dataGridViewIntermediateResults.Columns.Add($"k{kIndex + 1}_y{i + 1}",
                            $"k{kIndex + 1}_y{i + 1}");
                    }
                }

                for (int i = 0; i < solver.TimeSteps.Count; i++)
                {
                    var row = new List<string>();
                    row.Add((i + 1).ToString());
                    row.Add(solver.TimeSteps[i].ToString("F5"));

                    for (int j = 0; j < numEquations; j++)
                    {
                        row.Add(solver.Results[i][j].ToString("F5"));
                    }

                    var kValues = solver.KValues[i];
                    for (int kIndex = 0; kIndex < kValues.Length; kIndex++)
                    {
                        for (int j = 0; j < numEquations; j++)
                        {
                            row.Add(kValues[kIndex][j].ToString("F5"));
                        }
                    }

                    dataGridViewIntermediateResults.Rows.Add(row.ToArray());
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при отображении промежуточных результатов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayFinalResults()
        {
            try
            {
                dataGridViewFinalResults.Columns.Clear();
                dataGridViewFinalResults.Rows.Clear();

                int numEquations = equations.Count;

                dataGridViewFinalResults.Columns.Add("Variable", "Переменная");
                dataGridViewFinalResults.Columns.Add("Value", "Значение");

                var lastIndex = solver.Results.Count - 1;

                for (int i = 0; i < numEquations; i++)
                {
                    string variableName = $"y{i + 1}";
                    string value = solver.Results[lastIndex][i].ToString("F5");

                    dataGridViewFinalResults.Rows.Add(variableName, value);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при отображении итоговых результатов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewEquations.Rows.Clear();
                dataGridViewIntermediateResults.Columns.Clear();
                dataGridViewIntermediateResults.Rows.Clear();
                dataGridViewFinalResults.Columns.Clear();
                dataGridViewFinalResults.Rows.Clear();
                textBoxT0.Clear();
                textBoxT1.Clear();
                textBoxH.Clear();
                equations.Clear();
                solver.Results.Clear();
                solver.TimeSteps.Clear();
                solver.KValues.Clear();
                progressBarComputation.Value = 0;
                labelComputationTime.Text = "Время вычисления: 0 с";
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при очистке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewIntermediateResults.Rows.Count > 0)
                {
                    FileManager.SaveResults(dataGridViewIntermediateResults);
                }
                else
                {
                    MessageBox.Show("Нет данных для сохранения.", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при сохранении результатов: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonShowDependencyGraph_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleControls(false);
                var dependencyData = new List<Tuple<int, double>>();

                var examples = GetAllExamples();

                foreach (var example in examples)
                {
                    try
                    {
                        int numEquations = example.Equations.Length;
                        Func<double, double[], double>[] functions = new Func<double, double[], double>[numEquations];
                        double[] y0 = new double[numEquations];

                        for (int i = 0; i < numEquations; i++)
                        {
                            functions[i] = EquationParser.ParseEquation(example.Equations[i], numEquations);
                            y0[i] = example.InitialConditions[i];
                        }

                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();

                        RungeKuttaSolver tempSolver = new RungeKuttaSolver();

                        if (radioButtonRK2.Checked)
                        {
                            tempSolver.SolveRK2(functions, y0, example.T0, example.T1, example.H);
                        }
                        else
                        {
                            tempSolver.SolveRK4(functions, y0, example.T0, example.T1, example.H);
                        }

                        stopwatch.Stop();
                        double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

                        dependencyData.Add(new Tuple<int, double>(numEquations, elapsedSeconds));
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(ex);
                        MessageBox.Show($"Ошибка при обработке примера: {ex.Message}", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }


                DependencyGraphForm dependencyGraphForm = new DependencyGraphForm();
                dependencyGraphForm.UpdateChart(dependencyData);
                dependencyGraphForm.Show();
                ToggleControls(true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при построении графика зависимости: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonShowSolutionGraph_Click(object sender, EventArgs e)
        {
            try
            {
                if (solver.Results.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения. Пожалуйста, выполните вычисления.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SolutionGraphForm solutionGraphForm = new SolutionGraphForm();
                var solutionData = GetSolutionDataFromSolver();
                solutionGraphForm.UpdateSolutionChart(solutionData);
                solutionGraphForm.Show();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при открытии графика решения системы уравнений: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<List<Tuple<double, double>>> GetSolutionDataFromSolver()
        {
            try
            {
                int numEquations = equations.Count;
                var solutionData = new List<List<Tuple<double, double>>>();

                for (int i = 0; i < numEquations; i++)
                {
                    var variableData = new List<Tuple<double, double>>();
                    for (int j = 0; j < solver.TimeSteps.Count; j++)
                    {
                        variableData.Add(new Tuple<double, double>(solver.TimeSteps[j], solver.Results[j][i]));
                    }

                    solutionData.Add(variableData);
                }

                return solutionData;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при подготовке данных для графика решения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<List<Tuple<double, double>>>();
            }
        }

        private void buttonExample1_Click(object sender, EventArgs e)
        {
            LoadExample(1);
        }

        private void buttonExample2_Click(object sender, EventArgs e)
        {
            LoadExample(2);
        }

        private void buttonExample3_Click(object sender, EventArgs e)
        {
            LoadExample(3);
        }

        private void buttonExample4_Click(object sender, EventArgs e)
        {
            LoadExample(4);
        }

        private void buttonExample5_Click(object sender, EventArgs e)
        {
            LoadExample(5);
        }

        private void LoadExample(int exampleNumber)
        {
            try
            {
                dataGridViewEquations.Rows.Clear();

                if (exampleNumber == 1)
                {
                    // Пример 1: Маятник
                    dataGridViewEquations.Rows.Add("y2", "0"); // dy1/dt = y2
                    dataGridViewEquations.Rows.Add("-y1", "1"); // dy2/dt = -y1
                    textBoxT0.Text = "0";
                    textBoxT1.Text = "10";
                    textBoxH.Text = "0.1";
                }
                else if (exampleNumber == 2)
                {
                    // Пример 2: Популяционная динамика
                    dataGridViewEquations.Rows.Add("2 * y1 - 1.2 * y1 * y2", "10"); // dy1/dt = 2 * y1 - 1.2 * y1 * y2
                    dataGridViewEquations.Rows.Add("-y2 + 0.9 * y1 * y2", "5"); // dy2/dt = -y2 + 0.9 * y1 * y2
                    textBoxT0.Text = "0";
                    textBoxT1.Text = "50";
                    textBoxH.Text = "0.05";
                }
                else if (exampleNumber == 3)
                {
                    // Пример 3: Электрическая цепь RLC
                    dataGridViewEquations.Rows.Add("y2", "0"); // dy1/dt = y2 (ток)
                    dataGridViewEquations.Rows.Add("-0.5 * y2 - 10 * y1 + 20 * sin(0.5 * t)",
                        "1"); // dy2/dt = напряжение в цепи
                    textBoxT0.Text = "0";
                    textBoxT1.Text = "30";
                    textBoxH.Text = "0.01";
                }
                else if (exampleNumber == 4)
                {
                    // Пример 4: Эпидемия
                    dataGridViewEquations.Rows.Add("-0.3 * y1 * y2", "0.99"); // dS/dt = -0.3 * S * I
                    dataGridViewEquations.Rows.Add("0.3 * y1 * y2 - 0.1 * y2", "0.01"); // dI/dt = 0.3 * S * I - 0.1 * I
                    dataGridViewEquations.Rows.Add("0.1 * y2", "0"); // dR/dt = 0.1 * I
                    textBoxT0.Text = "0";
                    textBoxT1.Text = "100";
                    textBoxH.Text = "0.01";
                }
                else if (exampleNumber == 5)
                {
                    // Пример 4: Спутник
                    dataGridViewEquations.Rows.Add("((3.0 - 4.0) * y2 * y3) / 2.0", "0.1"); // d(ω1)/dt = ((I2 - I3) * ω2 * ω3) / I1
                    dataGridViewEquations.Rows.Add("((4.0 - 2.0) * y3 * y1) / 3.0", "0.2"); // d(ω2)/dt = ((I3 - I1) * ω3 * ω1) / I2
                    dataGridViewEquations.Rows.Add("((2.0 - 3.0) * y1 * y2) / 4.0", "0.3"); // d(ω3)/dt = ((I1 - I2) * ω1 * ω2) / I3
                    textBoxT0.Text = "0";
                    textBoxT1.Text = "50";
                    textBoxH.Text = "0.01";
                }
                else
                {
                    MessageBox.Show($"Пример с номером {exampleNumber} не найден.", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                MessageBox.Show($"Ошибка при загрузке примера {exampleNumber}: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ExampleSystem> GetAllExamples()
        {
            var examples = new List<ExampleSystem>
            {
                // Пример 1: Маятник (простая гармоническая система)
                new ExampleSystem
                {
                    Equations = new string[] { "y2", "-y1" },
                    InitialConditions = new double[] { 0, 1 },
                    T0 = 0,
                    T1 = 10,
                    H = 0.1
                },

                // Пример 2: Популяционная динамика
                new ExampleSystem
                {
                    Equations = new string[] { "2 * y1 - 1.2 * y1 * y2", "-y2 + 0.9 * y1 * y2" },
                    InitialConditions = new double[] { 10, 5 },
                    T0 = 0,
                    T1 = 50,
                    H = 0.05
                },

                // Пример 3: Электрическая цепь RLC
                new ExampleSystem
                {
                    Equations = new string[] { "y2", "-0.5 * y2 - 10 * y1 + 20 * sin(0.5 * t)" },
                    InitialConditions = new double[] { 0, 1 },
                    T0 = 0,
                    T1 = 30,
                    H = 0.01
                },

                // Пример 4: Эпидемия
                new ExampleSystem
                {
                    Equations = new string[] { "-0.3 * y1 * y2", "0.3 * y1 * y2 - 0.1 * y2", "0.1 * y2" },
                    InitialConditions = new double[] { 0.99, 0.01, 0 },
                    T0 = 0,
                    T1 = 100,
                    H = 0.01
                },

                // Пример 5: Спутник
                new ExampleSystem
                {
                    Equations = new string[]{"((3.0 - 4.0) * y2 * y3) / 2.0",  // d(ω1)/dt = ((I2 - I3) * ω2 * ω3) / I1
                                             "((4.0 - 2.0) * y3 * y1) / 3.0",  // d(ω2)/dt = ((I3 - I1) * ω3 * ω1) / I2
                                             "((2.0 - 3.0) * y1 * y2) / 4.0"},  // d(ω3)/dt = ((I1 - I2) * ω1 * ω2) / I3
                                                                          
                    InitialConditions = new double[] { 0.1, 0.2, 0.3 },
                    T0 = 0, 
                    T1 = 50,
                    H = 0.05
                }
            };

            return examples;
        }


        private void dataGridViewEquations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}