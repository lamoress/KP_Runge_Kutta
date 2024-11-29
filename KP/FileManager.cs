using System;
using System.IO;
using System.Windows.Forms;

namespace KP
{
    public static class FileManager
    {
        public static void SaveResults(DataGridView dataGridView)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", Title = "Сохранить результаты" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        int columnCount = dataGridView.Columns.Count;
                        string columnNames = "";
                        string[] outputCsv = new string[dataGridView.Rows.Count + 1];
                        for (int i = 0; i < columnCount; i++)
                        {
                            columnNames += dataGridView.Columns[i].HeaderText.ToString() + ";";
                        }
                        outputCsv[0] += columnNames;

                        for (int i = 1; i < outputCsv.Length; i++)
                        {
                            for (int j = 0; j < columnCount; j++)
                            {
                                outputCsv[i] += dataGridView.Rows[i - 1].Cells[j].Value?.ToString() + ";";
                            }
                        }

                        File.WriteAllLines(sfd.FileName, outputCsv);
                        MessageBox.Show("Результаты успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.LogException(ex);
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}