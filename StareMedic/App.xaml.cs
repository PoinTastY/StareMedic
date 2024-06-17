using System.Runtime.InteropServices;

namespace StareMedic;

public partial class App : Application
{
#if WINDOWS
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
#endif



public App()
	{
		InitializeComponent();
        
        Application.Current.UserAppTheme = AppTheme.Dark;

       
        //fullscreen
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
#if WINDOWS
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            ShowWindow(windowHandle, 3);
#endif
        });

        MainPage = new AppShell();

    }


     

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        

        window.Title = "StareMedic";
        

        return window;
    }



}
