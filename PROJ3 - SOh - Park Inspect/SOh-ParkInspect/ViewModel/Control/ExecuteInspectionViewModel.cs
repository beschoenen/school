using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.Command;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;
using SOh_ParkInspect.ViewModel.Question;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class ExecuteInspectionViewModel : ParkInspectViewModel
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly RouterService _router;

        public ICommand SaveQuestionListCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private Inspection _inspection;

        public string ChecklistName { get; set; }
        public ObservableCollection<IQuestionWrapper> ControlList { get; set; }

        public ExecuteInspectionViewModel(RouterService router, IInspectionRepository inspectionRepo)
        {
            _router = router;
            _inspectionRepository = inspectionRepo;

            SaveQuestionListCommand = new RelayCommand(SaveAnswers);
            BackCommand = new RelayCommand(_router.GoBack);

            ControlList = new ObservableCollection<IQuestionWrapper>();
        }

        private void SaveAnswers()
        {
            var invalid = ControlList.FirstOrDefault(control => !control.IsValid);
            if (invalid != null)
            {
                MessageBox.Show($"De vraag \"{invalid.Question.Text}\" is niet geldig ingevuld!", "Fout", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ControlList.ToList().ForEach(x =>
            {
                var answer = x.GetAnswer();
                answer.Inspection = _inspection;
                answer.Employee = Settings.CurrentUser;

                if (answer.Photo != null)
                {
                    answer.Photo.Inspection = _inspection;
                }

                _inspection.DateTimeDone = DateTime.Now;

                _inspection.Answers.Add(answer);
                x.Question.Answers.Add(answer);
            });

            if (!_inspectionRepository.Update(_inspection))
            {
                MessageBox.Show("Er ging iets fout, probeer het opnieuw!");
                return;
            }

            MessageBox.Show("De inspectie is opgeslagen.");
            _router.GoBack();
        }

        public override void OnEnter()
        {
            if (ViewBag?.Inspection == null)
            {
                MessageBox.Show("Geen inspectie geselecteerd, u gaat nu terug naar het vorige scherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Information);
                _router.GoBack();

                return;
            }

            if (ViewBag.Inspection?.Checklist == null)
            {
                MessageBox.Show("Geen checklist beschikbaar, u gaat nu terug naar het vorige scherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Information);
                _router.GoBack();

                return;
            }

            _inspection = ViewBag.Inspection;
            _inspection.DateTimeStarted = DateTime.Now;

            var questions = _inspection.Checklist.ChecklistQuestions.Select(x => x.Question).ToList();

            if (questions.Count == 0)
            {
                MessageBox.Show("Geen vragen beschikbaar, u gaat nu terug naar het vorige scherm.", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Information);
                _router.GoBack();

                return;
            }

            ControlList.Clear();

            ChecklistName = _inspection.Checklist.Name;

            //Dynamically create QuestionViewModels
            foreach (var item in questions)
            {
                switch (item.QuestionType.Name)
                {
                    case QuestionType.MultipleChoice: //Multiple Choice -- Checkboxes
                        ControlList.Add(new MultipleChoiceQuestionViewModel(item));
                        break;
                    case QuestionType.SingleChoice: //Single choice -- ComboBox/RadioButtons
                        ControlList.Add(new SingleChoiceQuestionViewModel(item));
                        break;
                    case QuestionType.Text: //Text -- TextBox
                        ControlList.Add(new OpenQuestionViewModel(item));
                        break;
                    case QuestionType.Number: //Numeriek -- TextBox
                        ControlList.Add(new NumericQuestionViewModel(item));
                        break;
                    case QuestionType.Decimal: //KommaGetal -- TextBox
                        ControlList.Add(new DecimalQuestionViewModel(item));
                        break;
                    case QuestionType.Date: // Datum -- DatePicker
                        ControlList.Add(new DateQuestionViewModel(item));
                        break;
                    case QuestionType.Time: //Tijd -- DateTimePicker
                        ControlList.Add(new TimeQuestionViewModel(item));
                        break;
                    case QuestionType.DateTime: //DatumTijd -- DateTimePicker
                        ControlList.Add(new DateTimeQuestionViewModel(item));
                        break;
                    case QuestionType.Photo: //Foto -- FilePicker
                        ControlList.Add(new PhotoQuestionViewModel(item));
                        break;
                }
            }
        }
    }
}