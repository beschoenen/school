using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Database;
using GalaSoft.MvvmLight.CommandWpf;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.Repository.Interface;
using SOh_ParkInspect.ViewModel.Template;
using static CryptSharp.Crypter;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class EditTemplateViewModel : ParkInspectViewModel
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IChecklistQuestionRepository _checklistQuestionRepository;
        private readonly IAnswerSetValueRepository _answerSetValueRepository;
        private readonly RouterService _router;

        public ICommand AddExistingQuestionCommand { get; set; }
        public ICommand AddNewQuestionCommand { get; set; }
        public ICommand SaveTemplateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RemoveSelectedQuestionCommand { get; set; }

        public QuestionType SelectedQuestionType { get; set; }

        public ObservableCollection<Database.Question> ExistingQuestions { get; set; }
        public ObservableCollection<ChecklistQuestion> TemplateQuestions { get; set; }
        public ObservableCollection<QuestionType> QuestionTypes { get; }
        public ObservableCollection<ITemplateWrapper> ControlList { get; set; }

        private Checklist _checklist;

        public Checklist Checklist
        {
            get { return _checklist; }
            set
            {
                _checklist = value;
                RaisePropertyChanged();
            }
        }

        private ChecklistQuestion _selectedTemplateQuestion;

        public ChecklistQuestion SelectedTemplateQuestion
        {
            get { return _selectedTemplateQuestion; }
            set
            {
                _selectedTemplateQuestion = value;
                RaisePropertyChanged();
                SelectTemplateQuestion();
            }
        }

        private Database.Question _selectedExistingQuestion;

        public Database.Question SelectedExistingQuestion
        {
            get { return _selectedExistingQuestion; }
            set
            {
                _selectedExistingQuestion = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AddExistingQuestionEnabled));
            }
        }

        public bool AddExistingQuestionEnabled => ExistingQuestions.Count > 0 && SelectedExistingQuestion != null;

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

        public EditTemplateViewModel(RouterService router,
                                     IQuestionRepository questionRepository,
                                     IQuestionTypeRepository questionTypeRepository,
                                     IAnswerSetValueRepository answerSetValueRepository,
                                     IChecklistRepository checklistRepository,
                                     IChecklistQuestionRepository checklistQuestionRepository)
        {
            _questionRepository = questionRepository;
            _answerSetValueRepository = answerSetValueRepository;
            _checklistRepository = checklistRepository;
            _checklistQuestionRepository = checklistQuestionRepository;
            _router = router;

            AddExistingQuestionCommand = new RelayCommand(AddExistingQuestion, () => SelectedExistingQuestion != null);
            AddNewQuestionCommand = new RelayCommand(AddNewQuestion, () => SelectedQuestionType != null);
            SaveTemplateCommand = new RelayCommand(SaveTemplate);
            CancelCommand = new RelayCommand(_router.GoBack);
            RemoveSelectedQuestionCommand = new RelayCommand(RemoveSelectedQuestion);

            ExistingQuestions = new ObservableCollection<Database.Question>();
            QuestionTypes = new ObservableCollection<QuestionType>(questionTypeRepository.All());
            TemplateQuestions = new ObservableCollection<ChecklistQuestion>();
            ControlList = new ObservableCollection<ITemplateWrapper>();

            SelectedQuestionType = QuestionTypes.Count > 0 ? QuestionTypes[0] : null;
        }

        private void RemoveSelectedQuestion()
        {
            TemplateQuestions.Remove(SelectedTemplateQuestion);
            SelectedTemplateQuestion = TemplateQuestions.Count > 0 ? TemplateQuestions[0] : null;
        }

        public override void OnEnter()
        {
            if (Settings.IsOfflineMode)
            {
                MessageBox.Show("Dit scherm is niet beschikbaar in offline mode.");
                _router.GoBack();
                return;
            }

            TemplateQuestions.Clear();

            if (ViewBag?.Template == null)
            {
                Checklist = new Checklist();
                return;
            }

            Checklist = ViewBag.Template.GetCleanModel();

            const string regex = "\\s{1}[Vv]{1}\\d{1,}$";

            if (Regex.Match(Checklist.Name, regex).Success)
            {
                Checklist.Name = Regex.Replace(Checklist.Name, regex, string.Empty);
            }

            Checklist.Name += $" v{Checklist.Version}";

            RaisePropertyChanged(nameof(Checklist));

            _checklistQuestionRepository.GetChecklistQuestions(Checklist.Parent).ForEach(q => TemplateQuestions.Add(q.GetCleanModel()));

            _questionRepository.All().ForEach(ExistingQuestions.Add);
        }

        private void AddExistingQuestion()
        {
            if (SelectedExistingQuestion == null) return;
            if (TemplateQuestions.Any(t => t.Question.Equals(SelectedExistingQuestion))) return;

            var max = TemplateQuestions.Max(t => t.Order);
            var checklistQuestion = new ChecklistQuestion
            {
                Checklist = Checklist,
                Order = max == null ? 0 : max + 1,
                Question = SelectedExistingQuestion
            };

            TemplateQuestions.Add(checklistQuestion);
            SelectedTemplateQuestion = checklistQuestion;
        }

        private void AddNewQuestion()
        {
            if (SelectedQuestionType == null) return;

            var newQuestion = new Database.Question
            {
                QuestionType = SelectedQuestionType,
                Hash = MD5.Crypt($"{SelectedQuestionType.Name}-{DateTime.Now}")
            };
            var max = TemplateQuestions.Max(t => t.Order);
            var checklistQuestion = new ChecklistQuestion
            {
                Checklist = Checklist,
                Order = max == null ? 0 : max + 1,
                Question = newQuestion
            };

            TemplateQuestions.Add(checklistQuestion);
            SelectedTemplateQuestion = checklistQuestion;
        }

        private void SelectTemplateQuestion()
        {
            ControlList.Clear();
            if (SelectedTemplateQuestion == null) return;

            ITemplateWrapper control;

            switch (SelectedTemplateQuestion.Question.QuestionType.Name)
            {
                case QuestionType.MultipleChoice: // multiple choice
                case QuestionType.SingleChoice: // single choice
                    control = new TemplateChoiceViewModel(SelectedTemplateQuestion.Question, _answerSetValueRepository);
                    break;
                case QuestionType.Text: // text
                case QuestionType.Number: // numeric
                case QuestionType.Decimal: // decimal
                case QuestionType.Date: // date
                case QuestionType.Time: // time
                case QuestionType.DateTime: // datetime
                case QuestionType.Photo: // photo
                    control = new TemplateViewModel(SelectedTemplateQuestion.Question);
                    break;
                default:
                    control = null;
                    break;
            }

            if (control != null)
            {
                ControlList.Add(control);
            }
        }

        private void SaveTemplate()
        {
            if (!CanSave()) return;

            Checklist.Hash = MD5.Crypt($"Checklist {Checklist.Inspections.Count}-{Checklist.Name}");
            Checklist.Name = Checklist.Name?.Trim();
            Checklist.Remarks = Checklist.Remarks?.Trim() ?? "";

            var questions = _checklistQuestionRepository.GetChecklistQuestions(Checklist.Parent);

            TemplateQuestions
                .ToList()
                .ForEach(q =>
                {
                    var question = questions.FirstOrDefault(x => x.Question.ID == q.Question.ID);

                    if (question == null)
                    {
                        Checklist.ChecklistQuestions.Add(q);
                        return;
                    }

                    if (q.Question.Equals(question.Question))
                    {
                        Checklist.ChecklistQuestions.Add(q);
                        return;
                    }

                    Checklist.ChecklistQuestions.Add(q.GetCleanModel());
                });

            Checklist.DateTimeCreated = DateTime.Now;

            _checklistRepository.Add(Checklist);
            MessageBox.Show($"Template met de naam {Checklist.Name} en {Checklist.ChecklistQuestions.Count} vragen is opgeslagen.");
            _router.GoBack();
        }

        private bool CanSave()
        {
            if (_checklistRepository.All().Any(x => x.Name == Checklist?.Name?.Trim()))
            {
                ErrorMessage = "Naam van de vragenlijst is al in gebruik.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Checklist.Name))
            {
                ErrorMessage = "Naam van de vragenlijst mag niet leeg zijn.";
                return false;
            }

            if (TemplateQuestions == null || TemplateQuestions.Count == 0)
            {
                ErrorMessage = "Er zijn geen vragen toegevoegd aan het template.";
                return false;
            }

            foreach (var templateQuestion in TemplateQuestions)
            {
                if (string.IsNullOrWhiteSpace(templateQuestion.Question.Text))
                {
                    ErrorMessage = "Er zijn een of meer vragen onvolledig.";
                    return false;
                }

                if (templateQuestion.Question.QuestionType == null)
                {
                    ErrorMessage = "Er is een vraag zonder vraagtype.";
                    return false;
                }

                if (!new[] { QuestionType.MultipleChoice, QuestionType.SingleChoice }.Contains(templateQuestion.Question.QuestionType.Name))
                {
                    continue;
                }

                if ((templateQuestion.Question.AnswerSetValues == null || templateQuestion.Question.AnswerSetValues.Count == 0 )&& templateQuestion.Question.QuestionType.Name != "Multiple choice")
                {
                    ErrorMessage = "Er zijn een of meerdere multiple of single choice vragen zonder antwoorden.";
                    return false;
                }
            }
            return true;
        }
    }
}