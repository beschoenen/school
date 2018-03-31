using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SOh_ParkInspect.Enum;
using SOh_ParkInspect.Helper;
using SOh_ParkInspect.Repository;
using SOh_ParkInspect.Repository.Dummy;
using SOh_ParkInspect.Repository.Interface;
using SOh_ParkInspect.ViewModel.Control;
using System.Reflection;
using Database;
using SOh_ParkInspect.Model;
using SOh_ParkInspect.ViewModel.Question;
using SOh_ParkInspect.ViewModel.Template;
using SOh_ParkInspect.ViewModel.ManagementReport;

namespace SOh_ParkInspect.ViewModel
{
    public class ViewModelLocator
    {
        private static RepositoryType? CurrentRepositoryType;

        private static List<IRepositoryMap> RepositoryMap;

        ///////////////////////////
        // Register View viewmodels

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();

        ///////////////////////////////
        // Register Control viewmodels

        public ChangePasswordViewModel ChangePassword => ServiceLocator.Current.GetInstance<ChangePasswordViewModel>();
        public ClientInfoViewModel ClientInfo => ServiceLocator.Current.GetInstance<ClientInfoViewModel>();
        public CreateTaskViewModel CreateTask => ServiceLocator.Current.GetInstance<CreateTaskViewModel>();
        public EditTemplateViewModel EditTemplate => ServiceLocator.Current.GetInstance<EditTemplateViewModel>();
        public EmployeeInfoViewModel EmployeeInfo => ServiceLocator.Current.GetInstance<EmployeeInfoViewModel>();
        public TaskDetailsViewModel TaskDetails => ServiceLocator.Current.GetInstance<TaskDetailsViewModel>();
        public TaskOverviewViewModel TaskOverview => ServiceLocator.Current.GetInstance<TaskOverviewViewModel>();
        public ManagementRapportViewModel ManagementRapport => ServiceLocator.Current.GetInstance<ManagementRapportViewModel>();
        public ManagementReportGenerationViewModel ManagementReportGeneration => ServiceLocator.Current.GetInstance<ManagementReportGenerationViewModel>();
        public RegisterInspectorViewModel RegisterInspector => ServiceLocator.Current.GetInstance<RegisterInspectorViewModel>();
        public TemplateOverviewViewModel TemplateOverview => ServiceLocator.Current.GetInstance<TemplateOverviewViewModel>();
        public UserManagementViewModel UserManagement => ServiceLocator.Current.GetInstance<UserManagementViewModel>();
        public InspectorAvailabilityViewModel InspectorAvailability => ServiceLocator.Current.GetInstance<InspectorAvailabilityViewModel>();
        public InspectionDurationViewModel InspectionDuration => ServiceLocator.Current.GetInstance<InspectionDurationViewModel>();
        public NewInspectionViewModel NewInspection => ServiceLocator.Current.GetInstance<NewInspectionViewModel>();
        public LocationInfoViewModel LocationInfo => ServiceLocator.Current.GetInstance<LocationInfoViewModel>();
        public ManagentViewModel Management => ServiceLocator.Current.GetInstance<ManagentViewModel>();
        public HeatMapViewModel HeatMap => ServiceLocator.Current.GetInstance<HeatMapViewModel>();
        public AccountViewModel Account => ServiceLocator.Current.GetInstance<AccountViewModel>();
        public InspectionOverviewViewModel InspectionOverview => ServiceLocator.Current.GetInstance<InspectionOverviewViewModel>();
        public ClientOverviewViewModel ClientOverview => ServiceLocator.Current.GetInstance<ClientOverviewViewModel>();
        public LinkInspectorViewModel LinkInspector => ServiceLocator.Current.GetInstance<LinkInspectorViewModel>();
        public EmployeeAvailabilityViewModel EmployeeAvailability => ServiceLocator.Current.GetInstance<EmployeeAvailabilityViewModel>();
        public ExecuteInspectionViewModel ExecuteInspection => ServiceLocator.Current.GetInstance<ExecuteInspectionViewModel>();

