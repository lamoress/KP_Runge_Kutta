using System;
using System.Collections.Generic;

namespace KP
{
    public class RungeKuttaSolver
    {
        public List<double[]> Results { get; private set; }
        public List<double> TimeSteps { get; private set; }
        public List<double[][]> KValues { get; private set; } // Сохранение значений k

        public RungeKuttaSolver()
        {
            Results = new List<double[]>();
            TimeSteps = new List<double>();
            KValues = new List<double[][]>();
        }

        public void SolveRK4(Func<double, double[], double>[] functions, double[] y0, double t0, double t1, double h, IProgress<int> progress = null)
        {
            try
            {
                int numEquations = y0.Length;
                int steps = (int)Math.Ceiling((t1 - t0) / h) + 1;
                double[] y = new double[numEquations];
                y0.CopyTo(y, 0);
                double t = t0;

                KValues = new List<double[][]>();

                for (int step = 0; step < steps; step++)
                {
                    try
                    {
                        double[] k1 = new double[numEquations];
                        double[] k2 = new double[numEquations];
                        double[] k3 = new double[numEquations];
                        double[] k4 = new double[numEquations];

                        for (int i = 0; i < numEquations; i++)
                        {
                            k1[i] = functions[i](t, y);
                        }

                        double[] yTemp = new double[numEquations];
                        for (int i = 0; i < numEquations; i++)
                        {
                            yTemp[i] = y[i] + h * k1[i] / 2.0;
                        }

                        for (int i = 0; i < numEquations; i++)
                        {
                            k2[i] = functions[i](t + h / 2.0, yTemp);
                        }

                        for (int i = 0; i < numEquations; i++)
                        {
                            yTemp[i] = y[i] + h * k2[i] / 2.0;
                        }

                        for (int i = 0; i < numEquations; i++)
                        {
                            k3[i] = functions[i](t + h / 2.0, yTemp);
                        }

                        for (int i = 0; i < numEquations; i++)
                        {
                            yTemp[i] = y[i] + h * k3[i];
                        }

                        for (int i = 0; i < numEquations; i++)
                        {
                            k4[i] = functions[i](t + h, yTemp);
                        }

                        double[] yNew = new double[numEquations];
                        for (int i = 0; i < numEquations; i++)
                        {
                            yNew[i] = y[i] + h / 6.0 * (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]);
                        }

                        Results.Add((double[])y.Clone());
                        TimeSteps.Add(t);
                        KValues.Add(new double[][] { k1, k2, k3, k4 });

                        y = yNew;
                        t += h;
                        if (t > t1) t = t1;

                        progress?.Report((int)((double)(step + 1) / steps * 100));
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(ex);
                        throw new InvalidOperationException($"Ошибка на шаге {step + 1}: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                throw new Exception($"Ошибка в SolveRK4: {ex.Message}", ex);
            }
        }

        public void SolveRK2(Func<double, double[], double>[] functions, double[] y0, double t0, double t1, double h, IProgress<int> progress = null)
        {
            try
            {
                int numEquations = y0.Length;
                int steps = (int)Math.Ceiling((t1 - t0) / h) + 1;
                double[] y = new double[numEquations];
                y0.CopyTo(y, 0);
                double t = t0;

                KValues = new List<double[][]>();

                for (int step = 0; step < steps; step++)
                {
                    try
                    {
                        double[] k1 = new double[numEquations];
                        double[] k2 = new double[numEquations];

                        for (int i = 0; i < numEquations; i++)
                        {
                            k1[i] = functions[i](t, y);
                        }

                        double[] yTemp = new double[numEquations];
                        for (int i = 0; i < numEquations; i++)
                        {
                            yTemp[i] = y[i] + h * k1[i];
                        }

                        for (int i = 0; i < numEquations; i++)
                        {
                            k2[i] = functions[i](t + h, yTemp);
                        }

                        double[] yNew = new double[numEquations];
                        for (int i = 0; i < numEquations; i++)
                        {
                            yNew[i] = y[i] + h / 2.0 * (k1[i] + k2[i]);
                        }

                        Results.Add((double[])y.Clone());
                        TimeSteps.Add(t);
                        KValues.Add(new double[][] { k1, k2 });

                        y = yNew;
                        t += h;
                        if (t > t1) t = t1;

                        progress?.Report((int)((double)(step + 1) / steps * 100));
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(ex);
                        throw new InvalidOperationException($"Ошибка на шаге {step + 1}: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                throw new Exception($"Ошибка в SolveRK2: {ex.Message}", ex);
            }
        }
    }
}
