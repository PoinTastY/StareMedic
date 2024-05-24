using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using System.Runtime.InteropServices;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif

namespace StareMedic;

public static class MauiProgram
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("HelveticaNeue.ttf", "HelveticaNeue");
				fonts.AddFont("CascadiaCode.ttf", "CascadiaCode-Regular");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("ComicSansMS.ttf", "ComicSansMS");
				fonts.AddFont("OpenSans-Regular.ttf", "Sans Serif");
				fonts.AddFont("Hospital-Regular.ttf", "HospitalRegular");
				
			});
		builder.UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("HelveticaNeue.ttf", "HelveticaNeue");
                fonts.AddFont("CascadiaCode.ttf", "CascadiaCode-Regular");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("ComicSansMS.ttf", "ComicSansMS");
                fonts.AddFont("OpenSans-Regular.ttf", "Sans Serif");
                fonts.AddFont("Hospital-Regular.ttf", "HospitalRegular");

            });
        builder.ConfigureLifecycleEvents(events =>
        {
        #if WINDOWS
                    events.AddWindows(w =>
                    {
                        w.OnWindowCreated(window =>
                        {
                            window.ExtendsContentIntoTitleBar = true; //If you need to completely hide the minimized maximized close button, you need to set this value to false.
                            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            ShowWindow(hWnd, SW_MAXIMIZE);
                            ////WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd); // esto funciona tambien, quitando la importacion al dll y la linea anterior
                            ////var _appWindow = AppWindow.GetFromWindowId(myWndId);// pero esta fullscreen no muestra los botones para abrir o cerrar y oculta la barra
                            ////_appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                        });
                    });
        #endif
                });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