        ///////////
        // Template

        public TemplateChoiceViewModel TemplateChoice => ServiceLocator.Current.GetInstance<TemplateChoiceViewModel>();
        public TemplateViewModel Template => ServiceLocator.Current.GetInstance<TemplateViewModel>();

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //////////////////////
            // Register Singletons

            SimpleIoc.Default.Register(() => new RouterService());

            ////////////////////////////////////////
            // Register dependency injection aliases

            SimpleIoc.Default.Register<Context, OnlineDatabase>();

            RepositoryMap = new List<IRepositoryMap>
            {
                new RepositoryMap<IAnswerSetValueRepository, DummyAnswerSetValueRepository, AnswerSetValueRepository>(),
                new RepositoryMap<IAvailabilityRepository, DummyAvailabilityRepository, AvailabilityRepository>(),
                new RepositoryMap<IChecklistRepository, DummyChecklistRepository, ChecklistRepository>(),
                new RepositoryMap<IChecklistQuestionRepository, DummyChecklistQuestionRepository, ChecklistQuestionRepository>(),
                new RepositoryMap<ICustomerRepository, DummyCustomerRepository, CustomerRepository>(),
                new RepositoryMap<IInspectionRepository, DummyInspectionRepository, InspectionRepository>(),
                new RepositoryMap<IIntervalFrequencyRepository, DummyIntervalFrequencyRepository, IntervalFrequencyRepository>(),
                new RepositoryMap<IJobTitleRepository, DummyJobTitleRepository, JobTitleRepository>(),
                new RepositoryMap<IParkingLotRepository, DummyParkingLotRepository, ParkingLotRepository>(),
                new RepositoryMap<IQuestionRepository, DummyQuestionRepository, QuestionRepository>(),
                new RepositoryMap<IQuestionTypeRepository, DummyQuestionTypeRepository, QuestionTypeRepository>(),
                new RepositoryMap<ITaskRepository, DummyTaskRepository, TaskRepository>(),
                new RepositoryMap<IUserRepository, DummyUserRepository, UserRepository>(),
                new RepositoryMap<IAddressRepository, DummyAddressRepository, AddressRepository>(),
                new RepositoryMap<IAvailabilityRepository, DummyAvailabilityRepository, AvailabilityRepository>(),
                new RepositoryMap<IScheduleRepository, DummyScheduleRepository, ScheduleRepository>(),
            };

            RegisterRepositories(Settings.DEBUGGING ? RepositoryType.Dummy : RepositoryType.Online);

            ///////////////////////////
            // Register View viewmodels

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();

            ///////////////////////////////
            // Register Control viewmodels

            SimpleIoc.Default.Register<ChangePasswordViewModel>();
            SimpleIoc.Default.Register<ClientInfoViewModel>();
            SimpleIoc.Default.Register<CreateTaskViewModel>();
            SimpleIoc.Default.Register<EditTemplateViewModel>();
            SimpleIoc.Default.Register<EmployeeInfoViewModel>();
            SimpleIoc.Default.Register<TaskDetailsViewModel>();
            SimpleIoc.Default.Register<TaskOverviewViewModel>();
            SimpleIoc.Default.Register<InspectionOverviewViewModel>();
            SimpleIoc.Default.Register<ManagementRapportViewModel>();
            SimpleIoc.Default.Register<RegisterInspectorViewModel>();
            SimpleIoc.Default.Register<TemplateOverviewViewModel>();
            SimpleIoc.Default.Register<ManagentViewModel>();
            SimpleIoc.Default.Register<UserManagementViewModel>();
            SimpleIoc.Default.Register<LocationInfoViewModel>();
            SimpleIoc.Default.Register<ManagementReportGenerationViewModel>();
            SimpleIoc.Default.Register<InspectorAvailabilityViewModel>();
            SimpleIoc.Default.Register<InspectionDurationViewModel>();
            SimpleIoc.Default.Register<HeatMapViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<EmployeeAvailabilityViewModel>();
            SimpleIoc.Default.Register<InspectionOverviewViewModel>();
            SimpleIoc.Default.Register<NewInspectionViewModel>();
            SimpleIoc.Default.Register<LinkInspectorViewModel>();
            SimpleIoc.Default.Register<ClientOverviewViewModel>();
            SimpleIoc.Default.Register<ExecuteInspectionViewModel>();

