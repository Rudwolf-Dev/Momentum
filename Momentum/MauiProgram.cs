using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Momentum.Data;

namespace Momentum
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App, MainWindow, AppShell>();
            builder
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                });

            RegisterViewModels(builder);
            RegisterServices(builder);
            RegisterPages(builder);

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if WINDOWS
            // Launch the app window maximized on Windows
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(app =>
                {
                    app.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false;

                        if (window.AppWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                        {
                            //presenter.SetBorderAndTitleBar(false, false);
                            presenter.Maximize();
                        }
                    });
                });
            });
#endif

            return builder.Build();
        }

        // =========================================
        // VIEWMODELS
        // =========================================

        private static void RegisterViewModels(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<BaseViewModel>();

            builder.Services.AddSingleton<AppViewModel>();

            builder.Services.AddSingleton<DashboardViewModel>();

            builder.Services.AddSingleton<TasksViewModel>();

            builder.Services.AddSingleton<NotesViewModel>();

            builder.Services.AddSingleton<HabitsViewModel>();

            builder.Services.AddSingleton<SettingsViewModel>();
        }

        // =========================================
        // SERVICES
        // =========================================

        private static void RegisterServices(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IDialogService, DialogService>();

            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddSingleton<TaskService>();

            builder.Services.AddSingleton<NoteService>();

            builder.Services.AddSingleton<HabitService>();
        }

        // =========================================
        // PAGES
        // =========================================

        private static void RegisterPages(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DashboardPage>();

            builder.Services.AddSingleton<TasksPage>();

            builder.Services.AddSingleton<NotesPage>();

            builder.Services.AddSingleton<HabitsPage>();

            builder.Services.AddSingleton<SettingsPage>();
        }
    }
}
