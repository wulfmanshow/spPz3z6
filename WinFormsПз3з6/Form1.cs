namespace WinFormsПз3з6
{
    public partial class Form1 : Form
    {
        private int[] mass = new int[10000];
        public Form1()
        {
            InitializeComponent();
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                mass[i] = random.Next(1, 151);
            }
        }
        private void SetListBox(int[] mas)
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.Invoke(new Action<int[]>(SetListBox), new object[] { mas });
            }
            else
            {
                foreach (int ma in mas)
                {
                    listBox1.Items.Add(ma);
                }
            }
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            SetListBox(mass);
            int max = 0, min = 0;
            double avg = 0;
            void MaxNumber(int[] mas)
            {
                max = mas[0];
                for (int i = 0; i < mas.Length; i++)
                {
                    if (max < mas[i])
                    {
                        max = mas[i];
                    }
                }
            }
            void MinNumber(int[] mas)
            {
                min = mas[0];
                for (int i = 0; i < mas.Length; i++)
                {
                    if (min > mas[i])
                    {
                        min = mas[i];
                    }
                }
            }
            void AvgNumber(int[] mas)
            {

                for (int i = 0; i < mas.Length; i++)
                {
                    avg += mas[i];
                }
                avg /= mas.Length;
            }
            void output(int[] mas)
            {
                using (StreamWriter writer = new StreamWriter("result.txt"))
                {
                    for (int i = 0; i < mas.Length; i++)
                    {
                        writer.WriteLine(mas[i]);
                    }
                    writer.WriteLine($"max-{max}, min-{min}, avg-{avg}");
                }
            }


            Thread thread1 = new Thread(() => MaxNumber(mass));
            thread1.Start();

            Thread thread2 = new Thread(() => MinNumber(mass));
            thread2.Start();

            Thread thread3 = new Thread(() => AvgNumber(mass));
            thread3.Start();

            Thread thread4 = new Thread(() => output(mass));
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            label1.Text = max.ToString();
            label2.Text = min.ToString();
            label3.Text = avg.ToString();
            Console.WriteLine($"{max}, {min}, {avg}");
        }
    }
}