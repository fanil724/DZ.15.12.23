using System.IO;
using System.Windows;

namespace Task3
{
    public partial class MainWindow : Window
    {
        List<Resume> resumes = new List<Resume>();

        public MainWindow()
        {
            InitializeComponent();
            alls.IsChecked = true;
            max.IsChecked = true;
            string[] files = Directory.GetFiles("Resume");


            files.AsParallel().ForAll(f =>
            {
                using (StreamReader str = new StreamReader(f))
                {
                    resumes.Add(new Resume(str.ReadToEnd()));
                }
            });

            Thread.Sleep(25000);

        }

        private void sal_Click(object sender, RoutedEventArgs e)
        {
            otchet.Items.Clear();
            List<Resume> res = new List<Resume>();
            if (max.IsChecked == true)
                res = resumes.AsParallel().OrderByDescending(x => x.Salary).ToList();

            if (min.IsChecked == true)
                res = resumes.AsParallel().OrderBy(x => x.Salary).ToList();
            Thread.Sleep(10000);

            if (ones.IsChecked == true)
                otchet.Items.Add(res.First());

            if (somes.IsChecked == true)
            {
                foreach (var s in res.Take(3))
                    otchet.Items.Add(s);
            }

            if (alls.IsChecked == true)
            {
                foreach (var s in res)
                    otchet.Items.Add(s);
            }


        }

        private void city_Click(object sender, RoutedEventArgs e)
        {
            if (nameCity.Text == string.Empty)
            {
                MessageBox.Show("Введите название города", "Info", MessageBoxButton.OK);
                return;
            }
            otchet.Items.Clear();
            List<Resume> res = new List<Resume>();

            res = resumes.Where(x => x.City == nameCity.Text).ToList();

            Thread.Sleep(10000);

            foreach (var s in res)
                otchet.Items.Add(s);


        }

        private void opit_Click(object sender, RoutedEventArgs e)
        {
            otchet.Items.Clear();
            List<Resume> res = new List<Resume>();
            if (max.IsChecked == true)
                res = resumes.AsParallel().OrderByDescending(x => x.Experience).ToList();

            if (min.IsChecked == true)
                res = resumes.AsParallel().OrderBy(x => x.Experience).ToList();
            Thread.Sleep(10000);

            if (ones.IsChecked == true)
                otchet.Items.Add(res.First());

            if (somes.IsChecked == true)
            {
                foreach (var s in res.Take(3))
                    otchet.Items.Add(s);
            }

            if (alls.IsChecked == true)
            {
                foreach (var s in res)
                    otchet.Items.Add(s);
            }
        }

        private void ones_Checked(object sender, RoutedEventArgs e)
        {
            somes.IsChecked = false;
            alls.IsChecked = false;
        }

        private void somes_Checked(object sender, RoutedEventArgs e)
        {
            ones.IsChecked = false;
            alls.IsChecked = false;
        }

        private void alls_Checked(object sender, RoutedEventArgs e)
        {
            ones.IsChecked = false;
            somes.IsChecked = false;
        }

        private void max_Checked(object sender, RoutedEventArgs e)
        {
            min.IsChecked = false;
        }

        private void min_Checked(object sender, RoutedEventArgs e)
        {
            max.IsChecked = false;
        }
    }
}