            //////////////////////
            // Question viewmodels

            SimpleIoc.Default.Register<DateQuestionViewModel>();
            SimpleIoc.Default.Register<DateTimeQuestionViewModel>();
            SimpleIoc.Default.Register<SingleChoiceQuestionViewModel>();
            SimpleIoc.Default.Register<MultipleChoiceQuestionViewModel>();
            SimpleIoc.Default.Register<NumericQuestionViewModel>();
            SimpleIoc.Default.Register<DecimalQuestionViewModel>();
            SimpleIoc.Default.Register<OpenQuestionViewModel>();
            SimpleIoc.Default.Register<PhotoQuestionViewModel>();
            SimpleIoc.Default.Register<TimeQuestionViewModel>();

            //////////////////////
            // Template viewmodels

            SimpleIoc.Default.Register<TemplateViewModel>();
            SimpleIoc.Default.Register<TemplateChoiceViewModel>();
        }

        ///////////////////////////////////////////////////////
        // Reflection methods to help register the repositories

        public static void RegisterRepositories(RepositoryType forceType)
        {
            // When the type doesn't change
            if (forceType == CurrentRepositoryType) return;

            ChangeDbContext(forceType);

            RepositoryMap.ForEach(map =>
                                  {
                                      Unregister(map.Interface);
                                      RegisterRepository(map, forceType);
                                  });

            CurrentRepositoryType = forceType;
        }

        private static void ChangeDbContext(RepositoryType type)
        {
            Unregister(typeof(Context));

            if (new[] { RepositoryType.Online, RepositoryType.Dummy }.Contains(type))
            {
                Register(typeof(Context), typeof(OnlineDatabase));
            }
            else
            {
                Register(typeof(Context), typeof(OfflineDatabase));
            }
        }

        private static void RegisterRepository(IRepositoryMap map, RepositoryType type)
        {
            Type repoType = map.DummyRepository;

            if (!Settings.DEBUGGING && type != RepositoryType.Dummy)
            {
                repoType = map.Repository;
                if (map.Repository == null)
                {
                    repoType = map.DummyRepository;
                    Console.WriteLine($"Warning: reverting to dummy repo for {map.DummyRepository.Name}");
                }
            }

            Register(map.Interface, repoType);
        }

        private static void Register(Type tInterface, Type tClass)
        {
            MethodInfo methodInfo =
                SimpleIoc.Default.GetType().GetMethods().ToList()
                         .Where(m => m.Name == "Register")
                         .Where(m => m.IsGenericMethod)
                         .Where(m => m.GetGenericArguments().Length == 2)
                         .Where(m => m.GetParameters().Length == 0)
                         .Single();

            methodInfo.MakeGenericMethod(tInterface, tClass)
                      .Invoke(SimpleIoc.Default, null);
        }

        private static void Unregister(Type tClass)
        {
            MethodInfo methodInfo =
                SimpleIoc.Default.GetType().GetMethods().ToList()
                         .Where(m => m.Name == "Unregister")
                         .Where(m => m.IsGenericMethod)
                         .Where(m => m.GetGenericArguments().Length == 1)
                         .Where(m => m.GetParameters().Length == 0)
                         .Single();

            methodInfo.MakeGenericMethod(tClass)
                      .Invoke(SimpleIoc.Default, null);
        }
    }
}