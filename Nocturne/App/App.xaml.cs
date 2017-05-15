using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Nocturne.BL.Interfaces;
using Nocturne.BL.Services;
using Nocturne.App.Helpers;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Nocturne.BL.Monitors;
using Windows.UI.ViewManagement;

namespace Nocturne.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static int CurrentUserId { get; set; }    
        public static bool IsAdministrator { get; set; } 
        public static bool IsWorker { get; set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            //Initialize Ioc
            var serviceLocator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            
            //Run EF migration
            var dataStore = ServiceLocator.Current.GetInstance<IDataStore>();
            dataStore.Initialize();
        }

        private static IUnityContainer ConfigureUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ICertificateMonitor, CertificateMonitor>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRfidMonitor, RfidMonitor>(new ContainerControlledLifetimeManager());
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICardService, CardService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IClientService, ClientService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDiscountService, DiscountService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDiscountTypeService, DiscountTypeService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISessionService, SessionService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager()); 
            container.RegisterType<IDataStore, DatabaseDataStore>(new ContainerControlledLifetimeManager());
            return container;
        }

        #region Events

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            //Initiate Log channel and Log sessions 
            Logger.GetLogger().InitiateLogger();
            //Delete the logfile which are beyond the dates 
            Logger.GetLogger().Deletefile();

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(LognPage), e.Arguments);
                //var mainModel = ServiceLocator.Current.GetInstance<MainViewModel>();
                //var navigation = ServiceLocator.Current.GetInstance<INavigationService>();
                //navigation.Navigate(mainModel);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        #endregion Events
    }
}
