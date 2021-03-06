﻿using System;
using System.Globalization;
using System.Windows;
using ZumtenSoft.Linq2ObsCollection.Collections;

namespace ZumtenSoft.Linq2ObsCollection.Samples.TreeSample
{
    /// <summary>
    /// Interaction logic for TreeSampleControl.xaml
    /// </summary>
    public partial class TreeSampleControl
    {
        private readonly IObservableCollection<Student> _students;

        public TreeSampleControl()
        {
            InitializeComponent();

            _students = new ObservableCollection<Student>();

            // Creating a collection of viewModels sorted by student name
            IObservableCollection<StudentViewModel> studentViewModels =
                from student in _students
                orderby student.Score, student.Name
                select new StudentViewModel(student);

            // Creating a grouping by score (1 to 10, 11 to 20, ..., 91 to 100)
            IObservableCollection<StudentGroupingNode> groups =
                from vm in studentViewModels
                group vm by (vm.Score - 1) / 10 into grp
                orderby grp.Key
                select new StudentGroupingNode(grp.Key * 10 + 1, ((grp.Key + 1) * 10), grp);

            ListStudents.ItemsSource = groups;

            string[] names = new string[] {
	            "Rhiannon Norris", "Marcelino Mciver", "Stacey Parton", "Roseline Rollings", "Sparkle Vieyra",
	            "Flora Kingman", "Carletta Huffaker", "Allison Baisden", "Phuong Matte", "Darci Tittle",
	            "Priscilla Uzzle", "Lucrecia Sabine", "Many Volz", "Oralia Rizzi", "Divina Gajewski",
	            "Granville Holm", "Elinore Martelli", "Cherise Dobbin", "Maura Murphey", "Silva Coker",
	            "Sharmaine Truesdell", "Chelsea Hanberry", "Dorethea Irion", "Ronni Steinman", "Lester Elam",
	            "Nicolasa Broughton", "Michale Canto", "Tory Wehrle", "Rosia Ohlinger", "Delta Gendreau",
	            "Jude Quinto", "Marilyn Erhardt", "Sergio Cassell", "Kenda Bolland", "Elna Hutsell",
	            "Gilbert Escobar", "Raymond Steenbergen", "Loraine Towle", "Deanne Holding", "Hui Parkison",
	            "Delsie Lukasik", "Asia Bergren", "Meri Carpenter", "Scot Sax", "Jennine Crapo",
	            "Kristi Greenhalgh", "Keturah Kravetz", "Synthia Dona", "Frank Mccaffery", "Zana Alley"
            };

            Random rnd = new Random();
            foreach (string name in names)
                _students.Add(new Student(name, rnd.Next(45, 100)));
        }


        private void btnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            StudentViewModel student = (StudentViewModel)ListStudents.SelectedItem;
            student.Model.Name = TxtName.Text;
            student.Model.Score = Int32.Parse(TxtScore.Text);
        }

        private void btnCreate_OnClick(object sender, RoutedEventArgs e)
        {
            Student student = new Student(TxtName.Text, Int32.Parse(TxtScore.Text));
            _students.Add(student);
        }

        private void LstStudents_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            StudentViewModel student = e.NewValue as StudentViewModel;
            BtnUpdate.IsEnabled = student != null;
            if (student != null)
            {
                TxtName.Text = student.Model.Name;
                TxtScore.Text = student.Model.Score.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
