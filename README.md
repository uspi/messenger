# Messenger

<a href="https://docs.microsoft.com/en-us/dotnet/framework/">
    <img src="https://img.shields.io/badge/.Net_Framework-v4.5-brightgreen"/>
</a>
<a href="https://docs.microsoft.com/en-us/dotnet/framework/">
    <img src="https://img.shields.io/badge/Ninject-v3.3.4-brightgreen"/>
</a>

The messenger assumes registration and subsequent login to your account. It uses a division into three directories: client, core, server. Event model, using the `MVVM` pattern. The implementation: 
* `IoC Ninject`, 
* `Expression Trees`, 
* `Custom Controls (WPF)`,
* `Attached Properties`,
* `Value Converters`,
* `user32.dll` based Window Resizer. 
* `TCP/IP` for communication with server
* `Fody Nuget` for automatic implementation of INotifyPropertyChanged interface

Authorization is implemented, and all data is stored in the SQL Database.

Pattern MVVM is implemented according to concept WPF.


## UI

* WPF foundation for GUI
  + XAML / XAML styles

Fonts:

* IBM Plex Sans
* Noto Sans
* Font Awesome

### Gratitude

The basis for this repository was the "WPF UI Programming (C #)" video cycle from which I borrowed advanced topics such as "Expression trees" and "Memory allocation for user password".