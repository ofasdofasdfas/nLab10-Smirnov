using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace StudentInfoApp
{
    public partial class MainWindow : Window
    {
        private string filePath = "students.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    FileContent.Text = content;
                }
                else
                {
                    MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var students = File.ReadAllLines(filePath)
                                       .Select(line => new Student(line))
                                       .ToList();

                    var filteredStudents = students.Where(s => s.AverageGrade < 70).ToList();

                    StringBuilder result = new StringBuilder();
                    foreach (var student in filteredStudents)
                    {
                        result.AppendLine(student.ToString());
                    }

                    ProcessedContent.Text = result.ToString();

                    string outputFilePath = "filtered_students.txt";
                    File.WriteAllText(outputFilePath, result.ToString());
                    MessageBox.Show($"Filtered data saved to: {outputFilePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string University { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public double AverageGrade { get; set; }
        public string Specialty { get; set; }

        public Student(string data)
        {
            var parts = data.Split(';').Select(p => p.Trim()).ToArray();
            LastName = parts[0];
            FirstName = parts[1];
            MiddleName = parts[2];
            Gender = parts[3];
            Nationality = parts[4];
            Height = int.Parse(parts[5]);
            Weight = int.Parse(parts[6]);
            BirthDate = DateTime.Parse(parts[7]);
            PhoneNumber = parts[8];
            Address = parts[9];
            University = parts[10];
            Course = int.Parse(parts[11]);
            Group = parts[12];
            AverageGrade = double.Parse(parts[13]);
            Specialty = parts[14];
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}, {Gender}, {Nationality}, {Height} cm, {Weight} kg, Born: {BirthDate.ToShortDateString()}, Phone: {PhoneNumber}, Address: {Address}, University: {University}, Course: {Course}, Group: {Group}, Avg Grade: {AverageGrade}, Specialty: {Specialty}";
        }
    }
}
