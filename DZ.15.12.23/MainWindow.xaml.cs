using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace DZ._15._12._23
{
    public partial class MainWindow : Window
    {
        string text = "";
        List<int> ints = new List<int>();
        public MainWindow()
        {
            InitializeComponent();

            Task t = Task.Run(() =>
            {
                using (StreamReader reader = new StreamReader("Test.txt"))
                {
                    text = reader.ReadToEnd();
                    ints = text.Split(" ").Select(x => Convert.ToInt32(x)).ToList();
                    Dispatcher.Invoke(() => result.Items.Add(text));
                }
            });
        }

        private void task1_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            ints.AsParallel().ForAll((x) =>
            {
                int cnt = 0;
                for (int i = 0; i < ints.Count; i++)
                {
                    if (ints[i] == x) cnt++;
                }
                if (cnt == 1) count++;
            });
            result.Items.Add($"Количество уникальных значений: {count}");
        }

        private void task2_Click(object sender, RoutedEventArgs e)
        {
            List<string> str = new List<string>();
            Enumerable.Range(0, ints.Count).AsParallel().ForAll(x =>
           {

               List<string> s = new List<string>();
               var res = ints.Skip(x).ToList();

               for (int i = 1; i < res.Count - 1; i++)
               {
                   if (i == 1) s.Add(res[i - 1].ToString());

                   if (res[i - 1] < res[i])
                   {
                       s.Add(res[i].ToString());
                   }
                   else
                   {
                       i = res.Count;
                   }
               }
               str.Add(string.Join(" ", s));
           });
            Thread.Sleep(1000);
            string s1 = str.OrderByDescending(x => x.Count()).First();

            result.Items.Add($"Самая длинная возрастающая последовательность: {s1} ");
        }

        private void task3_Click(object sender, RoutedEventArgs e)
        {

            List<string> strings = new List<string>();

            // Parallel.For(0, ints.Count, x => strings.Add(string.Join(" ", ints.Skip(x).TakeWhile((y) => y > 0))));

            Enumerable.Range(0, ints.Count).AsParallel().ForAll(x => strings.Add(string.Join(" ", ints.Skip(x).TakeWhile((y) => y > 0))));

            var s = strings.OrderByDescending(x => x.Length).FirstOrDefault();

            foreach (string s2 in strings)
            {
                result.Items.Add(s2);
            }
            result.Items.Add($"Самая длинная положительная последовательность: {s} ");

        }

        private void clr_Click(object sender, RoutedEventArgs e)
        {
            result.Items.Clear();
            result.Items.Add(text);
        }
    }
}