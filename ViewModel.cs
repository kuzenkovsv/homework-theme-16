using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace homework_theme_16
{
    class ViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Коллекция вычислений
        /// </summary>
        private ObservableCollection<MyMessages> calculationsdb = new ObservableCollection<MyMessages>();
        public ObservableCollection<MyMessages> Calculationsdb
        {
            get
            {
                return calculationsdb;
            }
            set
            {
                calculationsdb = value;
                OnPropertyChanged(nameof(calculationsdb));
            }
        }

        /// <summary>
        /// Информация о времени работы приложения
        /// </summary>
        private ObservableCollection<MyMessages> timeInformationdb = new ObservableCollection<MyMessages>();
        public ObservableCollection<MyMessages> TimeInformationdb
        {
            get
            {
                return timeInformationdb;
            }
            set
            {
                timeInformationdb = value;
                OnPropertyChanged(nameof(timeInformationdb));
            }
        }


        /// <summary>
        /// Вычисление
        /// </summary>
        private RelayCommand calculate;
        public RelayCommand Calculate
        {
            get
            {
                return calculate ??
                  (calculate = new RelayCommand(obj =>
                  {
                      MainWindow calc = obj as MainWindow;

                      if (calc.OperationSelection.Text != null)
                      {
                          int res;
                          switch (calc.OperationSelection.Text)
                          {
                              case "Сложение":
                                  res = Convert.ToInt32(calc.Numb1.Text) + Convert.ToInt32(calc.Numb2.Text);
                                  calc.Result.Text = Convert.ToString(res);
                                  Calculationsdb.Add(new MyMessages(DateTime.Now, $"Сумма чисел {calc.Numb1.Text} и " +
                                      $"{calc.Numb2.Text} равна {calc.Result.Text}"));
                                  break;
                              case "Вычитание":
                                  res = Convert.ToInt32(calc.Numb1.Text) - Convert.ToInt32(calc.Numb2.Text);
                                  calc.Result.Text = Convert.ToString(res);
                                  Calculationsdb.Add(new MyMessages(DateTime.Now, $"{calc.Numb1.Text} вычесть " +
                                      $"{calc.Numb2.Text} равно {calc.Result.Text}"));
                                  break;

                              default:
                                  break;
                          }
                      }

                  }));
            }
        }

        void TimeInfo(ObservableCollection<MyMessages> TimeInformationdb)
        {
            try
            {
                int count = 0;
                int count1 = 0;

                while (true)
                {
                    Thread.Sleep(7000);
                    using (StreamWriter sw = new StreamWriter("messageLog.txt", true))
                    {
                        sw.WriteLine($" {DateTime.Now} - С момента запуска прошло {count += 7} сек.");
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TimeInformationdb.Add(new MyMessages(DateTime.Now, $"С момента запуска прошло {count1 += 7} сек."));

                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
        

        async void TimeInfoAsync(ObservableCollection<MyMessages> TimeInformationdb)
        {
            await Task.Run(() => TimeInfo(TimeInformationdb));
        }


        public ViewModel()
        {
            TimeInfoAsync(TimeInformationdb);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
