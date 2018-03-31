using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using PhotoQuestionControl = SOh_ParkInspect.Control.Question.PhotoQuestionControl;

namespace SOh_ParkInspect.ViewModel.Question
{
    public class PhotoQuestionViewModel : ViewModelBase, IQuestionWrapper
    {
        public Database.Question Question { get; }
        public string Answer { get; set; }

        public ICollection<AnswerSetValue> AnswerSet { get; }

        public ICommand LoadAndShowCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        private BitmapImage _photoValue;
        public BitmapImage PhotoValue
        {
            get { return _photoValue; }
            set
            {
                _photoValue = value;
                RaisePropertyChanged();
            }
        }

        public bool IsValid => PhotoValue != null;

        public UserControl Control => new PhotoQuestionControl
        {
            DataContext = this
        };

        public PhotoQuestionViewModel(Database.Question question)
        {
            Question = question;
            AnswerSet = Question.AnswerSetValues;

            LoadAndShowCommand = new RelayCommand(LoadAndShow);
            RemoveCommand = new RelayCommand(Remove);
        }

        public Answer GetAnswer()
        {
            var filename = PhotoValue.UriSource.LocalPath.Split(Convert.ToChar("\\")).Last();
            return new Answer
            {
                Text = filename,
                Photo = new Photo
                {
                    Filename = filename,
                    Image = ImageToBase64()
                }
            };
        }

        private string ImageToBase64()
        {
            var bytes = File.ReadAllBytes(PhotoValue.UriSource.AbsolutePath);

            return Convert.ToBase64String(bytes);
        }

        private void Remove()
        {
            PhotoValue = null;
            Answer = string.Empty;
        }

        private void LoadAndShow()
        {
            var op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };

            if (op.ShowDialog() != true) return;

            PhotoValue = new BitmapImage(new Uri(op.FileName));
            Answer = PhotoValue.UriSource.LocalPath.Split(Convert.ToChar("\\")).Last();
        }
    }
}