using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prKol_ind1_WindowsForms_Giniyatullina
{
    public partial class Form1 : Form
    {
        private int[] originalNumbers;   // исходные числа
        private int[] reversedNumbers;   // перевернутые числа
        private string file1;  // путь к текущему файлу
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите файл с числами";
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file1 = openFileDialog1.FileName;

                try
                {
                    string content = File.ReadAllText(file1);

                    string[] parts = content.Split(new char[] { ' ', ',', '\n', '\r', '\t' },
                                                   StringSplitOptions.RemoveEmptyEntries);

                    List<int> numbers = new List<int>();
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int num))
                        {
                            numbers.Add(num);
                        }
                    }

                    if (numbers.Count == 0)
                    {
                        MessageBox.Show("Файл не содержит чисел!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    originalNumbers = numbers.ToArray();

                    label1.Text = "Исходные числа: " + string.Join(" ", originalNumbers);

                    label2.Text = "Перевернутые числа: ";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (originalNumbers == null || originalNumbers.Length == 0)
            {
                MessageBox.Show("Файл не содержит чисел!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < originalNumbers.Length; i++)
            {
                stack.Push(originalNumbers[i]);
            }

            reversedNumbers = new int[stack.Count];
            int index = 0;
            while (stack.Count > 0)
            {
                reversedNumbers[index] = stack.Pop();
                index++;
            }

            label2.Text = "Перевернутые числа: " + string.Join(" ", reversedNumbers);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (reversedNumbers == null || reversedNumbers.Length == 0)
            {
                MessageBox.Show("Нет данных для сохранения!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Title = "Сохранить результат";
            saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog1.FileName = "file2.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = string.Join(" ", reversedNumbers);
                    File.WriteAllText(saveFileDialog1.FileName, content);

                    MessageBox.Show($"Результат сохранен!\n\nФайл: {saveFileDialog1.FileName}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
