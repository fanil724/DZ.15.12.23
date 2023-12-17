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
            int count = 0;
            int pos = 0;
            ints.AsParallel().AsOrdered().ForAll((x) =>
           {
               int cnt = 0;
               int p = 0;
               var res = ints.Skip(x).ToList();
               for (int i = 0; i < res.Count() - 1; i++)
               {
                   if (res[i] < res[i + 1])
                   {
                       cnt++;
                       if (cnt == 1) p = i;
                   }
                   else
                   {
                       if (count < cnt)
                       {
                           count = cnt;
                           pos = p;
                       }

                       cnt = 0;

                   }
               }

           });

            string s = "";
            for (int i = pos; i < count; i++)
            {
                s += $" {ints[i]}";
            }
            result.Items.Add($"Самая длинная возрастающая последовательность: {s} количесвто элементов {count} ");

        }

        private void task3_Click(object sender, RoutedEventArgs e)
        {

            List<string> strings = new List<string>();

            ints.AsParallel().AsOrdered().ForAll((x) =>
             {
                 strings.Add(string.Join(" ", ints.Skip(x).TakeWhile((y) => y > 0)));
             });

            var s = strings.OrderByDescending(x => x.Length).FirstOrDefault();

            //foreach (string s2 in strings)
            //{
            //    result.Items.Add(s2);
            //}
            result.Items.Add($"Самая длинная положительная последовательность: {s} ");

        }

        private void clr_Click(object sender, RoutedEventArgs e)
        {
            result.Items.Clear();
            result.Items.Add(text);
        }
    }
}