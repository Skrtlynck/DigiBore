using System;
using System.Collections.Generic;
using System.IO;
using DigiBore.Model;
using DigiBore.Repositories;
using DigiBore.Repositories.Interfaces;
using DigiBore.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DigiBore
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public IHost Host { get; }

        public App()
        {
            InitializeComponent();

            Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((context, services) =>
            {
                // Services

                // Repositories
                services.AddSingleton<IProjectsRepository, ProjectsRepository>();
                // Views and ViewModels
                services.AddTransient<ProjectsViewModel>();
            }).Build();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.AppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            m_window.Activate();
        }

        private Window m_window;

        public static T GetService<T>()
        where T : class
        {
            if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }

            return service;
        }
    }
   
}
